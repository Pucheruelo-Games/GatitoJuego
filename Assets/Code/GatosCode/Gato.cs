using System.Threading;
using UnityEngine;

public class Gato : MonoBehaviour
{
    [SerializeField] SpriteRenderer _skin;
    private GatoStats _stats;
    private float _timer;

    public void Init(GatoStats stats)
    {
        _stats = stats;
        _skin.sprite = _stats.sprite;
    }
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _stats.atkSpeed)
        {
            _stats.atkType.Atacar(this);
            _timer = 0;
        }
    }
}
