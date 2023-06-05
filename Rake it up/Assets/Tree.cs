using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] bushes;
    private AudioSource rakeSound;
    void Update()
    {
        if (FindObjectOfType<GameManager>())
        {

            if (FindObjectOfType<GameManager>().autoFall && FindObjectOfType<GameManager>().roundStarted)
            {
                shed();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("rahhhhh");
        if (other.CompareTag("rake")) {
            if (other.gameObject.GetComponent<RakeUse>().inUse)
            {
                rakeSound = other.GetComponent<AudioSource>();
                rakeSound.Play();
                shed();
            }
        }
    }

    private void shed()
    {
        for(int i = 0; i < bushes.Length; i++)
        {
            bushes[i].GetComponent<SpawnLeaves>().drop();
        }
    }
}
