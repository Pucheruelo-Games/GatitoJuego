using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

namespace Code
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cardPreview;
        [SerializeField] private InventoryCard cardPrefab;
        [SerializeField] private RectTransform cardContainer;
        [SerializeField] private int cardSlots;
        [SerializeField] private Card[] startCards;
        private List<InventoryCard> _cards = new();
        private InventoryCard _selectedCard;
        private InventoryCard _carriedCard;
        private InventoryCard _precard;
        private Vector3 _precardpos;
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
            GetPointedCard(out var precard);

            if (precard != _precard)
            {
                if (_precard != null)
                {
                    _precard.VisualRect.position = _precardpos;
                }

                _precard = precard;
                if (precard != null && _precard != _carriedCard)
                {
                    _precardpos = _precard.VisualRect.position;
                    var pos = _precardpos;
                    pos.y += 15;

                    _precard.VisualRect.position = pos;
                }
            }
            
            if (InputManager.Instance.Player.Click.WasPressedThisFrame() && GetPointedCard(out var card))
            {
                _carriedCard = card;
            }
            
            if (_carriedCard)
            {
                var position = InputManager.Instance.Player.Position.ReadValue<Vector2>();
                var worldPos = Camera.main.ScreenToWorldPoint(position) * Vector2.one;
                _carriedCard.VisualRect.position = position;
                _carriedCard.OnCarriedUpdate(worldPos, cardPreview);
                
                if (InputManager.Instance.Player.Click.WasReleasedThisFrame() && _carriedCard)
                {
                    _carriedCard.OnCardDrop(worldPos);
                    cardPreview.gameObject.SetActive(false);
                    _carriedCard.VisualRect.gameObject.SetActive(true);
                    _carriedCard.VisualRect.localPosition = Vector3.zero;
                    _carriedCard = null;
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