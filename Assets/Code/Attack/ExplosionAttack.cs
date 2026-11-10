using Code;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "ExplosionAttack", menuName = "Attack/ExplosionAttack", order = 3)]
    public class ExplosionAttack : Attack
    {
        [SerializeField] private int damage;
        public override void Hit(AttackContext attackContext)
        {
            if (IsEnemyInRange(attackContext, out var target))
            {
                var Hits = Physics2D.OverlapBoxAll(attackContext.SourcePosition, 2 * Vector2.one, 0, attackContext.TargetLayerMask);
                foreach(var hit in Hits)
                {
                    if(hit.TryGetComponent(out IDamageable d))
                    {
                        d.TakeDamage(damage);
                    }
                }
                Destroy(attackContext.Behaviour.gameObject);
            }
                
        }

        public override bool IsEnemyInRange(AttackContext context, out IDamageable target)
        {
            var hit = Physics2D.Raycast(context.SourcePosition, context.Direction, 0.1f, context.TargetLayerMask);
            if (!hit)
            {
                target = null;
                return false;
            }
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                target = damageable;
                return true;
            }

            target = null;
            return false;
        }

    }
}