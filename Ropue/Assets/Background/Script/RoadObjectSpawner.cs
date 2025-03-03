using System.Collections;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class RoadObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] RoadObject;
    [SerializeField] private Vector2 spawnPoint;
    private Vector2 lastObjectSpawnPoint;
    private bool isSpawn;
    private int minSpawnTimeDelay;

    void Awake()
    {
        minSpawnTimeDelay = 2;
        isSpawn = true;
        spawnPoint = transform.position;
    }

    private void Start()
    {
        StartCoroutine(SpawnRoadObjects());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnRoadObjects()
    {
        while (isSpawn)
        {
            if (spawnPoint.x <= transform.position.x)
            {
                var index = Random.Range(0, RoadObject.Length);

                if (RoadObject[index].CompareTag("Trampoline"))
                {
                    spawnPoint = new Vector2(transform.position.x + 12, -4);
                }

                else if (RoadObject[index].CompareTag("Volcano"))
                {
                    spawnPoint = new Vector2(transform.position.x + 12, 0);
                }

                Instantiate(RoadObject[index], spawnPoint, Quaternion.identity);
            }
            yield return new WaitForSeconds(minSpawnTimeDelay);
        }
    }
}
