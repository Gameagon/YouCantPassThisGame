using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedMesh : MonoBehaviour
{
	List<Vector3> vertices = new List<Vector3>();
	List<Vector3> normals = new List<Vector3>();
	List<Vector2> uvs = new List<Vector2>();
	List<List<int>> submeshIndices = new List<List<int>>();

	public List<Vector3> Vertices { get { return vertices; } set { vertices = value; } }
	public List<Vector3> Normals { get { return normals; } set { normals = value; } }
	public List<Vector2> Uvs { get { return uvs; } set { uvs = value; } }
    public List<List<int>> SubmeshIndices { get { return submeshIndices; } set { submeshIndices = value; } }

	public void AddTriangle(MeshTriangle _trianlge)
    {
        int currentVertexCount = vertices.Count;

        vertices.AddRange(_trianlge.Vertices);
        normals.AddRange(_trianlge.Normals);
        uvs.AddRange(_trianlge.Uvs);

        if (submeshIndices.Count < _trianlge.SubmeshIndicex + 1)
        {
            for(int i = submeshIndices.Count; i < _trianlge.SubmeshIndicex + 1; i++)
            {
                submeshIndices.Add(new List<int>());
            }
        }

        for(int i = 0; i < 3; i++)
        {
            submeshIndices[_trianlge.SubmeshIndicex].Add(currentVertexCount + i);
        }
    }
}

public class MeshTriangle
{
    List<Vector3> vertices = new List<Vector3>();
    List<Vector3> normals = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    int submeshIndicex;

    public List<Vector3> Vertices { get { return vertices; } set { vertices = value; } }
    public List<Vector3> Normals { get { return normals; } set { normals = value; } }
    public List<Vector2> Uvs { get { return uvs; } set { uvs = value; } }
    public int SubmeshIndicex { get { return submeshIndicex; } set { submeshIndicex = value; } }

    public MeshTriangle(Vector3[] _vertices, Vector3[] _normals, Vector2[] _uvs, int _submeshIndex)
    {
        Clear();

        vertices.AddRange(_vertices);
        normals.AddRange(_normals);
        uvs.AddRange(_uvs);

        submeshIndicex = _submeshIndex;
    }

    public void Clear()
    {
        vertices.Clear();
        normals.Clear();
        uvs.Clear();

        submeshIndicex = 0;
    }
}