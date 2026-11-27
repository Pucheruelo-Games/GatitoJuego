using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    [CreateAssetMenu(fileName = "CardPack", menuName = "Card/CardPack")]
    public class CardPack : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        [SerializeField, Range(0f,100f)] private float chance;
        public float Chance => chance;
        [SerializeField] private int minRound;
        public int MinRound => minRound;
        [SerializeField] private int cost;
        public int Cost => cost;
        [SerializeField] private CardPoolEntry[] cardPool;

        public Card GetRandomCard()
        {
            CardPoolEntry? selectedEntry = null;
            int tries = 100;
            while (selectedEntry == null && tries > 0)
            {
                selectedEntry = cardPool[UnityEngine.Random.Range(0, cardPool.Length)];
                if (Random.Range(0f, 100f) > ((CardPoolEntry)selectedEntry).chance) 
                    selectedEntry = null;
            }

            if (selectedEntry == null) selectedEntry = cardPool[0];

            return ((CardPoolEntry)selectedEntry).card;
        }
        
        [Serializable]
        public struct CardPoolEntry
        {
            [SerializeField] public Card card;
            [SerializeField, Range(0f,100f)] public float chance;
        } 
    }
}