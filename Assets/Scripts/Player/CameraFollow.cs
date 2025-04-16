using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 4f;
    [SerializeField] private Vector3 _offset = new Vector3(0, 10, -10);
    void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            _smoothSpeed * Time.deltaTime);
        
        transform.LookAt(_target);
    }
}
