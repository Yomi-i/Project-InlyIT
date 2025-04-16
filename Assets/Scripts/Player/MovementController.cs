using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 40f;

    [SerializeField] private  OnScreenJoystick _joystick;
    [SerializeField] private float _deadzone = 0.2f; // for OnScreen joystick controller.
    

    // References.
    private Rigidbody _rb;
    private Transform _characterTransform;


    // Variables.
    private Vector2 _playerInput;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _characterTransform = transform;
    }

    void FixedUpdate()
    {
        _playerInput = GetInput();

        if (_playerInput.magnitude > _deadzone)
        {
            Move();
            RotateTowardsMovement();
        }
        else StopMovement();
    }

    private Vector2 GetInput()
    {
        
        if (_joystick._InputDir != Vector2.zero)
        {
            return _playerInput = _joystick._InputDir;
        }
        else return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void Move()
    {
        Vector3 movementDirection =  new Vector3(_playerInput.x, 0, _playerInput.y); // Didn't used vector3 from beggining for RotateTowardsMovement, dont forget it.
        _rb.MovePosition(transform.position + movementDirection * _movementSpeed * Time.deltaTime);
    }

    private void RotateTowardsMovement()
    {
        float targetAngle = Mathf.Atan2(-_playerInput.y, _playerInput.x) * Mathf.Rad2Deg;
        float rotation = Mathf.LerpAngle(
            transform.rotation.eulerAngles.y,
            targetAngle,
            _rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    private void StopMovement()
    {
        _rb.velocity = Vector3.zero;
    }
}