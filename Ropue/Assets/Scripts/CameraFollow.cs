using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    
    public Vector3 offset;      

    void Start()
    {
        offset = transform.position - player.position;   
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
    }
}
