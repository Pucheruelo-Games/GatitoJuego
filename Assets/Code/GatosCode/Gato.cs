using System.Threading;
using UnityEngine;

public class Gato : MonoBehaviour, IDañable
{
    [SerializeField] SpriteRenderer _skin;
    private GatoStats _stats;
    private float _hp;
    private float _timer;

    public void Init(GatoStats stats)
    {
        _stats = stats;
        _hp = stats.hp;
        _skin.sprite = _stats.sprite;
    }

    public void RecibirDmg(float dmg)
    {
        _hp -= dmg;
        if( _hp <= 0)
        {
            Morir();
        }
    }
    private void Morir()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _stats.atkSpeed) return;
        if (!_stats.atkType.PuedeAtacar(this)) return;
        
        _stats.atkType.Atacar(this);
        _timer = 0;
        
    }
}
