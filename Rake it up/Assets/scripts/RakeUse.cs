using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakeUse : MonoBehaviour
{
 

    public float downAngle = 40f;
    public float yAngle = -159.47f;
    public float forwardDuration = 2f;
    public float backwardDuration = 2f;
    private bool isRotating = false;
    public bool inUse = false;

    public Transform player;
    public float yoffsetMult = 3f;

    private Quaternion originalRotation;
    private Quaternion startRotation;
    void Start()
    {
        originalRotation = transform.localRotation;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inUse = true;
            if (isRotating)
            {
                StopAllCoroutines();
            }
        
            StartCoroutine(RotateForward());
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            inUse = false;
            if (isRotating) {
                StopAllCoroutines();
            }
            StartCoroutine(RotateBackward());
        } 
    }

    private System.Collections.IEnumerator RotateForward()
    {
        isRotating = true;
        Quaternion targetForward = Quaternion.Euler(new Vector3(downAngle - player.position.y * yoffsetMult, yAngle, 0f));
        startRotation = transform.localRotation;

        float t = 0f;
        while (t < 1f)
        {
        
            t += Time.deltaTime / forwardDuration;
            transform.localEulerAngles = Quaternion.Slerp(startRotation, targetForward, t).eulerAngles;
            yield return null;
        }

        isRotating = false;
    }
    private System.Collections.IEnumerator RotateBackward()
    {
        isRotating = true;

        float t = 0f;
        startRotation = transform.localRotation;

        // Reset rotation
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / backwardDuration;
            transform.localEulerAngles = Quaternion.Slerp(startRotation, originalRotation, t).eulerAngles;
            yield return null;
        }

        isRotating = false;
    }

}


