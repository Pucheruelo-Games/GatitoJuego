using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _vel = 5f;
    [SerializeField] InputActionReference _mover;
    [SerializeField] float _maxI = -5f;
    [SerializeField] float _maxD = 5f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _mover.action.Enable();
        
    }
    private void OnDisable()
    {
        _mover.action.Disable();
    }
    private void Update()
    {
        float dir = _mover.action.ReadValue<Vector2>().x;
        float pos = transform.position.x;
        if ((pos <= _maxI &&dir<0)||(  pos >= _maxD&&dir>0))
        {
            dir = 0;
        }
        _rb.linearVelocityX = dir * _vel;
        
    }
}
