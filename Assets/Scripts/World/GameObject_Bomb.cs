using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class GameObject_Bomb : MonoBehaviour
{
    // Variables,
    [SerializeField] private float _rotationSpeed = 30f;
    [SerializeField] private float _damageAmount = 10f;

    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed = 3.5f;
    

    // Flags.
    private bool bIsFollowing = false;


    void Update()
    {
        if (!bIsFollowing) transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        else
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
        float distanceToTarget = Vector3.Distance(transform.position, _target.position);
        bIsFollowing = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (bIsFollowing) bIsFollowing = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttributesController playerHealth = collision.gameObject.GetComponent<AttributesController>();
            if (playerHealth != null) playerHealth.ReduceHealth(_damageAmount);
            Destroy(gameObject);

            Debug.LogWarning("Damage taken from bomb!");
        }
    }
}