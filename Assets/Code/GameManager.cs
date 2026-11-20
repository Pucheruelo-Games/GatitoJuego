using Code;
using System;
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
    
    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else _instance = this;
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
    public event Action<int> OnCoinsChange = delegate { };
    public event Action OnRoundChange = delegate { };

}
