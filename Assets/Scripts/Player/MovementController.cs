using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float deadzone = 0.2f; // for OnScreen joystick controller.
    

    // References.
    private Rigidbody rb;
    private Transform characterTransform;


    // Variables.
    private Vector2 _playerInput;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterTransform = transform;
    }

    void FixedUpdate()
    {
        _playerInput = GetInput();

        if (_playerInput.magnitude > deadzone)
        {
            Move();
            RotateTowardsMovement();
        }
        else StopMovement();
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void Move()
    {
        Vector3 movementDirection =  new Vector3(_playerInput.x, 0, _playerInput.y); // Didn't used vector3 from beggining for RotateTowardsMovement, dont forget it.
        rb.MovePosition(transform.position + movementDirection * movementSpeed * Time.deltaTime);
    }

    private void RotateTowardsMovement()
    {
        float targetAngle = Mathf.Atan2(-_playerInput.y, _playerInput.x) * Mathf.Rad2Deg;
        float rotation = Mathf.LerpAngle(
            transform.rotation.eulerAngles.y,
            targetAngle,
            rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    private void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }
}