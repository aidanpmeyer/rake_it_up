using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform target; // The target point to rotate around
    public float rotationSpeed = 10f; // The speed of camera rotation
    public float distance = 40f; // The distance between the camera and the target point
    public float height = 20;
    private Vector3 offset; // The initial offset between the camera and the target
    private float currentAngle; // The current angle of rotation
    

    private void Start()
    {
        // Calculate the initial offset
        offset = transform.position - target.position;
    }

    private void Update()
    {
        // Calculate the desired position based on the target position and distance
        Vector3 desiredPosition = target.position + offset;

        // Calculate the new angle of rotation
        currentAngle += rotationSpeed * Time.deltaTime;

        // Calculate the new position based on the angle
        Vector3 rotation = Quaternion.Euler(0f, currentAngle, 0f) * Vector3.forward;
        desiredPosition = target.position + (rotation * distance);

        // Set the new camera position
        desiredPosition.y = desiredPosition.y + height;
        transform.position = desiredPosition;
        

        // Look at the target
        transform.LookAt(target);
    }
}