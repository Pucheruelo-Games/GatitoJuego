using System;
using System.Collections;
using Code;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] MouseBehaviour _prefab;
    [SerializeField] MouseData[] _tipos;
    [SerializeField] GameObject[] _spawners;
    private int _spawneados;
    [SerializeField] float _intervalo = 4;
    [SerializeField] int _tiempoPausa;
    private int _ronda;
    public Action<string> CuentaCambio;
    int _enemRonda = 8;
    private int _tiempo;
    List<MouseData> PosiblesSpawned = new();
    private void Awake()
    {
        _tiempo = _tiempoPausa;
    }
    private void Start()
    {
        GameManager.Instance.NextRound();
        StartCoroutine(CuentaAtras());
    }
    private IEnumerator CuentaAtras()
    {
        _ronda = GameManager.Instance.Round;
        if (_ronda > 1)
        {
            yield return new WaitForSeconds(1f);
        }
        while (_tiempo > 0)
        {
            Debug.Log(_tiempo+"!");
            CuentaCambio?.Invoke(_tiempo.ToString());
            yield return new WaitForSeconds(1f);
            _tiempo--;
            
        }
        CuentaCambio?.Invoke("YA!");
        GenerarPosiblesEnemigos();
        yield return new WaitForSeconds(1f);
        CuentaCambio?.Invoke(_tiempo.ToString());

        _tiempo = _tiempoPausa;
        EmpezarRonda();
    }
    private void GenerarPosiblesEnemigos()
    {
        PosiblesSpawned.Clear();
        for (int i = 0; i < _tipos.Length; i++)
        {
            if (_tipos[i].Difficulty <= _ronda)
            {
                PosiblesSpawned.Add(_tipos[i]);
            }
        }
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
        StartCoroutine(SpawnearEnemigosDeRonda(cantidad,intervalo,_ronda));
    }
    private void CambiarRonda()
    {
        
        GameManager.Instance.NextRound();
        TransicionTienda();
        StartCoroutine(CuentaAtras());
    }

    public IEnumerator SpawnearEnemigosDeRonda(int cantidad, float intervalo, int ronda)
    {
        while (cantidad > 0)
        {
            float var = Random.Range(-0.5f, 0.5f);
            float espera = Mathf.Max(0.1f, intervalo + var);
            yield return new WaitForSeconds(espera);

            int carril = Random.Range(0, 6);
            Vector2 spawner = _spawners[carril].transform.position;
           
            MouseBehaviour r = Instantiate(_prefab, spawner, Quaternion.identity);

            r.Init(PosiblesSpawned[Random.Range(0, PosiblesSpawned.Count)]);
            _spawneados++;
            r.OnEntityDeath += RatonMuelto;
            Debug.Log("Ha spawneado una rata");
            cantidad--;
        }

        yield return new WaitUntil(TodosMuertos);
        CambiarRonda();
    }

    private bool TodosMuertos()
    {
        return _spawneados <= 0;
    }

    private void RatonMuelto()
    {
        _spawneados--;
    }
}