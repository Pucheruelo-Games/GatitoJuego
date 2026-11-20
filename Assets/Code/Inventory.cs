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
        [SerializeField] private int cardSlots;
        [SerializeField] private Card[] startCards;
        private List<InventoryCard> _cards = new();
        private InventoryCard _selectedCard;
        private InventoryCard _carriedCard;

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
            }
            else
            {
                _instance = this;
            }
        }

        private void Update()
        {
            if (InputManager.Instance.Player.Click.WasPressedThisFrame() && GetPointedCard(out var card))
            {
                _carriedCard = card;
            }
            
            if (_carriedCard)
            {
                var canvasPos = InputManager.Instance.Player.Position.ReadValue<Vector2>();
                var worldPos = Camera.main.ScreenToWorldPoint(canvasPos) * Vector2.one;
                
                if (InputManager.Instance.Player.Click.WasReleasedThisFrame() && _carriedCard)
                {
                    _carriedCard.OnCarriedUpdate(worldPos, canvasPos, cardPreview, true);
                    _carriedCard = null;
                }
                else
                {
                    _carriedCard.OnCarriedUpdate(worldPos, canvasPos, cardPreview);
                }
            }
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