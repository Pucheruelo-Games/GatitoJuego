using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Code
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private PreviewHandler cardPreview;
        [SerializeField] private InventoryCard cardPrefab;
        [SerializeField] private RectTransform cardContainer;
        [SerializeField] private RectTransform selectedContainer;
        [SerializeField] private int cardSlots;
        [SerializeField] private Card[] startCards;
        private List<InventoryCard> _cards = new();
        private InventoryCard _selectedCard;
        private InventoryCard _carriedCard;
        private LayerMask _tileLayerMask;

        public InventoryCard CarriedCard
        {
            get => _carriedCard;
            set
            {
                _carriedCard = value;
            }
        }

        private static Inventory _instance;
        public static Inventory Instance => _instance;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            _tileLayerMask = LayerMask.GetMask("Tile");
        }

        private void Update()
        {
            if (GetPointedCard(out var card))
            {
                if (!_carriedCard)
                {
                    if (_selectedCard)
                    {
                        _selectedCard.OnDeselected();
                    }
                    _selectedCard = card;
                    _selectedCard.OnSelected(selectedContainer);
                }
            }
            
            if (InputManager.Instance.Player.Click.WasPressedThisFrame())
            {
                if (card)
                {
                    _carriedCard = card;
                }
                else if (_selectedCard)
                {
                    var canvasPos = InputManager.Instance.Player.Position.ReadValue<Vector2>();
                    var worldPos = Camera.main.ScreenToWorldPoint(canvasPos) * Vector2.one;

                    if (GetSelectedTiles(worldPos, _selectedCard.Card) != null)
                    {
                        _carriedCard = _selectedCard;
                    }
                }
            }
            
            if (_carriedCard)
            {
                var canvasPos = InputManager.Instance.Player.Position.ReadValue<Vector2>();
                var worldPos = Camera.main.ScreenToWorldPoint(canvasPos) * Vector2.one;

                var tiles = GetSelectedTiles(worldPos, _selectedCard.Card);
                
                if (InputManager.Instance.Player.Click.WasReleasedThisFrame() && _carriedCard)
                {
                    _carriedCard.OnCarriedUpdate(tiles, canvasPos, cardPreview, true);
                    _carriedCard = null;
                }
                else
                {
                    _carriedCard.OnCarriedUpdate(tiles, canvasPos, cardPreview);
                }
            }
        }
        
        private Tile[] GetSelectedTiles(Vector2 worldPos, Card card)
        {
            foreach (var hit in Physics2D.OverlapBoxAll(worldPos, Vector2.one * 0.1f, 0, _tileLayerMask))
            {
                if (!hit.TryGetComponent(out Tile tile)) continue;
                
                return GetAreaTiles(tile, card);
            }
            return null;
        }


        private Tile[] GetAreaTiles(Tile center, Card card)
        {
            if (!center) return null;

            if (card.Width <= 1 && card.Height <= 1) return new Tile[] { center };

            List<Tile> tileList = new();
            
            int rightCount = 0;
            int leftCount = 0;
            int upCount = 0;
            int downCount = 0;

            Tile left = center;
            Tile up = center;
            Tile down = center;
            Tile right = center;

            int leftOffset = Mathf.CeilToInt((card.Width-1f)/2f);
            int rightOffset = Mathf.FloorToInt((card.Width - 1f) / 2f);
            int upOffset = Mathf.CeilToInt((card.Height-1f)/2f);
            int downOffset = Mathf.FloorToInt((card.Width-1f)/2f);
            
            // LEFT
            for (int i = 0; i < leftOffset; i++)
            {
                left = left.Left;
                if (!left) leftCount++;
            }
            for (int i = 0; i < leftCount; i++) center = center.Right;
            
            // RIGHT
            for (int i = 0; i < rightOffset; i++)
            {
                right = right.Right;
                if (!right) rightCount++;
            }
            for (int i = 0; i < rightCount; i++) center = center.Left;
            
            // UP
            for (int i = 0; i < upOffset; i++)
            {
                up = up.Up;
                if (!up) upCount++;
            }
            for (int i = 0; i < upCount; i++) center = center.Down;
            
            // DOWN
            for (int i = 0; i < downOffset; i++)
            {
                down = down.Down;
                if (!down) downCount++;
            }
            for (int i = 0; i < downCount; i++) center = center.Up;
            
            for (int i = 0; i < leftOffset; i++) center = center.Left;
            for (int i = 0; i < upOffset; i++) center = center.Up;


            for (int x = 0; x < card.Width; x++)
            {
                Tile current = center;
                
                for (int y = 0; y < card.Height; y++)
                {
                    tileList.Add(current);
                    current = current.Down;
                }

                center = center.Right;
            }

            return tileList.ToArray();
        }
        
        private static bool GetPointedCard(out InventoryCard card)
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = InputManager.Instance.Player.Position.ReadValue<Vector2>();
            List<RaycastResult> result = new();
            EventSystem.current.RaycastAll(eventData, result);
            foreach (var hit in result)
            {
                print(hit.gameObject.name);
                if (!hit.gameObject.TryGetComponent(out InventoryCard c))
                    continue;
                
                card = c;
                return true;
            }

            card = null;
            return false;
        }

        private void Start()
        {
            foreach (var card in startCards) AddCard(card);
        }

        public bool AddCard(Card card)
        {
            if (_cards.Count >= cardSlots) return false;
            var newCard = Instantiate(cardPrefab, cardContainer);
            newCard.Init(card);
            _cards.Add(newCard);
            return true;
        }
    }
}