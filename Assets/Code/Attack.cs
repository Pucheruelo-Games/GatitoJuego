using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Code
{
    public abstract class Attack : MonoBehaviour
    {
        [field:NonSerialized] public float AtkMulti = 1;
        [field:SerializeField] protected AttackTag TargetTag { get; private set; }
        public abstract bool IsEnemyInRange(out IDamageable target);
        public bool IsEnemyInRange() => IsEnemyInRange(out IDamageable target);
        public abstract void Perform();
        
    }
}