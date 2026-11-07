using System;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] Mesa _mesa;
    [SerializeField] Monedas _monedas;
    [SerializeField] TextMeshProUGUI _vidaUI;
    [SerializeField] TextMeshProUGUI _monedasUI;
    private void OnEnable()
    {
        _mesa.VidaCambiada += ActualizarVida;
        _mesa.HasPerdido += Perder;
        _monedas.MonedasCambiadas += ActualizarMonedas;
    }
    private void OnDisable()
    {
        _mesa.VidaCambiada -= ActualizarVida;
        _mesa.HasPerdido -= Perder;
        _monedas.MonedasCambiadas -= ActualizarMonedas;

    }
    private void ActualizarMonedas(int monedas)
    {
        Debug.Log("Se han cambiado las monedas");
        _monedasUI.text = monedas.ToString();
    }
    private void ActualizarVida(float vida)
    {
        _vidaUI.text = vida.ToString();
    }

    private void Perder()
    {

    }
}
