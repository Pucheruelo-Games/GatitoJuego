using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _vel = 5f;
    [SerializeField] private float _velDisp = 5f;
    [SerializeField] InputActionReference _mover;
    [SerializeField] float _maxI = -2.75f;
    [SerializeField] float _maxD = 2.75f;
    [SerializeField] GameObject _prefabDisp;
    private Rigidbody2D _rb;
    private Coroutine _disparar;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _mover.action.Enable();
        if (_disparar != null)
        {
            StopCoroutine(_disparar);
        }
        _disparar = StartCoroutine(Atacar());   
    }
    private void OnDisable()
    {
        _mover.action.Disable();
        if (_disparar != null)
        {
            StopCoroutine(_disparar);
            _disparar = null;
        }
    }
    private void Update()
    {
        float timer = Time.deltaTime;
        float dir = _mover.action.ReadValue<Vector2>().x;
        float pos = transform.position.x;

        if ((pos <= _maxI &&dir<0)||(  pos >= _maxD&&dir>0))
        {
            dir = 0;
        }
        _rb.linearVelocityX = dir * _vel;
        
    }
    private IEnumerator Atacar()
    {
        while (true)
        {
            yield return new WaitForSeconds(_velDisp);
            Instantiate(_prefabDisp, transform.position, Quaternion.identity);
        }
        
    }
}
