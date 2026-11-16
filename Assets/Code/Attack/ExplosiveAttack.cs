using UnityEngine;

namespace Code
{

    public class ExplosiveAttack : Attack
    {
        [SerializeField] private Vector2 direction;
        [SerializeField] private int damage;
        [SerializeField] private Transform origin;
        [SerializeField] private float range;
        [SerializeField] private float ExplosionRadius;

        
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
            {
                foreach (var hit in Physics2D.CircleCastAll(origin.position, ExplosionRadius, direction))
                {
                    if (hit.collider.TryGetComponent(out IDamageable damageable) && damageable.AttackTag == TargetTag)
                    {
                        damageable.TakeDamage(damage);
                    }
                }
            }
            if(this.TryGetComponent(out MouseBehaviour mouseBehaviour))
            {
                mouseBehaviour.TakeDamage(999999);
            }
                
        }
    }
}