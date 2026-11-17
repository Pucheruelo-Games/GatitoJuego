using Code;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class LaserProjectile : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float velocity;
        [SerializeField] private float range;
        public int Damage => damage;
        public float Range => range;
        [SerializeField] private Rigidbody2D rigidbody2D;
        private Vector2 _initialPosition;
        private Vector2 _direction;
        private AttackTag _targetTag;
        private HashSet<IDamageable> damagedTargets;
        public void Init(Vector2 direction, AttackTag targetTag, HashSet<IDamageable> damageds)
        {
            _direction = direction;
            _targetTag = targetTag;
            damagedTargets = damageds;
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
            if(!damagedTargets.Add(damageable)) return;
            damageable.TakeDamage(damage);
        }

        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}
