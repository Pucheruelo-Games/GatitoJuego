using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] Image _barraRonda;
        private float _totalEnemigos;
        
        private void Start()
        {
            _mesa.OnHealthChange += ActualizarVida;
            GameManager.Instance.OnCoinsChange += ActualizarMonedas;
            GameManager.Instance.OnRoundChange += ActualizarRonda;
            _roundManager.CuentaCambio += CuentaAtras;
            _roundManager.RatonMuerto += ActualizarBarraEnemigos;
            _roundManager.TotalRatones += ObtenerEnemigosRonda;
        }
        private void OnDisable()
        {
            _mesa.OnHealthChange -= ActualizarVida;
            GameManager.Instance.OnCoinsChange -= ActualizarMonedas;
            GameManager.Instance.OnRoundChange -= ActualizarRonda;
            _roundManager.CuentaCambio -= CuentaAtras;
            _roundManager.RatonMuerto -= ActualizarBarraEnemigos;
            _roundManager.TotalRatones -= ObtenerEnemigosRonda;
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
        private void ActualizarBarraEnemigos(int enemigos)
        {
            Debug.Log("DIVISION: "+enemigos/_totalEnemigos+" ENEMIGOS: "+enemigos+" TOTAL: "+_totalEnemigos);
            _barraRonda.fillAmount = enemigos/_totalEnemigos;
        }
        private void ObtenerEnemigosRonda(int enemigos)
        {
            _totalEnemigos = enemigos;
            _barraRonda.fillAmount = 1;
        }

        private void Perder()
        {

        }
    }
}