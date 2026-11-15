using System;
using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MouseBehaviour : MonoBehaviour, IDamageable
    {
        public AttackTag AttackTag => AttackTag.Mouse;
        [SerializeField] private float health;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float moveSpeed;
        [SerializeField] private int reward;
        [SerializeField] private Attack attack;
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigidbody2D;
        private bool _dead;
        private float _timer;
        private LayerMask _targetLayerMask;
        public Action OnDeath;

        public void TakeDamage(int damage)
        {
            if (_dead) return;
            health -= damage;
            if (health <= 0) Die();
            else if (animator) animator.SetTrigger("Hurt");
        }

        private void FixedUpdate()
        {
            if (_dead) return;
            if (attack.IsEnemyInRange())
            {
                if (animator) animator.SetBool("Walk",false);
                _timer += Time.fixedDeltaTime;
                if (_timer >= attackSpeed)
                {
                    _timer -= attackSpeed;
                    attack.Perform();
                    if (animator) animator.SetTrigger("Attack");
                }
            }
            else
            {
                _timer = 0;
                rigidbody2D.linearVelocityY = -moveSpeed;
                if (animator) animator.SetBool("Walk",true);
            }
        }

        private void Die()
        {
            if (animator) animator.SetTrigger("Dead");
            _dead = true;
            OnDeath?.Invoke();
            GameManager.Instance.Coins += reward;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2);
        }
    }
}