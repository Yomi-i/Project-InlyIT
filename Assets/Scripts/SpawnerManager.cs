using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    // Variables.
    [Header("Spawner range")]
    [SerializeField] private float _RangeX = 12f;
    [SerializeField] private float _RangeZ = 12f;

    [Header("Spawner restictions")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _minDistanceFromPlayer = 5f;
    [SerializeField] private float _minSpawnCooldown = 2f;
    [SerializeField] private float _maxSpawnCooldown = 5f;


    [Header("Prefabs")]
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject bonusPrafab;
    private GameObject[] _availablePrefabs; // array of prefabs to spawn;


    // Flags.
    private bool bIsSpawning = false;


    void Start()
    {
        _availablePrefabs = new GameObject[]
        {
            notePrefab,
            bombPrefab,
            healthPrefab,
            bonusPrafab
        };
        StartCoroutine(
                SpawnCooldown());
    }
    
    IEnumerator SpawnCooldown()
    {
        while(true)
        {
            float waitTime = Random.Range(_minSpawnCooldown, _maxSpawnCooldown);
            yield return new WaitForSeconds(waitTime);

            SpawnRandomObject();
        }
    }

    private void SpawnRandomObject()
    {
        if (bIsSpawning) return;

        bIsSpawning = true;

        GameObject randomPrefab = _availablePrefabs[Random.Range(0, _availablePrefabs.Length)];
        Vector3 randomSpawnPos = FindSpawnPos();
        if (randomPrefab)
        {
                Instantiate(randomPrefab, randomSpawnPos, Quaternion.identity);
        }
        else 
        {
            Debug.LogWarning("No prefabs to spawn");
            bIsSpawning = false;
            return;
        }

        bIsSpawning = false;
    }

    private Vector3 FindSpawnPos()
    {
        while(bIsSpawning)
        {
            Vector3 safeSpawnPos = new Vector3(
                Random.Range(-_RangeX, _RangeX),
                1,
                Random.Range(-_RangeZ, _RangeZ));
            float distanceToPlayer = Vector3.Distance(safeSpawnPos, _playerTransform.position);
            
            if (distanceToPlayer >= _minDistanceFromPlayer) return safeSpawnPos;
        }

        return Vector3.zero;
    }
}