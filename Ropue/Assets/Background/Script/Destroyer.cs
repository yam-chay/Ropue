using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private RoadObjectSpawner roadObjects;

    private void Awake()
    {
        roadObjects = GetComponent<RoadObjectSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RemoveRoadObject(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RemoveRoadObject(collision.collider);
    }

    private void RemoveRoadObject(Collider2D collider)
    {
        for (int i = 0; i < roadObjects.RoadObject.Length; i++)
        {
            if (collider.gameObject == roadObjects.RoadObject[i])
            {
                Destroy(collider.gameObject);
                break;
            }
        }
    }
}
