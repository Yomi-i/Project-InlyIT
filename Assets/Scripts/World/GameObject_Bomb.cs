using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class GameObject_Bomb : MonoBehaviour
{
    // Variables.
    [SerializeField] private float _damageAmount = 10f;
    [SerializeField] private float _followSpeed = 3.5f;
    private Transform _target;


    // Flags.
    private bool bIsFollowing = false;


    void Update()
    {
        if (bIsFollowing) 
        {
            Vector3 targetPosition = _target.position;
            transform.position = Vector3.MoveTowards(
                transform.position, 
                targetPosition, 
                _followSpeed * Time.deltaTime);
        };
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _target = other.gameObject.GetComponent<Transform>();
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);
        bIsFollowing = true;
        }
        
        Debug.Log("Player currently targeted by bomb!");
    }

    void OnTriggerExit(Collider other)
    {
        if (bIsFollowing) bIsFollowing = false;

        Debug.Log("Player evaded the bomb");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttributesController playerHealth = collision.gameObject.GetComponent<AttributesController>();
            if (playerHealth != null) playerHealth.ReduceHealth(_damageAmount);
            Destroy(gameObject);

            if (InteractionLogger.Instance != null) InteractionLogger.Instance.LogInteraction("Player", "Bomb");
            Debug.Log("Damage taken from bomb!");
        }
    }
}