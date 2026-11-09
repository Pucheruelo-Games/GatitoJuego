using UnityEngine;

namespace Code
{
    public abstract class Attack : ScriptableObject
    {
        public abstract void Hit(AttackContext attackContext);

        public bool IsEnemyInRange(AttackContext attackContext) => IsEnemyInRange(attackContext, out var target);
        public abstract bool IsEnemyInRange(AttackContext attackContext, out IDamageable target);
    }
}