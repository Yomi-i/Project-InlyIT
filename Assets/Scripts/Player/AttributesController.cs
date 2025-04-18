using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesController : MonoBehaviour
{
    [SerializeField] private MovementController _movementController;

    // Variables.
    [SerializeField] private float _playerHealth = 100f;
    [SerializeField] private float _playerHealthMax = 100f;
    [Header("Player respawn")]
    [SerializeField] private float _respawnDelay = 3f;
    [SerializeField]private Vector3 _spawnPoint;
    private float _playerMovementMult;
    private float _boosterDuration;
    private float _nonAffectedMoveSpeed;


    // Flags.
    private bool bIsAffected = false;
    private bool bIsDead = false;

    
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
        
        if (_playerHealth <= 0) 
        {
            bIsDead = true;
            OnDeath();
        }
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
        if (bIsDead)
        {   
            gameObject.GetComponentInParent<Collider>().enabled = false;
            _movementController.bIsInputAllowed = false;
            gameObject.transform.Rotate(0f, 0f, 90f);
            UIManager.Instance.ShowOnDeathScreen();
            StartCoroutine(OnRespawn());
        }
    }
    
    private IEnumerator OnRespawn()
    {
        yield return new WaitForSeconds(_respawnDelay);
        transform.position = _spawnPoint;
        gameObject.transform.Rotate(0f, 0f, -90f);
        _playerHealth = _playerHealthMax;
        gameObject.GetComponentInParent<Collider>().enabled = true;
        _movementController.bIsInputAllowed = true;
        bIsDead = false;
        UIManager.Instance.OnRespawn();
    }
}