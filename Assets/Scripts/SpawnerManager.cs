using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject bonusPrafab;


    // Update is called once per frame
    void Update()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
        Instantiate(notePrefab, randomSpawnPosition, Quaternion.identity);
    }
}
