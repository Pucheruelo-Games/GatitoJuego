using System;
using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MouseBehaviour : EntityBehaviour
    {
        private MouseEntity _mouseEntity;
        private float _timer;
        private Rigidbody2D _rigidbody2D;
        private AttackContext _context;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Debug.Assert(_rigidbody2D,"MouseBehaviour needs a Rigidbody2D");
        }

        protected override void OnInit()
        {
            if (Entity is not MouseEntity mouse) throw new Exception("Entity is not MouseEntity");
            _mouseEntity = mouse;
            _context = new AttackContext(transform.position, Vector2.down, LayerMask.NameToLayer("Cat"));
        }
        
        private void FixedUpdate()
        {
            if (!Entity.Attack.IsEnemyInRange(_context))
            {
                _rigidbody2D.linearVelocity = _mouseEntity.Speed * Vector2.down;
            }
            else
            {
                _rigidbody2D.linearVelocity = Vector2.zero;
                _timer += Time.fixedDeltaTime;
                if (_timer < Entity.AttackSpeed) return;
                
                Entity.Attack.Hit(_context);
                _timer= 0;
            }
        }

        protected override void OnDeath()
        {
            GameManager.Singleton.Coins += _mouseEntity.Reward;
            Destroy(gameObject);
        }
    }
}