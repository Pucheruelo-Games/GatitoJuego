using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    [Serializable]
    public struct CardDictionary
    {
        public CardEntry[] Entries;

        public int GetId(Card c)
        {
            foreach ((var id, var card) in Entries)
            {
                if (card == c) return id;
            }

            return -1;
        }

        public Card GetCard(int i)
        {
            return Entries.ToList().First(c => c.ID == i).Card;
        }
    }
    [Serializable]
    public struct CardEntry
    {
        public int ID;
        public Card Card;

        public void Deconstruct(out int i, out Card card)
        {
            i = ID;
            card = Card;
        }
    }

    [CreateAssetMenu(fileName = "GameData", menuName = "GameData")]
    public class CardDataBase : ScriptableSingleton<CardDataBase>
    {
        [SerializeField] private CardDictionary cardDictionary;

        [ContextMenu("LoadCards")]
        private void LoadCards()
        {
            List<Card> newCards = new();
            foreach (string guid in AssetDatabase.FindAssets("t:Card"))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                newCards.Add(AssetDatabase.LoadAssetAtPath<Card>(path));
            }

            cardDictionary.Entries = new CardEntry[newCards.Count];
            for (int i = 0; i < newCards.Count; i++)
            {
                cardDictionary.Entries[i] = new CardEntry()
                {
                    ID = i, Card = newCards[i]
                };
            }
        }

        public int GetCardID(Card card)
        {
            return cardDictionary.GetId(card);
        }

        public Card GetCard(int cardID)
        {
            return cardDictionary.GetCard(cardID);
        }

        public Card[] GetCards(params int[] ids)
        {
            Card[] cards = new Card[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                cards[i] = cardDictionary.GetCard(ids[i]);
            }
            return cards;
        }

        public int[] GetCardIDs(params Card[] cards)
        {
            int[] cardIds = new int[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                cardIds[i] = cardDictionary.GetId(cards[i]);
            }
            return cardIds;
        }
    }
}