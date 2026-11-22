using Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

namespace Code
{
    [RequireComponent(typeof(Attack))]
    public class CatBehaviour : MonoBehaviour, IDamageable
    {
        public AttackTag AttackTag => AttackTag.Cat;
        [SerializeField] private float health;
        [SerializeField] private float attackSpeed;
        [SerializeField] private Attack attack;
        [SerializeField] private Animator animator;
        [SerializeField] private VisualEffect[] Particles;
        private float _timer;
        private Tile[] _tiles;
        private List<Effect> activeEffects = new List<Effect>();
        private float _poisonTick;

        public void Init(params Tile[] tiles)
        {
            var position = Vector3.zero;
            _tiles = tiles;
            foreach (var tile in _tiles)
            {
                tile.Cat = this;
                position += tile.transform.position;
            }
            position /= _tiles.Length;
            position.y += 0.2f;
            transform.position = position;
            if (animator) animator.SetTrigger("Attack");
            if (Particles.Length > 0)
            {
                Particles[0].Play();
            }
        }

        private void FixedUpdate()
        {
            if (attack != null)
            {
                if (attack.IsEnemyInRange())
                {
                    _timer += Time.fixedDeltaTime;
                    if (_timer >= attackSpeed)
                    {
                        _timer -= attackSpeed;
                        attack.Perform();
                        if (animator) animator.SetTrigger("Attack");
                    }
                }
            }
            
            List<Effect> newListEff = new List<Effect>(activeEffects);
            foreach (Effect ef in newListEff)
            {
                ef.duration -= Time.fixedDeltaTime;
                if (ef.duration <= 0)
                {
                    activeEffects.Remove(ef);
                }
            }

            if (HasActiveEffects(EffectType.Poison, out int lvl) && _poisonTick <= 0)
            {
                TakeDamage(lvl);
                _poisonTick = 1;
            }
            else
            {
                _poisonTick -= Time.fixedDeltaTime;
            }
        }

        public void TakeDamage(int damage, Effect[] effects = null)
        {
            health -= damage;
            if (health <= 0) Die();
            else if (animator) animator.SetTrigger("Hurt");
            if (effects == null) return;
            foreach (Effect ef in effects)
            {
                switch (ef.effectType)
                {
                    case (EffectType.KnockBack):
                        Debug.Log("Un gato no puede ser knockbacked");
                        break;
                    case (EffectType.Poison):
                        activeEffects.Add(new Effect(ef.effectType, ef.duration, ef.level));
                        break;
                    case (EffectType.Slowness):
                        break;
                }
            }
            activeEffects.Sort();
        }
        private bool HasActiveEffects(EffectType ef, out int lvl)
        {
            foreach (Effect efec in activeEffects)
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

        private void Die()
        {
            foreach (var tile in _tiles) tile.Cat = null;
            if (animator) animator.SetTrigger("Dead");
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
    }
}