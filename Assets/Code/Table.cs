using System;
using UnityEngine;

namespace Code
{
    public class Table : MonoBehaviour, IDamageable
    {
        [SerializeField] private int health = 10;

        public event Action<int> OnHealthChange;
        private void Start()
        {
            OnHealthChange?.Invoke(health);
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
            OnHealthChange?.Invoke(health);
        }
    }

}