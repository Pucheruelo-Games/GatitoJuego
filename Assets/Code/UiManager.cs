using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] Mesa _mesa;
    [SerializeField] Monedas _monedas;
    [SerializeField] RoundManager _roundManager;
    [SerializeField] TextMeshProUGUI _vidaUI;
    [SerializeField] TextMeshProUGUI _monedasUI;
    [SerializeField] GameObject _cuentaAtrasTxt;
    [SerializeField] GameObject _rondaTxt;
    private void OnEnable()
    {
        _mesa.VidaCambiada += ActualizarVida;
        _mesa.HasPerdido += Perder;
        _monedas.MonedasCambiadas += ActualizarMonedas;
        _roundManager.CambioRonda += ActualizarRonda;
        _roundManager.CuentaCambio += CuentaAtras;
    }
    private void OnDisable()
    {
        _mesa.VidaCambiada -= ActualizarVida;
        _mesa.HasPerdido -= Perder;
        _monedas.MonedasCambiadas -= ActualizarMonedas;
        _roundManager.CambioRonda -= ActualizarRonda;
        _roundManager.CuentaCambio -= CuentaAtras;

    }
    private void CuentaAtras(String num)
    {
        if (num.Equals("0"))
        {
            _cuentaAtrasTxt.SetActive(false);
        }
        else
        {
            if (!_cuentaAtrasTxt.activeInHierarchy)
            {
                _cuentaAtrasTxt.SetActive(true);
            }
            _cuentaAtrasTxt.GetComponent<TextMeshProUGUI>().text = num + "!";
        }
        
    }
    private void ActualizarRonda(int ronda)
    {
        _rondaTxt.GetComponent<TextMeshProUGUI>().text = ronda.ToString();
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
