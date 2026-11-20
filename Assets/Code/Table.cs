using System;
using UnityEngine;

namespace Code
{
    public class Table : MonoBehaviour, IDamageable
    {
        public AttackTag AttackTag => AttackTag.Cat;
        [SerializeField] private int health = 10;
        public event Action<int> OnHealthChange;
        private void Start()
        {
            OnHealthChange?.Invoke(health);
        }
        public void TakeDamage(int damage, Effect[] effects)
        {
            health -= damage;
            OnHealthChange?.Invoke(health);
        }
    }

}