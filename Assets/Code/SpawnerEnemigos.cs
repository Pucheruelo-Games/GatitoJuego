using System.Collections;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    [SerializeField] Raton _prefab;
    [SerializeField] RatonStats[] _tipos;
    [SerializeField] GameObject[] _spawners;
    [SerializeField] RoundManager _roundManager;
    [SerializeField] Monedas _monedas;
    private int _spawneados;
    public IEnumerator SpawnearEnemigosDeRonda(int cantidad, float intervalo, int ronda)
    {
        while (cantidad > 0)
        {
            float var = Random.Range(-0.5f, 0.5f);
            float espera = Mathf.Max(0.1f, intervalo + var);
            yield return new WaitForSeconds(espera);

            int carril = Random.Range(0, 6);
            Vector2 spawner = _spawners[carril].transform.position;
            Raton r = Instantiate(_prefab, spawner, Quaternion.identity);

            r.Init(_tipos[Random.Range(0, _tipos.Length)]);
            _spawneados++;
            r.Muelto += _monedas.GanarMonedas;
            r.Muelto += RatonMuelto;
            Debug.Log("Ha spawneado una rata");
            cantidad--;
        }
        yield return new WaitUntil(TodosMuertos);
        _roundManager.CambiarRonda();
    }
    private bool TodosMuertos()
    {
        return _spawneados <= 0;
    }
    private void RatonMuelto(int xd)
    {
        _spawneados--;
    }
}
