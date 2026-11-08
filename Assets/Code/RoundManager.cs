using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] int _ronda = 1;
    [SerializeField] float _intervalo = 4;
    [SerializeField] SpawnerEnemigos _spawner;
    [SerializeField] int _tiempoPausa;
    public Action<int> CambioRonda;
    public Action<String> CuentaCambio;
    int _enemRonda = 8;
    private int _tiempo;
    private void Awake()
    {
        _tiempo = _tiempoPausa;
    }
    private void Start()
    {
        CambioRonda?.Invoke(_ronda);
        StartCoroutine(CuentaAtras());
    }
    private IEnumerator CuentaAtras()
    {
        if (_ronda > 1)
        {
            yield return new WaitForSeconds(1f);
            TransicionTienda();
        }
        while (_tiempo > 0)
        {
            Debug.Log(_tiempo+"!");
            CuentaCambio?.Invoke(_tiempo.ToString());
            yield return new WaitForSeconds(1f);
            _tiempo--;
            
        }
        CuentaCambio?.Invoke("YA!");
        yield return new WaitForSeconds(1f);
        CuentaCambio?.Invoke(_tiempo.ToString());

        _tiempo = _tiempoPausa;
        EmpezarRonda();
    }

    private void TransicionTienda()
    {
        //XD
    }

    private void EmpezarRonda()
    {
        float intervalo = Mathf.Max(0.15f, _intervalo / _ronda);
        int enemVar = UnityEngine.Random.Range(_enemRonda-_ronda, _enemRonda+_ronda);
        int cantidad = enemVar * _ronda;

        Debug.Log("Ronda: " + _ronda + "CantidadEnemigos: " + cantidad);
        StartCoroutine(_spawner.SpawnearEnemigosDeRonda(cantidad,intervalo,_ronda));
    }
    public void CambiarRonda()
    {
        _ronda++;
        CambioRonda?.Invoke(_ronda);
        StartCoroutine(CuentaAtras());
    }
}
