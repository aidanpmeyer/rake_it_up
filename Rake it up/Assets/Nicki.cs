using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nicki : MonoBehaviour
{
    private Animator animator;
    private AudioSource noise;
    public AudioClip[] phrases;

    private float minSpeed = 2f;
    private float maxSpeed = 8f;
    private float minChangeTime = 5f;
    private float maxChangeTime = 10f;
    private float changeDirectionTime;
    private float moveSpeed;
    private Vector3 targetDirection;

    private void Start()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();
        noise = GetComponent<AudioSource>();

        SetRandomMoveSpeed();
        SetRandomTargetDirection();
        changeDirectionTime = GetRandomChangeDirectionTime();
        noise.PlayOneShot(phrases[0]);
        animator.Play("Run", 0, 0);
    }



    // Update is called once per frame
    void Update()
    {
        // Rotate towards the target direction
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 100f);

        // Move in the target direction
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        changeDirectionTime -= Time.deltaTime;
        if (changeDirectionTime <= 0f)
        {
            ChangeDirection();
            changeDirectionTime = GetRandomChangeDirectionTime();
            animator.Play("Run",0, changeDirectionTime);
            CallFunctionOnDirectionChange();
        }
        //LoopCurrentAnimation();
    }


    private void ChangeDirection()
    {
        
        noise.PlayOneShot(phrases[(int)Random.Range(0, phrases.Length)]);
        SetRandomTargetDirection();
        SetRandomMoveSpeed();
    }

    private void SetRandomTargetDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        targetDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;
    }

    private void SetRandomMoveSpeed()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private float GetRandomChangeDirectionTime()
    {
        return Random.Range(minChangeTime, maxChangeTime);
    }

    private void CallFunctionOnDirectionChange()
    {
        // Replace this with your custom function to be called when the direction changes
        Debug.Log("Direction changed!");
    }

    private void LoopCurrentAnimation()
    {
        // Reset the current animation time to 0 to make it loop
        animator.Play("Run", 0, 0);
    }
}
