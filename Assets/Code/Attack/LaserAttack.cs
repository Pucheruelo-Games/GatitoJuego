using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class LaserAttack : Attack
    {
        [SerializeField] private LaserProjectile projectile;
        [SerializeField] private Vector2 direction;
        [SerializeField] private Transform origin;
        [SerializeField] private int length;

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
            StartCoroutine(Laser());
            
        }
        private IEnumerator Laser()
        {
            HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();
            for (int i = 0; i < length; i++)
            {
                var newProjectile = Instantiate(projectile);
                newProjectile.transform.position = origin.position;
                newProjectile.Init(direction, TargetTag, damagedTargets);
                yield return new WaitForSeconds(0.1f);
            }
            
        }
        
        private void OnDrawGizmos()
        {
            if (!origin) return;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(origin.position, direction * projectile.Range);
        }
    }
}