using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadObjectSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnPoint;
    [SerializeField] private Transform parallaxParent;
    private bool isSpawn;
    private float spawnTime;
    private float spawnTimeLimit;

    void Start()
    {
        Debug.Log($"transform.position " + transform.position);
        spawnPoint = transform.position + new Vector3(5, -4);
        spawnTimeLimit = 1;
        isSpawn = true;
        GameObject para = GameObject.FindGameObjectWithTag("Parallax");
        parallaxParent = para.transform;
        gameObject.transform.SetParent(parallaxParent);
        transform.position = spawnPoint;
        Debug.Log($"transform.position " + transform.position);

    }

    void Update()
    {
        if (isSpawn)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnTimeLimit)
            {
                isSpawn = false;
            }
        }    

        if (transform.position.x >= spawnPoint.x + 7)
        {
            Destroy(gameObject);
        }
    }
}
