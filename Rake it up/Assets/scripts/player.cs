using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float movementSpeed = 5f;  // Speed of camera movement
    public float rotationSpeed = 100f;  // Speed of camera rotation

    private void Update()
    {
        // Camera Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement) * movementSpeed * Time.deltaTime;

        // Move the camera
        transform.Translate(movement);

        // Camera Rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera based on mouse input
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left, mouseY * rotationSpeed * Time.deltaTime);
    }
}
