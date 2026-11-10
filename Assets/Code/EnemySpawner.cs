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
    [SerializeField] MouseData[] _enemys;
    [SerializeField] GameObject[] _spawners;
   
    [SerializeField] float _timeBetweenSpawn = 4;
    [SerializeField] int _pauseTimer;
    private int _spawned;
    private int _round;
    public Action<string> PauseTimerChanged;
    int _enemysRound = 8;
    private int _timer;
    List<MouseData> ProbabilitySpawn = new();
    private void Awake()
    {
        _timer = _pauseTimer;
    }
    private void Start()
    {
        GameManager.Instance.NextRound();
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        _round = GameManager.Instance.Round;
        if (_round > 1)
        {
            yield return new WaitForSeconds(1f);
        }
        while (_timer > 0)
        {
            Debug.Log(_timer+"!");
            PauseTimerChanged?.Invoke(_timer.ToString());
            yield return new WaitForSeconds(1f);
            _timer--;
            
        }
        PauseTimerChanged?.Invoke("YA!");
        GenerateProbabilityEnemys();
        yield return new WaitForSeconds(1f);
        PauseTimerChanged?.Invoke(_timer.ToString());

        _timer = _pauseTimer;
        BeginRound();
    }
    private void GenerateProbabilityEnemys()
    {
        ProbabilitySpawn.Clear();
        for (int i = 0; i < _enemys.Length; i++)
        {
            if (_enemys[i].Difficulty <= _round)
            {
                ProbabilitySpawn.Add(_enemys[i]);
            }
        }
    }

    private void GoToShop()
    {
        //XD
    }

    private void BeginRound()
    {
        float betweenSpawns = Mathf.Max(0.15f, _timeBetweenSpawn / _round);
        int enemVar = UnityEngine.Random.Range(_enemysRound-_round, _enemysRound+_round);
        int quantity = enemVar * _round;

        Debug.Log("Ronda: " + _round + "CantidadEnemigos: " + quantity);
        StartCoroutine(SpawnEnemyRound(quantity,betweenSpawns));
    }
    private void ChangeRound()
    {
        
        GameManager.Instance.NextRound();
        GoToShop();
        StartCoroutine(Countdown());
    }

    public IEnumerator SpawnEnemyRound(int quantity, float betweenEnemys)
    {
        while (quantity > 0)
        {
            float var = Random.Range(-0.5f, 0.5f);
            float espera = Mathf.Max(0.1f, betweenEnemys + var);
            yield return new WaitForSeconds(espera);

            int carril = Random.Range(0, 6);
            Vector2 spawner = _spawners[carril].transform.position;
           
            MouseBehaviour r = Instantiate(_prefab, spawner, Quaternion.identity);

            r.Init(ProbabilitySpawn[Random.Range(0, ProbabilitySpawn.Count)]);
            _spawned++;
            r.OnEntityDeath += DeadMouse;
            Debug.Log("Ha spawneado una rata");
            quantity--;
        }

        yield return new WaitUntil(AllDead);
        ChangeRound();
    }

    private bool AllDead()
    {
        return _spawned <= 0;
    }

    private void DeadMouse()
    {
        _spawned--;
    }
}