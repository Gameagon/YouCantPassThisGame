using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshSlicer
{
    public static void Cut(GameObject _originalObject, Vector3 _contactPoint, Vector3 _normal,
        Material _fillMaterial = null, bool fill = true, bool _addRigidbody = false, bool _copyComponents = false)
    {
        Plane plane = new Plane(_originalObject.transform.InverseTransformDirection(_normal), _originalObject.transform.InverseTransformPoint(_contactPoint));
        Mesh originalMesh = _originalObject.GetComponent<MeshFilter>().mesh;
        List<Vector3> addedVertices = new List<Vector3>();

        GeneratedMesh meshA = new GeneratedMesh();
        GeneratedMesh meshB = new GeneratedMesh();

        int[] submeshIndices;
        int triangleIndexA, triangleIndexB, triangleIndexC;

        for(int i = 0; i < originalMesh.subMeshCount; i++)
        {
            submeshIndices = originalMesh.GetTriangles(i);

            for(int e = 0; e < submeshIndices.Length; e++)
            {
                triangleIndexA = submeshIndices[e];
                triangleIndexB = submeshIndices[e + 1];
                triangleIndexC = submeshIndices[e + 2];

                //MeshTriangle currentTriangle = GetTriangle
            }
        }
    }
}