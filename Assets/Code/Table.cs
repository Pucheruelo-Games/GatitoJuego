using System;
using UnityEngine;

namespace Code
{
    public class Table : MonoBehaviour, IDamageable
    {
        public AttackTag AttackTag => AttackTag.Cat;
        
        public void TakeDamage(int damage, Effect[] effects)
        {
            GameManager.Instance.Health -= damage;
        }
    }

}