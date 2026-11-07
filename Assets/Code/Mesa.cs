using System;
using UnityEngine;

public class Mesa : MonoBehaviour, IDañable
{
    [SerializeField] float _hpMax = 10f;
    private float _hp;
    private bool _estaMuelto =false;

    public event Action<float> VidaCambiada;
    public event Action HasPerdido;
    private void Start()
    {
        _hp = _hpMax;
        VidaCambiada?.Invoke(_hp);
    }
    public void RecibirDmg(float dmg)
    {
        if (_estaMuelto) return;
        _hp -= dmg;
        VidaCambiada?.Invoke(_hp);
        if ( _hp < 0)
        {
            Morir();
            _estaMuelto = true;
            HasPerdido?.Invoke();
        }
    }
    private void Morir()
    {
        Debug.Log("Aqui se acabaria la partida");
    }
}
