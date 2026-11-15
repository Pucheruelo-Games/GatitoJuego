using System;
using System.Threading;
using Code;
using UnityEngine;

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
        private float _timer;
        private Tile[] _tiles;

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
        }

        private void FixedUpdate()
        {
            if (attack.IsEnemyInRange())
            {
                _timer += Time.fixedDeltaTime;
                if (_timer >= attackSpeed)
                {
                    _timer -= attackSpeed;
                    attack.Perform();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0) Die();
        }
        
        private void Die()
        {
            foreach (var tile in _tiles) tile.Cat = null;
            Destroy(this.gameObject);
        }
    }
}