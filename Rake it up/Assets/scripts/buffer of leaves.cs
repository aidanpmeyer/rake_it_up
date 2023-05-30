using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leafbuffer: MonoBehaviour
{
    public int numInstances = 400;
    public Material material;
    public Mesh mesh;

    ComputeBuffer positionBuffer;
    ComputeBuffer argsBuffer;

    void Start()
    {
        //create buffer containing some info about mesh, and the number of instances to draw
        argsBuffer = new ComputeBuffer(1, 5 * sizeof(uint), ComputeBufferType.IndirectArguments);
        argsBuffer.SetData(new uint[] { (uint)mesh.GetIndexCount(0), (uint)numInstances,0,0,0});

        //create buffer containing the positions for each instance
        Vector3[] randomPositions = new Vector3[numInstances];
        for (int i = 0; i < numInstances; i++)
        {
            randomPositions[i] = Random.insideUnitSphere * 200;
        }
        positionBuffer = new ComputeBuffer(numInstances, sizeof(float) * 3);
        positionBuffer.SetData(randomPositions);
        material.SetBuffer("PositionBuffer", positionBuffer);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Draw many copies of the mesh using GPU instancing
        Graphics.DrawMeshInstancedIndirect(mesh, 0, material, mesh.bounds, argsBuffer);
    }

    private void OnDestroy()
    {
        positionBuffer.Release();
        argsBuffer.Release();   

       
    }
}
