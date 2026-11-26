using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private RectTransform cardContainer;
        [SerializeField] private InventoryCard cardPrefab;
        private List<InventoryCard> _cards = new();
        private InventoryCard _carriedCard;

        private void Start()
        {
            GameManager.Instance.LoadGame();
            foreach (Card card in GameManager.Instance.Inventory)
            {
                AddCard(card);
            }
        }

        private void Update()
        {
            if (InputManager.Instance.Player.Click.WasPerformedThisFrame())
            {
                if (GetPointedCard(out InventoryCard card))
                {
                    _carriedCard = card;
                }
            }

            if (_carriedCard)
            {
                var canvasPos = InputManager.Instance.Player.Position.ReadValue<Vector2>();

                _carriedCard.transform.position = canvasPos;
                
                if (InputManager.Instance.Player.Click.WasReleasedThisFrame())
                {
                    if (GetPointedShopSlot(out var slot))
                    {
                        _carriedCard.transform.position = slot.transform.position;
                        _carriedCard = null;
                    }
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

        private static bool GetPointedShopSlot(out GameObject slot)
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = InputManager.Instance.Player.Position.ReadValue<Vector2>();
            List<RaycastResult> result = new();
            EventSystem.current.RaycastAll(eventData, result);
            foreach (var hit in result)
            {
                print(hit.gameObject.name);
                if (!hit.gameObject.CompareTag("ShopSlot"))
                    continue;
                
                slot = hit.gameObject;
                return true;
            }

            slot = null;
            return false;
        }
        
        public bool AddCard(Card card)
        {
            if (!card) return false;
            InventoryCard newCard = Instantiate(cardPrefab, cardContainer);
            newCard.Init(card);
            _cards.Add(newCard);
            return true;
        }
    }
}