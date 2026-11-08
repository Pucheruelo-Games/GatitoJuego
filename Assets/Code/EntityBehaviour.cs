using UnityEngine;

namespace Code
{
    public abstract class EntityBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        protected Entity Entity;
        protected uint CurrentHealth;
        
        public void Init(Entity entity)
        {
            Entity = entity;
            CurrentHealth = entity.Health;
            spriteRenderer.sprite = entity.Sprite;
            OnInit();
        }
        
        protected abstract void OnInit();
        
        public void TakeDamage(uint damage)
        {
            CurrentHealth -= damage;
            if( CurrentHealth <= 0)
            {
                OnDeath();
            }
        }

        protected abstract void OnDeath();
    }
}