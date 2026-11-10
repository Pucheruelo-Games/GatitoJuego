using System.Threading;
using Code;
using UnityEngine;

namespace Code
{
    public class CatBehaviour : EntityBehaviour
    {
        private float _timer;
        private AttackContext _context;

        protected override void OnInit()
        {
            _context = new AttackContext(transform.position, Vector3.up, LayerMask.NameToLayer("Mouse"), this);
        }

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer < Entity.AttackSpeed) return;
            if (!Entity.Attack.IsEnemyInRange(_context, out IDamageable a)) return;
        
            Entity.Attack.Hit(_context);
            _timer = 0;
        }
    }
}