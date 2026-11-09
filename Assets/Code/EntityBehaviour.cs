using System;
using UnityEngine;

namespace Code
{
    public abstract class EntityBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        protected Entity Entity;
        protected int CurrentHealth;
        public event Action OnEntityDeath;
        
        public void Init(Entity entity)
        {
            Entity = entity;
            CurrentHealth = entity.Health;
            spriteRenderer.sprite = entity.Sprite;
            OnInit();
        }
        
        protected abstract void OnInit();
        
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if( CurrentHealth <= 0)
            {
                OnDeath();
                OnEntityDeath?.Invoke();
            }
        }

        protected abstract void OnDeath();
    }
}