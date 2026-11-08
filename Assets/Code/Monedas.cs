using System;
using UnityEngine;

public class Monedas : MonoBehaviour
{
    [SerializeField] private int _monedasAct;
    public int MonedasAct => _monedasAct;
    public event Action<int> MonedasCambiadas;

    public void GanarMonedas(int m)
    {
        Debug.Log("Me han dado monedas");
        _monedasAct += m;
        MonedasCambiadas?.Invoke(_monedasAct);
    }
    public bool GastarMonedas(int m)
    {
        if(_monedasAct<m)return false;
        _monedasAct -= m;
        MonedasCambiadas?.Invoke(_monedasAct);
        return true;
    }
}
