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
    private int _totalspawneados;
    [SerializeField] float _intervalo = 4;
    [SerializeField] int _tiempoPausa;
    private int _ronda;
    public Action<string> CuentaCambio;
    public Action<int> TotalRatones;

    public Action<int> RatonMuerto;
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
        CrearListaEnemigosRonda();
        while (_tiempo > 0)
        {
            Debug.Log(_tiempo+"!");
            CuentaCambio?.Invoke(_tiempo.ToString());
            yield return new WaitForSeconds(1f);
            _tiempo--;
            
        }
        CuentaCambio?.Invoke("GO!");
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
        _spawneados = 0;
        int cantidad = CalcularCantidad();
        float duracion = Mathf.Max(0.1f, Mathf.Min(3f * _ronda+5, 60f));
        float intervalo = duracion / cantidad;

        Debug.Log("Ronda: " + _ronda + "CantidadEnemigos: " + cantidad);
        StartCoroutine(SpawnearEnemigosDeRonda(cantidad,intervalo,_ronda));
    }

    private int CalcularCantidad()
    {
        float media = 2f * _ronda + 2f;
        int cantidad = Mathf.RoundToInt(media * Random.Range(0.8f, 1.2f));
        return Mathf.Max(1, cantidad);
    }
    private void CambiarRonda()
    {
        GameManager.Instance.NextRound();
        TransicionTienda();
        StartCoroutine(CuentaAtras());
    }
    private IEnumerator RondaTerminada()
    {
        GameManager.Instance.Coins += 10;
        yield return new WaitForSeconds(1f);
        CambiarRonda();
    }

    public IEnumerator SpawnearEnemigosDeRonda(int cantidad, float intervalo, int ronda)
    {
        _spawneados = cantidad;
        TotalRatones?.Invoke(_spawneados);
        while (cantidad > 0)
        {
            float espera = Mathf.Max(0.1f, intervalo * Random.Range(0.6f, 1.4f));
            yield return new WaitForSeconds(espera);

            int carril = Random.Range(0, 6);
            Vector2 spawner = _spawners[carril].transform.position;
            
            MouseBehaviour r= (Instantiate(RatonesCampanaDeGauss(), spawner, Quaternion.identity));
            
            
            r.OnDeath += OnMouseDeath;
            Debug.Log("Ha spawneado una rata");
            cantidad--;
        }

        yield return new WaitUntil(TodosMuertos);
        StartCoroutine(RondaTerminada());
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
        RatonMuerto?.Invoke(_spawneados);
    }
}