using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Map_Data))]

public class Map_Generation : MonoBehaviour
{


    public void InitiateArrays(Map_Data data)
    {
        data.quads = new Vector3[(data.xSize) * (data.zSize)];
        data.vertices = new Vector3[(data.xSize + 1) * (data.zSize + 1)];
        data.triangles = new int[(3 * data.vertices.Length) / 2];
    }

    public void GenerateVertices(Map_Data data) 
    {
        for (int i = 0, z = 0; z <= data.zSize; z++) 
        {
            for (int x = 0; x <= data.xSize; x++)
            {
                data.vertices[i] = new Vector3(x, Mathf.Clamp(Mathf.PerlinNoise(x + data.seed / 100, z) * data.maxYRange, 0,data.maxYRange), z);
                i++;
            }
        }

        if(data.octaves != null)
        {
            foreach (Octave octave in data.octaves)
            {
                data.vertices = ApplyOctave(data, data.vertices, octave);
            }
        }
    }

    public void GenerateTriangles(Map_Data data)
    {/*
        GenerateVertices(data);
        data.triangles[0] = z*/
    }

    public Vector3[] ApplyOctave(Map_Data data, Vector3[] vertices, Octave octave)
    {
        octave.xSize = data.xSize;
        octave.zSize = data.zSize;
        GenerateOctave(octave);
        Vector3[] newVertices = new Vector3[vertices.Length];
        int count = 0;
        foreach (Vector3 vertice in vertices)
        {
            Vector3 temp = new Vector3(vertice.x,vertice.y * (octave.values[(int)vertice.x * (int)vertice.z + (int)vertice.z].y), vertice.z);
            newVertices[count] = temp;
            count++;
        }
        return newVertices;
    }

    public void GenerateOctave(Octave octave) 
    {
        for (int i = 0, z = 0; z <= octave.zSize; z++)
        {
            for (int x = 0; x <= octave.xSize; x++)
            {
                octave.values[i] = new Vector3(x,Mathf.PerlinNoise(x*octave.frequency,z*octave.frequency)*octave.amplitude,z);
                i++;
            }
        }
    }

    
}
