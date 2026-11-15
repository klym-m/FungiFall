using System.Collections;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    
    public GameObject[] safePrefabs;
    public GameObject[] poisonPrefabs;
    public GameObject [] fatalPrefabs;
    public Transform[] spawnPoints; 

    public float minInterval = 1f;
    public float maxInterval = 5f;


    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            GameObject prefab;
            float RandoMush = Random.value;
            float waitTime = Random.Range(minInterval, maxInterval);
            
            if (RandoMush < 0.02f)
            {
                prefab = fatalPrefabs[Random.Range(0, fatalPrefabs.Length)];
            }
            else if (RandoMush < 0.3f)
            {
                prefab = poisonPrefabs[Random.Range(0, poisonPrefabs.Length)];
                
            }
            else
            {
                prefab = safePrefabs[Random.Range(0, safePrefabs.Length)];
                
            }
            SpawnMushroom(prefab);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnMushroom(GameObject prefab)
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(prefab, point.position, Quaternion.identity);
    }
}
