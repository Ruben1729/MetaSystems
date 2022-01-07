using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNoiseGenerator : MonoBehaviour
{

    public float mPower = 3;

    public float mScale = 1;

    public float mTimeScale = 1;

    private float _xOffset;
    private float _yOffset;
    private MeshFilter _meshFilter;
    
    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        MakeNoise();
    }

    // Update is called once per frame
    void Update()
    {
        MakeNoise();
        _xOffset += Time.deltaTime * mTimeScale;
        _yOffset += Time.deltaTime * mTimeScale;
    }
    
    void MakeNoise()
    {
        Vector3[] vertices = _meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = CalculateHeight(vertices[i].x, vertices[i].z) * mPower;
        }

        _meshFilter.mesh.vertices = vertices;
    }

    float CalculateHeight(float x, float y)
    {
        float xCoord = x * mScale + _xOffset;
        float yCoord = y * mScale + _yOffset;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
