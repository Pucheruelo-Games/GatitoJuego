using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    [SerializeField] Raton _prefab;
    [SerializeField] RatonStats[] _tipos;
    [SerializeField] GameObject[] _spawners;
    [SerializeField] Monedas _monedas;
    [SerializeField] float _intervalo = 5f;
    private float _timer;
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _intervalo) return;
        Spawnear();
    }

    private void Spawnear()
    {
        _timer = 0;

        int carril = Random.Range(0, 6);
        Vector2 spawner = _spawners[carril].transform.position;
        Raton r = Instantiate(_prefab, spawner, Quaternion.identity);
        r.Init(_tipos[Random.Range(0, _tipos.Length)]);
        r.Muelto += _monedas.GanarMonedas;
        Debug.Log("Ha spawneado una rata");
    }
}
