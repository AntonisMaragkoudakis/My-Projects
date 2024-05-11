using UnityEngine;

public class camera_movement : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
    }

    void LateUpdate()
    {
        Vector3 newPosition = transform.position; 
        newPosition.z = Player.transform.position.z-15;
        newPosition.x = Player.transform.position.x;
        transform.position = newPosition;
    }
}