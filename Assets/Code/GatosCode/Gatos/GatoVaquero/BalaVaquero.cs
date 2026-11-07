using UnityEngine;

public class BalaVaquero : MonoBehaviour
{
    [SerializeField] float _dmg;
    [SerializeField] float _speed;
    [SerializeField] float _tiempo;
    [SerializeField] Sprite _sprite;
    [SerializeField] SpriteRenderer _spriteRenderer;
    public float GetTiempo()
    {
        return _tiempo;
    }
    private void Start()
    {
        Destroy(gameObject, _tiempo);
    }
    private void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
