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

        private Tile[] GetAreaTiles(Tile center, Card card, int lookBack = 0)
        {
            if (!center) return null;
            
            if (card.Width <= 1 && card.Height <= 1)
            {
                return new []{center};
            }

            if (lookBack >= 16) return null;
                    
            var right = center;
            var left = center;
                    
            var up = center;
            var down = center;
                    
            List<Tile> tileList = new();
                
            tileList.Add(center);

            for (int x = 1; x < card.Width; x++)
            {
                if (x % 2 == 0)
                {
                    if (right.Right) right = right.Right;
                    else return GetAreaTiles(center.Left, card, lookBack+1);
                    
                    tileList.Add(right);
                }
                else
                {
                    if (left.Left) left = left.Left;
                    else return GetAreaTiles(center.Right, card, lookBack+1);
                    
                    tileList.Add(left);
                }
                        
                for (int y = 1; y < card.Height; y++)
                {
                    if (y % 2 == 0)
                    {
                        if (down.Down) down = down.Down;
                        else return GetAreaTiles(center.Up, card, lookBack+1);
                        
                        tileList.Add(down);
                    }
                    else
                    {
                        if (up.Up) up = up.Up;
                        else return GetAreaTiles(center.Down, card, lookBack+1);
                        
                        tileList.Add(up);
                    }
                }
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