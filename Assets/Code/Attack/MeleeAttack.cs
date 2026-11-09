using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "Attack/MeleeAttack", order = 0)]
    public class MeleeAttack : Attack
    {
        [SerializeField] private int damage;
        
        public override bool IsEnemyInRange(AttackContext context, out IDamageable target)
        {
            var hit = Physics2D.Raycast(context.SourcePosition, context.Direction, 1, context.TargetLayerMask);
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
        public override void Hit(AttackContext context)
        {
            if (IsEnemyInRange(context, out var target))
                target.TakeDamage(damage);
        }
    }
}