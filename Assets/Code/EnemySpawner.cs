using Code;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MouseBehaviour mousePrefab;
    [SerializeField] private MouseEntity[] mice;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float interval = 5f;
    private float _timer;
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < interval) return;
        Spawn();
    }

    private void Spawn()
    {
        _timer = 0;
        var path = Random.Range(0, 6);
        Vector2 spawnPoint = spawnPoints[path].transform.position;
        var mouse = Instantiate(mousePrefab, spawnPoint, Quaternion.identity);
        mouse.Init(mice[Random.Range(0, mice.Length)]);
    }
}
