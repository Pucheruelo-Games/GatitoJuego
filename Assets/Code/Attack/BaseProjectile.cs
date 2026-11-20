using System;
using UnityEngine;

namespace Code
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float velocity;
        [SerializeField] private float range;
        [SerializeField] private Effect[] effects;
        public int Damage => damage;
        public float Range => range;
        [SerializeField] private Rigidbody2D rigidbody2D;
        private Vector2 _initialPosition;
        private Vector2 _direction;
        private AttackTag _targetTag;

        public void Init(Vector2 direction, AttackTag targetTag)
        {
            _direction = direction;
            _targetTag = targetTag;
            _initialPosition = transform.position;
        }
        
        private void FixedUpdate()
        {
            rigidbody2D.linearVelocity = velocity * _direction;
            if (Vector2.Distance(transform.position, _initialPosition) >= range) Despawn();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            if (damageable.AttackTag != _targetTag) return;

            damageable.TakeDamage(damage, effects);
            Despawn();
        }
        
        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}