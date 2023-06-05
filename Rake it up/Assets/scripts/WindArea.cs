using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{ 
    public string targetTag = "leaf";
    public float strength = 1f;

    public Transform blower;
    public Transform player;
    private Vector3 forceRot;

    private void OnTriggerEnter(Collider other)
    {
        forceRot = blower.forward * -1;
        forceRot.y *= 0.5f;
        
        if (other.CompareTag(targetTag))
        {
            other.GetComponent<Rigidbody>().AddForce(forceRot * strength, ForceMode.Force);
        }
    }
}
