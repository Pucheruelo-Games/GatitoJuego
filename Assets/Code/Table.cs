using System;
using UnityEngine;

namespace Code
{
    public class Table : MonoBehaviour, IDamageable
    {
        [SerializeField] private uint health = 10;

        public event Action<uint> OnHealthChange;
        private void Start()
        {
            health = health;
            OnHealthChange?.Invoke(health);
        }
        public void TakeDamage(uint damage)
        {
            health -= damage;
            OnHealthChange?.Invoke(health);
        }
    }

}