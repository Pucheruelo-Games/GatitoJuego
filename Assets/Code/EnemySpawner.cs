using System;
using System.Collections;
using System.Linq;
using Code;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] MouseBehaviour[] prefabs;
    [SerializeField] GameObject[] _spawners;
    private int _spawneados;
    [SerializeField] float _intervalo = 4;
    [SerializeField] int _tiempoPausa;
    private int _ronda;
    public Action<string> CuentaCambio;
    int _enemRonda = 6;
    private int _tiempo;
    private System.Collections.Generic.List<MouseBehaviour> _ratonesSpawneables = new System.Collections.Generic.List<MouseBehaviour>();
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
        CrearListaEnemigosRonda();
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
    private void CrearListaEnemigosRonda()
    {
        _ratonesSpawneables.Clear();
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i].Difficulty<=_ronda)
            {
                _ratonesSpawneables.Add(prefabs[i]);
            }
        }
        _ratonesSpawneables.Sort();
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
            
            MouseBehaviour r= (Instantiate(RatonesCampanaDeGauss(), spawner, Quaternion.identity));
            
            _spawneados++;
            r.OnDeath += OnMouseDeath;
            Debug.Log("Ha spawneado una rata");
            cantidad--;
        }

        yield return new WaitUntil(TodosMuertos);
        CambiarRonda();
    }
    private MouseBehaviour RatonesCampanaDeGauss()
    {
        float r1 = Random.Range(0, _ronda+1);
        float r2 = Random.Range(0, _ronda+1);
        int dificultad = Mathf.FloorToInt((r1 + r2) / 2);

        var candidatos = _ratonesSpawneables.Where(r => r.Difficulty == dificultad).ToList();
        if(candidatos.Count<=0)
        {
            return _ratonesSpawneables.OrderBy(r => Mathf.Abs(r.Difficulty - dificultad)).First();
        }
        int r3 = Random.Range(0, candidatos.Count);
        return candidatos[r3];
    }
    private bool TodosMuertos() => _spawneados <= 0;

    private void OnMouseDeath()
    {
        _spawneados--;
    }
}