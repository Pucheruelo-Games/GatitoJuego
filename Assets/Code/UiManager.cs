using System;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] Mesa _mesa;
    [SerializeField] TextMeshProUGUI _vidaUI;

    private void OnEnable()
    {
        _mesa.VidaCambiada += ActualizarVida;
        _mesa.HasPerdido += Perder;
    }
    private void OnDisable()
    {
        _mesa.VidaCambiada -= ActualizarVida;
        _mesa.HasPerdido -= Perder;
    }

    private void ActualizarVida(float vida)
    {
        _vidaUI.text = vida.ToString();
    }

    private void Perder()
    {

    }
}
