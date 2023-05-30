using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRake : MonoBehaviour
{
    public Transform targetTransform;
    public float rotationAngle = 90f;

    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private bool isRotating = false;

    private void Start()
    {
        originalRotation = transform.rotation;
        UpdateTargetRotation();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isRotating)
            {
                StartRotation();
            }
            else
            {
                StopRotation();
            }
        }

        UpdateTargetRotation();
    }

    private void UpdateTargetRotation()
    {
        targetRotation = Quaternion.Euler(rotationAngle, 0f, 0f);
    }

    private void StartRotation()
    {
        float rotationDuration = 1.0f; // Adjust the duration of rotation

        StartCoroutine(RotateObject(targetRotation, rotationDuration));
    }

    private void StopRotation()
    {
        StopAllCoroutines();
        StartCoroutine(ReturnToOriginalPosition(1.0f)); // Adjust the duration for returning to the original position
    }

    private System.Collections.IEnumerator RotateObject(Quaternion target, float duration)
    {
        isRotating = true;

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = target;

        isRotating = false;
    }

    private System.Collections.IEnumerator ReturnToOriginalPosition(float duration)
    {
        Quaternion currentRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(currentRotation, originalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = originalRotation;
    }
}