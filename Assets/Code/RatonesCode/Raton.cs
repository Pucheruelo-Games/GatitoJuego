using UnityEngine;

public class Raton : MonoBehaviour, IDañable
{
    [SerializeField] SpriteRenderer _skin;
    private RatonStats _stats;
    private float _hp;
    private float _timer;
    private IDañable _objetivo;
    public void Init(RatonStats stats)
    {
        _stats = stats;
        _hp = _stats.hp;
        _skin.sprite = stats.sprite;
    }
    private void Update()
    {
        if( _objetivo == null)
        {
            transform.Translate(Vector2.down * _stats.vel * Time.deltaTime);
            return;
        }
        _timer += Time.deltaTime;
        if(_timer >= _stats.atkSpeed)
        {
            _objetivo.RecibirDmg(_stats.dmg);
            _timer= 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gato")||collision.CompareTag("Mesa"))
        {
            _objetivo = collision.GetComponent<IDañable>();
        }
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
}
