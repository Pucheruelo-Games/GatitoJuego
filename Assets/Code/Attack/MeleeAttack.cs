using System;
using UnityEngine;

namespace Code
{
    public class MeleeAttack : Attack
    {
        [SerializeField] private Vector2 direction;
        [SerializeField] private int damage;
        [SerializeField] private Transform origin;
        [SerializeField] private float range;
        [SerializeField] private Effect[] effects;

        public override bool IsEnemyInRange(out IDamageable target)
        {
            foreach (var hit in Physics2D.RaycastAll(origin.position, direction, range))
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable) && damageable.AttackTag == TargetTag)
                {
                    target = damageable;
                    return true;
                }
            }
            target = null;
            return false;
        }

        public override void Perform()
        {
            if (IsEnemyInRange(out IDamageable target))
                target.TakeDamage(damage, effects);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(origin.position, direction * range);
        }
    }
}