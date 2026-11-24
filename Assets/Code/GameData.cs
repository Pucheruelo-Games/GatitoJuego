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
            if (!Entries.ToList().Exists(c => c.ID == i))
                return null;

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
    public class GameData : ScriptableSingleton<GameData>
    {
        private const string _saveFile = "game.json";

        private SaveData _saveData;
        public bool HasData => Health > 0;

        [SerializeField] private CardDictionary cards;
        
        private Card[] _inventory;
        public Card[] Inventory
        {
            get => _inventory;
            set => _inventory = value;
        }

        public int Health
        {
            get => _saveData.Health;
            set => _saveData.Health = value;
        }

        public int Coins
        {
            get => _saveData.Coins;
            set => _saveData.Coins = value;
        }

        public int Round
        {
            get => _saveData.Round;
            set => _saveData.Round = value;
        }

        [ContextMenu("LoadCards")]
        private void LoadCards()
        {
            List<Card> newCards = new();
            foreach (string guid in AssetDatabase.FindAssets("t:Card"))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                newCards.Add(AssetDatabase.LoadAssetAtPath<Card>(path));
            }

            cards.Entries = new CardEntry[newCards.Count];
            for (int i = 0; i < newCards.Count; i++)
            {
                cards.Entries[i] = new CardEntry()
                {
                    ID = i, Card = newCards[i]
                };
            }
        }

        public void Save()
        {
            int[] cardIds = new int[_inventory.Length];
            for (int i = 0; i < _inventory.Length; i++)
            {
                cardIds[i] = cards.GetId(_inventory[i]);
            }

            _saveData.CardIds = cardIds;

            string filepath = GetFilePath();

            try
            {
                File.WriteAllText(filepath, JsonUtility.ToJson(_saveData, true));
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Could not save {filepath} : {e.Message}");
            }
        }

        public void Load()
        {
            string filepath = GetFilePath();

            try
            {
                _saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(filepath));
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Could not load {filepath} : {e.Message}");
                return;
            }

            if (_saveData.CardIds == null || _saveData.CardIds.Length <= 0)
            {
                _inventory = Array.Empty<Card>();
                return;
            }
            
            List<Card> inventory = new();
            foreach (int saveId in _saveData.CardIds)
            {
                inventory.Add(cards.GetCard(saveId));
            }

            _inventory = inventory.ToArray();
        }
        
        public struct SaveData
        {
            public int[] CardIds;
            public int Health;
            public int Coins;
            public int Round;

            public SaveData(int[] cardsIds, int health, int coins, int round)
            {
                CardIds = cardsIds;
                Health = health;
                Coins = coins;
                Round = round;
            }
        }

        private string GetFilePath()
        {
#if UNITY_EDITOR
            return _saveFile;
#else
            return Path.Combine(Application.persistentDataPath, _saveFile);
#endif
        }
    }
}