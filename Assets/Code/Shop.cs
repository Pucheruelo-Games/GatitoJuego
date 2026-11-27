using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Code
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private RectTransform cardContainer;
        [SerializeField] private InventoryCard cardPrefab;
        [SerializeField] private RectTransform[] packSlots;
        [SerializeField] private CardPack[] cardPacks;
        [SerializeField] private ShopCardPack cardPackPrefab;
        [SerializeField] private RectTransform selectedContainer;
        private InventoryCard _selectedCard;
        private List<InventoryCard> _cards = new();
        private InventoryCard _carriedCard;

        private void Start()
        {
            GameManager.Instance.LoadGame();
            foreach (Card card in GameManager.Instance.Inventory)
            {
                AddCard(card);
            }

            foreach (RectTransform slot in packSlots)
            {
                var possiblePacks = cardPacks.Where(p => p.MinRound <= GameManager.Instance.Round).ToArray();
                
                CardPack selectedPack = null;
                int tries = 100;
                while (!selectedPack && tries > 0)
                {
                    selectedPack = possiblePacks[UnityEngine.Random.Range(0, possiblePacks.Length)];
                    if (Random.Range(0f, 100f) > selectedPack.Chance) 
                        selectedPack = null;
                }

                if (!selectedPack) selectedPack = cardPacks[0];

                ShopCardPack newPack = Instantiate(cardPackPrefab, slot);
                newPack.transform.position = slot.position;
                newPack.Init(selectedPack);
            }
        }

        private void Update()
        {
            if (GetPointedCard(out var iCard))
            {
                if (!_carriedCard)
                {
                    if (_selectedCard)
                    {
                        _selectedCard.OnDeselected();
                    }

                    if (_cards.Contains(iCard))
                    {
                        _selectedCard = iCard;
                        _selectedCard.OnSelected(selectedContainer);
                    }
                }
            }
            
            if (InputManager.Instance.Player.Click.WasPerformedThisFrame())
            {
                if (iCard)
                {
                    _carriedCard = iCard;
                }
                else if (GetPointedPack(out var pack))
                {
                    if (pack.TryOpen(out Card card))
                    {
                        print($"[PACK] opened and obtain {card.DisplayName}");
                        if (_cards.Count < GameManager.Instance.CardSlots)
                        {
                            AddCard(card);
                        }
                        else
                        {
                            InventoryCard newCard = Instantiate(cardPrefab, cardContainer);
                            newCard.Init(card);
                        }
                    }
                }
            }

            if (_carriedCard)
            {
                var canvasPos = InputManager.Instance.Player.Position.ReadValue<Vector2>();

                _carriedCard.OnCarriedUpdate(canvasPos);
                
                if (InputManager.Instance.Player.Click.WasReleasedThisFrame())
                {
                    _carriedCard.OnDeselected();
                    
                    if (GetPointedShopSlot(out var slot))
                    {
                        _carriedCard.transform.position = slot.transform.position;
                        _carriedCard = null;
                    }
                    else
                    {
                        _carriedCard = null;
                    }
                    
                }
            }
        }

        public void MoveCard(InventoryCard card)
        {
            
        }

        IEnumerator MoveCardCoroutine()
        {
            yield return null;
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
        
        private static bool GetPointedPack(out ShopCardPack cardPack)
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = InputManager.Instance.Player.Position.ReadValue<Vector2>();
            List<RaycastResult> result = new();
            EventSystem.current.RaycastAll(eventData, result);
            foreach (var hit in result)
            {
                print(hit.gameObject.name);
                if (!hit.gameObject.TryGetComponent(out ShopCardPack hittedPack))
                    continue;
                
                cardPack = hittedPack;
                return true;
            }

            cardPack = null;
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