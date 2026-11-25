using Code;
using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private int _round = 0;
    public int Round => _round;
    [SerializeField] private int _health = 10;
    public int Health { get => _health; set { _health = value; }}
    public void NextRound()
    {
        _round++;
        OnRoundChange.Invoke();
    }
    
    private int _coins;

    public int Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            OnCoinsChange.Invoke(_coins);
        }
    }
    private Card[] _inventory;
    public Card[] Inventory => _inventory;
    
    private const string _saveFile = "game.json";

    private SaveData? _saveData = null;
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
   
    public event Action<int> OnCoinsChange = delegate { };
    public event Action OnRoundChange = delegate { };
    
    public void Save()
    {
        string filepath = GetFilePath();

        try
        {
            File.WriteAllText(filepath, JsonUtility.ToJson(new SaveData(
                CardDataBase.instance.GetCardIDs(_inventory),
                Health,
                Coins,
                Round
                ), true));
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
        catch
        {
            Debug.Log("No game saved yet");
        }
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
