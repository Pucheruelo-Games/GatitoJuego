using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code
{
    public class Inventory : MonoBehaviour
    {
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
            if (_carriedCard)
            {
                _carriedCard.transform.position = Mouse.current.position.ReadValue();
            }
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