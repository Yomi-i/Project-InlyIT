using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject_HealthPack : MonoBehaviour
{
    [SerializeField] private float _healthAmount = -10f; // basically hitting to heal.

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttributesController playerHealth = collision.gameObject.GetComponent<AttributesController>();
            if (playerHealth != null) playerHealth.ReduceHealth(_healthAmount);
            Destroy(gameObject);

            Debug.Log("Health taken from health pack!");
        }
    }
}
