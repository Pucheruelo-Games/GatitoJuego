using System;
using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BaseProjectile : MonoBehaviour
    {
        private int _damage;
        private float _velocity;
        private Vector2 _direction;
        private float _range;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _initialPosition;
        private LayerMask _targetLayerMask;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Debug.Assert(_rigidbody2D,"Rigidbody2D not found");
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Debug.Assert(_spriteRenderer,"SpriteRenderer not found");
        }

        public void Init(Vector2 direction, int damage, float velocity, float range, LayerMask targetLayerMask, Sprite sprite)
        {
            _direction = direction;
            _damage = damage;
            _velocity = velocity;
            _range = range;
            _targetLayerMask = targetLayerMask;
            _initialPosition = _rigidbody2D.position;
            _spriteRenderer.sprite = sprite;
        }
        
        private void FixedUpdate()
        {
            _rigidbody2D.linearVelocity = _velocity * _direction;
            if (Vector2.Distance(transform.position, _initialPosition) >= _range) Despawn();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            if ((_targetLayerMask.value & (1 << other.gameObject.layer)) == 0) return;

            
            damageable.TakeDamage(_damage);
            Despawn();
        }
        
        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}