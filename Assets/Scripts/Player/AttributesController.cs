using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesController : MonoBehaviour
{
    [SerializeField] private MovementController _movementController;

    // Variables.
    [SerializeField] private float _playerHealth = 100f;
    [SerializeField] private float _playerHealthMax = 100f;
    private float _playerMovementMult;
    private float _boosterDuration;
    private float _nonAffectedMoveSpeed;


    // Flags.
    private bool bIsAffected = false;

    
    void Start()
    {
        _playerHealth = _playerHealthMax;
    }

    void FixedUpdate()
    {
        if (bIsAffected)
        {
            _boosterDuration -= Time.deltaTime;
            if (_boosterDuration <= 0)
            {
                EndAffectingMovement();
            }
        }
    }

    public void ReduceHealth(float amount)
    {
        _playerHealth -= amount;
        if (_playerHealth > _playerHealthMax) _playerHealth = _playerHealthMax;
        Debug.Log("Player health: " +_playerHealth);
        
        if (_playerHealth <= 0) OnDeath();
    }

    public void AffectMovement(float amount, float duration)
    {
        if (bIsAffected)
        {
            _boosterDuration += duration;

            Debug.Log("Movement increase prolonged, last duration: " + _boosterDuration);
        }
        else
        {
        _playerMovementMult = amount;
        _boosterDuration = duration;
        _nonAffectedMoveSpeed = _movementController._movementSpeed;
        _movementController._movementSpeed *= _playerMovementMult;
        bIsAffected = true;
        }

        Debug.Log("Movement multiplied: "+ _playerMovementMult + " for " + _boosterDuration);
    }

    private void EndAffectingMovement()
    {
        bIsAffected = false;
        _boosterDuration = 0f;
        _movementController._movementSpeed = _nonAffectedMoveSpeed;

        Debug.Log("Movement multiplier ended");
    }

    private void OnDeath()
    {
        Destroy(gameObject);
        InteractionLogger.Instance.ClearLogFile();
        // TODO: show end game screen with retry;
    }
}