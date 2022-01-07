using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlaneGenerator : MonoBehaviour
{
    
    [Header("Plane")]
    public float mSize = 1;
    public int mGridSize = 16;
    
    [Header("Waves")]
    public Octave[] Octaves;

    private MeshFilter _meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = GenerateMesh();
        transform.position = new Vector3(-mSize/2, 0, -mSize/2);
    }

    void Update()
    {
        var verts = _meshFilter.mesh.vertices;
        for (int x = 0; x <= mGridSize; x++)
        {
            for (int z = 0; z <= mGridSize; z++)
            {
                var y = 0f;
                for (int o = 0; o < Octaves.Length; o++)
                {
                    if (Octaves[o].alternate)
                    {
                        var perl = Mathf.PerlinNoise((x * Octaves[o].scale.x) / mGridSize, (z * Octaves[o].scale.y) / mGridSize) * Mathf.PI * 2f;
                        y += Mathf.Cos(perl + Octaves[o].speed.magnitude * Time.time) * Octaves[o].height;
                    }
                    else
                    {
                        var perl = Mathf.PerlinNoise((x * Octaves[o].scale.x + Time.time * Octaves[o].speed.x) / mGridSize, (z * Octaves[o].scale.y + Time.time * Octaves[o].speed.y) / mGridSize) - 0.5f;
                        y += perl * Octaves[o].height;
                    }
                }

                verts[index(x, z)] = new Vector3(x, y, z);
            }
        }

        _meshFilter.mesh.vertices = verts;
        _meshFilter.mesh.RecalculateNormals();
    }

    private int index(int x, int z)
    {
        return x * (mGridSize + 1) + z;
    }

    private int index(float x, float z)
    {
        return index((int)x, (int)z);
    }
    
    private Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        for (int x = 0; x < mGridSize + 1; x++)
        {
            for (int y = 0; y < mGridSize + 1; y++)
            {
                vertices.Add(new Vector3(-mSize * 0.5f + mSize * (x / ((float)mGridSize)), 0, -mSize * 0.5f + mSize * (y / ((float)mGridSize))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)mGridSize, y / (float) mGridSize));
            }
        }

        var triangles = new List<int>();
        var vertCount = mGridSize + 1;

        for (int i = 0; i < vertCount * vertCount - vertCount; i++)
        {
            if ((i + 1) % vertCount == 0)
                continue;
            
            triangles.AddRange(new List<int>()
            {
                i + 1 + vertCount, i + vertCount, i, i, i + 1, i + vertCount + 1
            });

        }
        
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);
        
        return mesh;
    }
    
    [Serializable]
    public struct Octave
    {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }

}
