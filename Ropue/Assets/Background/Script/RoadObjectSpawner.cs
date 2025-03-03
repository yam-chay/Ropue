using System.Collections;
using UnityEngine;

public class RoadObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] RoadObject;
    [SerializeField] private Transform playerTransform;
    private Vector2 spawnPoint;
    private bool isSpawn;
    private float spawnTimeLimit;

    void Awake()
    {
        spawnTimeLimit = 4;
        isSpawn = true;
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
            spawnPoint = new Vector2(playerTransform.position.x + 12, -4);
            var index = Random.Range(0, RoadObject.Length);
            Instantiate(RoadObject[index], spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimeLimit);
        }

    }
}
