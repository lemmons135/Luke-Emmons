using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] GameObject player;

    public float cameraFollowOffset = 0f;

    Camera mainCamera;
    float width;
    float height;

    float Xposition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        width = mainCamera.orthographicSize * mainCamera.aspect;
        height = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // the player is at the right boundary of the camera
        if (player.transform.position.x > transform.position.x + width - cameraFollowOffset)
        { 
            Xposition = player.transform.position.x - width + cameraFollowOffset;
            transform.position = new Vector3(Xposition, transform.position.y, transform.position.z);
        }
        // player is at the left boundary of the camera
        else if (player.transform.position.x < transform.position.x - width + cameraFollowOffset)
        {
            Xposition = player.transform.position.x + width - cameraFollowOffset;
            transform.position = new Vector3(Xposition, transform.position.y, transform.position.z);
        }
        //transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }
}
