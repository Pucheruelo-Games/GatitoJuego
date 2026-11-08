using System;
using UnityEngine;

public abstract class ProyectilBase : MonoBehaviour
{
    [SerializeField] protected float _dmg;
    [SerializeField] protected float _vel;
    [SerializeField] protected float _alcance;
    public float alcance => _alcance;

    protected virtual void Start()
    {
        Destroy(gameObject, _alcance);
    }
    protected virtual void Update()
    {
        Mover();
    }
    protected virtual void Mover()
    {
        transform.Translate(Vector2.up * _vel * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer("Raton")) return;
        if (!collider.TryGetComponent(out IDañable obj)) return;

        AlAtacar(collider);
        Destroy(gameObject);
    }

    protected abstract void AlAtacar(Collider2D obj);
}
