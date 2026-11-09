using System;
using UnityEngine;

namespace Code
{
    public class Inventory : MonoBehaviour
    {
        private static Inventory _instance;
        public static Inventory Instance => _instance;

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else _instance = this;
        }

        private InventoryCard[] _cards;
        
        private InventoryCard _selectedCard;
    }
}