using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnLeaves : MonoBehaviour
{
    public GameObject leafPrefab;
    public int numLeaves = 200;
    public float spawnRadius = 10f;
    public Color[] colors;
    private GameObject[] leaves;

    void Start()
    {
        leaves = new GameObject[numLeaves];
        for (int i = 0; i < numLeaves; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;

            Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            GameObject instantiatedLeaf = Instantiate(leafPrefab, transform.position + randomPosition, randomRotation);

            // Get the Renderer component of the instantiated leaf
            Renderer leafRenderer = instantiatedLeaf.GetComponent<Renderer>();

            // Check if the instantiated leaf has a Renderer component
            if (leafRenderer != null)
            {
                // Modify the material color or shader properties to change the leaf color
                Color color = colors[Random.Range(0,colors.Length)];
                leafRenderer.materials[0].color = color * .6f;
                leafRenderer.materials[1].color = color;
            }

            //add to the list
            leaves[i] = (instantiatedLeaf);
        }
    }

    public void drop()
    {
        for (int i = 0; i < leaves.Length; i++)
        {
            // Get the Rigidbody component of the current leaf
            Rigidbody leafRigidbody = leaves[i].GetComponent<Rigidbody>();

            // Check if the leaf has a Rigidbody component
            if (leafRigidbody != null)
            {
                // Disable the kinematic attribute of the Rigidbody
                leafRigidbody.isKinematic = false;
            }
        }
    }
}


