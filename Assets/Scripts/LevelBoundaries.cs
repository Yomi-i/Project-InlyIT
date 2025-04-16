using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundares : MonoBehaviour
{
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;

    // References.
    private Transform _characterTransform;

    // Start is called before the first frame update
    void Start()
    {
        _characterTransform = transform;
    }

    void LateUpdate()
    {
        Vector3 currentPosition = _characterTransform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, _minX, _maxX);
        currentPosition.z = Mathf.Clamp(currentPosition.z, _minZ, _maxZ);

        if  (currentPosition != _characterTransform.position)
        {
            _characterTransform.position = currentPosition;
        }
    }
}