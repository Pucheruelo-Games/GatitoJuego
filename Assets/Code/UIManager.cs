using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] Table _mesa;
        [SerializeField] EnemySpawner _roundManager;
        [SerializeField] TextMeshProUGUI _vidaUI;
        [SerializeField] TextMeshProUGUI _monedasUI;
        [SerializeField] GameObject _cuentaAtrasTxt;
        [SerializeField] GameObject _rondaTxt;
        
        private void Start()
        {
            _mesa.OnHealthChange += ActualizarVida;
            GameManager.Instance.OnCoinsChange += ActualizarMonedas;
            _roundManager.CambioRonda += ActualizarRonda;
            _roundManager.CuentaCambio += CuentaAtras;
        }
        private void OnDisable()
        {
            _mesa.OnHealthChange -= ActualizarVida;
            GameManager.Instance.OnCoinsChange -= ActualizarMonedas;
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
        private void ActualizarRonda()
        {
            _rondaTxt.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Round.ToString();
        }
        private void ActualizarMonedas(int monedas)
        {
            Debug.Log("Se han cambiado las monedas");
            _monedasUI.text = monedas.ToString();
        }
        private void ActualizarVida(int vida)
        {
            _vidaUI.text = vida.ToString();
        }

        private void Perder()
        {

        }
    }
}