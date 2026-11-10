using System;
using UnityEngine;

namespace Code
{
    public class Table : MonoBehaviour, IDamageable
    {
        public event Action<int> OnHealthChange;
        private void Start()
        {
            OnHealthChange?.Invoke(GameManager.Instance.Health);
        }
        public void TakeDamage(int damage)
        {
            GameManager.Instance.Health -= damage;
            OnHealthChange?.Invoke(GameManager.Instance.Health);
        }
    }

}