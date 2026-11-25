using System;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "CardPack", menuName = "Card/CardPack")]
    public class CardPack : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        [SerializeField] private CardPoolEntry[] cardPool;
        
        [Serializable]
        public struct CardPoolEntry
        {
            [SerializeField] public Card Card;
            [SerializeField] public float Chance;
        } 
    }
}