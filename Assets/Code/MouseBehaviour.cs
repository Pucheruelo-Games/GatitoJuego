using System;
using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MouseBehaviour : EntityBehaviour
    {
        private MouseData _mouseData;
        public MouseData MouseData => _mouseData;
        private float _timer;
        private Rigidbody2D _rigidbody2D;
        private LayerMask _targetLayerMask;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Debug.Assert(_rigidbody2D,"MouseBehaviour needs a Rigidbody2D");
        }

        protected override void OnInit()
        {
            if (Entity is not MouseData mouse) throw new Exception("Entity is not MouseEntity");
            _mouseData = mouse;
            _targetLayerMask = LayerMask.GetMask("Cat");
        }
        
        private void FixedUpdate()
        {
            var context = new AttackContext(transform.position, Vector2.down, _targetLayerMask, this);
            
            if (!Entity.Attack.IsEnemyInRange(context))
            {
                _rigidbody2D.linearVelocity = _mouseData.Speed * Vector2.down;
            }
            else
            {
                _rigidbody2D.linearVelocity = Vector2.zero;
                _timer += Time.fixedDeltaTime;
                if (_timer < Entity.AttackSpeed) return;
                
                Entity.Attack.Hit(context);
                _timer= 0;
            }
        }

        protected override void OnDeath()
        {
            GameManager.Instance.Coins += _mouseData.Reward;
            Destroy(gameObject);
        }
    }
}