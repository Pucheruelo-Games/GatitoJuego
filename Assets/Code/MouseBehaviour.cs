using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MouseBehaviour : MonoBehaviour, IDamageable, IComparable<MouseBehaviour>
    {
        public AttackTag AttackTag => AttackTag.Mouse;
        [SerializeField] private float health;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float moveSpeed;
        [SerializeField] private int reward;
        [SerializeField] private Attack attack;
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private int difficulty;
        private List<Effect> activeEffects = new List<Effect>();
        public int Difficulty => difficulty;
        private bool _dead;
        private float _timer;
        public Action OnDeath;
        private float _poisonTick;
        public void TakeDamage(int damage, Effect[] effects = null)
        {
            if (_dead) return;
            health -= damage;
            if (health <= 0) Die();
            else if (animator) animator.SetTrigger("Hurt");
            if (effects == null) return;
            foreach(Effect ef in effects)
            {
                switch (ef.effectType)
                {
                    case (EffectType.KnockBack):
                        rigidbody2D.AddForceY(ef.level, ForceMode2D.Impulse);
                        break;
                    case (EffectType.Poison):
                        activeEffects.Add(new Effect(ef.effectType, ef.duration, ef.level));
                        break;
                    case (EffectType.Slowness):
                        activeEffects.Add(new Effect(ef.effectType, ef.duration, ef.level));
                        break;
                }
            }
            activeEffects.Sort();
        }
        private bool HasActiveEffects(EffectType ef ,out int lvl)
        {
            foreach(Effect efec in activeEffects)
            {
                if (efec.effectType == ef)
                {
                    lvl = efec.level;
                    return true;
                }
            }
            lvl = 0;
            return false;
        }

        private void FixedUpdate()
        {
            if (_dead) return;
            List<Effect> newListEff = new List<Effect>(activeEffects);
            foreach (Effect ef in newListEff)
            {
                ef.duration -= Time.fixedDeltaTime;
                if (ef.duration <= 0)
                {
                    activeEffects.Remove(ef);
                }
            }
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
                if (HasActiveEffects(EffectType.Slowness, out int slow))
                {
                    float caluclo = moveSpeed / slow;
                    if (caluclo <= 0)
                    {
                        caluclo = 0;
                    }
                    rigidbody2D.AddForceY(-caluclo, ForceMode2D.Force);
                }
                else
                {
                    rigidbody2D.AddForceY(-moveSpeed, ForceMode2D.Force);
                }
                if (animator) animator.SetBool("Walk", true);
                _timer = 0;
            }
           
            
            if (HasActiveEffects(EffectType.Poison, out int lvl) && _poisonTick<=0)
            { 
                TakeDamage(lvl);
                _poisonTick = 1;
            }
            else
            {
                _poisonTick -= Time.fixedDeltaTime;
            }
        }

        private void Die()
        {
            if (animator) animator.SetTrigger("Dead");
            _dead = true;
            OnDeath?.Invoke();
            GameManager.Instance.Coins += reward;
            if (animator)
            {
                Destroy(GetComponent<Collider2D>()); 
                Destroy(this.gameObject, 2);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public int CompareTo(MouseBehaviour other)
        {
            return other.difficulty.CompareTo(difficulty);
        }
    }
}