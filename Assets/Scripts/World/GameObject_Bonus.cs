using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject_Bonus : MonoBehaviour
{
    [SerializeField] private float _bonusDuration = 5f;
    [SerializeField] private float _movementMultiplier = 1.5f;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttributesController playerAttributes = collision.gameObject.GetComponent<AttributesController>();
            playerAttributes.AffectMovement(_movementMultiplier, _bonusDuration);
            Destroy(gameObject);
            
            if (InteractionLogger.Instance != null) InteractionLogger.Instance.LogInteraction("Player", "Bonus");
            Debug.Log("Bonus taken!");
        }
    }
}
