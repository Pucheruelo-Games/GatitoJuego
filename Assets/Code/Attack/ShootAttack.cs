using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "ShootAttack", menuName = "Attack/ShootAttack", order = 0)]
    public class ShootAttack : Attack
    {
        [SerializeField] BaseProjectile projectile;
        [SerializeField] private uint damage;
        [SerializeField] private float velocity;
        [SerializeField] private float range;
        [SerializeField] private Sprite sprite;
        
        public override bool IsEnemyInRange(AttackContext context, out IDamageable target)
        {
            var hit = Physics2D.Raycast(context.SourcePosition, context.Direction, range, context.TargetLayerMask);
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
            var newProjectile = Instantiate(projectile, context.SourcePosition, Quaternion.identity);
            newProjectile.Init(context.Direction, damage, velocity, range, context.TargetLayerMask);
        }   
    }
}