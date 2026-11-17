using UnityEngine;

namespace Code
{
    public interface IDamageable
    {
        public void TakeDamage(int damage, Effect[] effects = null);
        public AttackTag AttackTag { get; }
    }
}