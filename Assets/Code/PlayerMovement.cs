using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float attackSpeed = 5f;
        [SerializeField] InputActionReference move;
        [SerializeField] float maxDistanceLeft = -2.75f;
        [SerializeField] float maxDistanceRight = 2.75f;
        [SerializeField] Attack attack;
        private Rigidbody2D _rigidbody2D;
        private LayerMask _targetLayerMask;
        private float _attackCooldown;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Debug.Assert(_rigidbody2D,$"{this} Rigidbody2D object not found");
            _targetLayerMask =  LayerMask.GetMask("Mouse");
        }
        private void OnEnable()
        {
            move.action.Enable();
        }
        private void OnDisable()
        {
            move.action.Disable();
        }
        private void Update()
        {
            float timer = Time.deltaTime;
            float dir = move.action.ReadValue<Vector2>().x;
            float pos = transform.position.x;

            if ((pos <= maxDistanceLeft &&dir<0)||(  pos >= maxDistanceRight&&dir>0))
            {
                dir = 0;
            }
            _rigidbody2D.linearVelocityX = dir * speed;
        
        }

        private void FixedUpdate()
        {
            var context = new AttackContext(transform.position, Vector2.up, _targetLayerMask, null);
            _attackCooldown += Time.fixedDeltaTime;
            if (_attackCooldown > attackSpeed)
            {
                attack.Hit(context);
                _attackCooldown = 0;
            }
        }
    }
}