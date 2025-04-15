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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput = GetInput();

        if (playerInput.magnitude > deadzone)
        {
            Move(playerInput);
            RotateTowardsMovement(playerInput);
        }
        else StopMovement();
    }

    private Vector2 GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical).normalized;
    }

    private void Move(Vector2 direction)
    {
        Vector3 movementDirection =  new Vector3(direction.x, 0, direction.y); // Didn't used vector3 from beggining for RotateTowardsMovement, dont forget it.
        rb.MovePosition(transform.position + movementDirection * movementSpeed * Time.deltaTime);
    }

    private void RotateTowardsMovement(Vector2 direction)
    {
        if (direction.magnitude > 0)
        {
            float targetAngle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;
            float rotation = Mathf.LerpAngle(
                transform.rotation.eulerAngles.y,
                targetAngle,
                rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    private void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }
}