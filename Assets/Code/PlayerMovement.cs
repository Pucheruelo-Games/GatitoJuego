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
        [SerializeField] Animator animator;
        private Rigidbody2D _rigidbody2D;
        private float _attackCooldown;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Debug.Assert(_rigidbody2D,$"{this} Rigidbody2D object not found");
        }
        private void Update()
        {
            var pos = _rigidbody2D.position;
            
            var dir = InputManager.Instance.Player.Move.ReadValue<Vector2>().x;

            if (dir != 0f)
            {
                pos.x += dir * speed * Time.deltaTime;
            }
            else
            {
                if (Inventory.Instance.CarriedCard) return;
                if (!InputManager.Instance.Player.Click.IsPressed()) return;
                
                var canvasPos = InputManager.Instance.Player.Position.ReadValue<Vector2>();
                var worldPos = Camera.main.ScreenToWorldPoint(canvasPos) * Vector2.one;
                
                if (Mathf.Abs(worldPos.y - transform.position.y) > 1f) return;
                
                pos.x = worldPos.x;
            }
            
            if (pos.x <= maxDistanceLeft)
            {
                _rigidbody2D.position = new Vector3(maxDistanceLeft, transform.position.y, transform.position.z);
            }
            else if (pos.x >= maxDistanceRight)
            {
                _rigidbody2D.position = new Vector3(maxDistanceRight, transform.position.y, transform.position.z);
            }
            else
            {
                _rigidbody2D.position = new Vector3(pos.x, transform.position.y, transform.position.z);
            }
        }

        private void FixedUpdate()
        {
            if (attack)
            {
                if (_attackCooldown <= 0f)
                {
                    if (attack.IsEnemyInRange())
                    {
                        _attackCooldown = attackSpeed;
                        attack.Perform();
                        if (animator) animator.SetTrigger("Attack");
                    }
                }
                else
                {
                    _attackCooldown -= Time.fixedDeltaTime;
                }
            }
        }
    }
}