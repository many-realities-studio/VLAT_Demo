using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera reference
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Make the canvas face the camera's position
            Vector3 directionToCamera = transform.position - mainCamera.transform.position;
            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }
}