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
        [SerializeField] float maxDistanceLeft = -2.75f;
        [SerializeField] float maxDistanceRight = 2.75f;
        [SerializeField] Attack attack;
        private Rigidbody2D _rigidbody2D;
        private float _attackCooldown;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Debug.Assert(_rigidbody2D,$"{this} Rigidbody2D object not found");
        }
        private void Update()
        {
            if (Inventory.Instance.CarriedCard) return;
            var dir = InputManager.Instance.Player.Move.ReadValue<Vector2>().x;
            var pos = transform.position.x;

            if ((pos <= maxDistanceLeft && dir < 0) || ( pos >= maxDistanceRight && dir > 0))
            {
                dir = 0;
            }
            _rigidbody2D.linearVelocityX = dir * speed;
        }

        private void FixedUpdate()
        {
            _attackCooldown += Time.fixedDeltaTime;
            if (_attackCooldown > attackSpeed && attack.IsEnemyInRange())
            {
                attack.Perform();
                _attackCooldown = 0f;
            }
        }
    }
}