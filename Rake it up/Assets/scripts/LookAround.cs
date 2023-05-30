using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    //Inspired by https://www.youtube.com/watch?v=_QajrabyTJc Brackeys
    [Tooltip("Mouse Senisitivity - Default 100")]
    public float mouseSensitivity;

    float xRot = 0f;

    public Transform playerBody;

    public Vector3 screenPosition;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        screenPosition = Input.mousePosition;
    }
}
