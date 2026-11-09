using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
    public static GameManager Singleton => _singleton;

    private void Awake()
    {
        if (Singleton) Destroy(gameObject);
        else _singleton = this;
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
