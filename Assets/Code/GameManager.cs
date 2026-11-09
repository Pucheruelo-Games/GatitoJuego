using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private int _round = 1;
    public int Round => _round;

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
}
