using System;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "CardPack", menuName = "Card/CardPack")]
    public class CardPack : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        [SerializeField, Range(0f,100f)] private float chance;
        [SerializeField] private int minRound;
        [SerializeField] private CardPoolEntry[] cardPool;
        
        [Serializable]
        public struct CardPoolEntry
        {
            [SerializeField] public Card card;
            [SerializeField, Range(0f,100f)] public float chance;
        } 
    }
}