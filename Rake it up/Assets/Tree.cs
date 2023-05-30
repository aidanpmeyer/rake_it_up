using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] bushes;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            shed();
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
