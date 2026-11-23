using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

namespace Code
{

    public class ExplosiveAttack : Attack
    {
        [SerializeField] private Vector2 direction;
        [SerializeField] private int damage;
        [SerializeField] private Transform origin;
        [SerializeField] private float range;
        [SerializeField] private float ExplosionRadius;
        [SerializeField] protected VisualEffect[] Particles;

        
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
            foreach (var hit in Physics2D.OverlapCircleAll(origin.position, ExplosionRadius))
            {
                if (hit.TryGetComponent(out IDamageable damageable) && damageable.AttackTag == TargetTag)
                {
                    damageable.TakeDamage(damage);

                }
            }
            if (Particles.Length > 0)
            {
                Particles[0].Play();
            }
            if (this.TryGetComponent(out MouseBehaviour mouseBehaviour))
            {
                mouseBehaviour.TakeDamage(999999);
            }
                
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(origin.position, ExplosionRadius);
        }
    }
}