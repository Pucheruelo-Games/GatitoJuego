using UnityEngine;

namespace Code
{
    public class ShootAttack : Attack
    {
        [SerializeField] private BaseProjectile projectile;
        [SerializeField] private Vector2 direction;
        [SerializeField] private Transform origin;
        
        public override bool IsEnemyInRange(out IDamageable target)
        {
            foreach (var hit in Physics2D.RaycastAll(origin.position, direction, projectile.Range))
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
            var newProjectile = Instantiate(projectile);
            newProjectile.transform.position = origin.position;
            newProjectile.Init(direction, TargetTag);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(origin.position, direction * projectile.Range);
        }
    }
}