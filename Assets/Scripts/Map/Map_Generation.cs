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
        data.triangles = new int[(2 * 3 * data.quads.Length)];
    }

    public void GenerateVertices(Map_Data data) 
    {
        for (int i = 0, z = 0; z <= data.zSize; z++) 
        {
            for (int x = 0; x <= data.xSize; x++)
            {
                data.vertices[i] = new Vector3(x, Mathf.Clamp(Mathf.PerlinNoise((float)x / (data.xSize + 1) / 2 + (float)data.seed/1000, (float)z / (data.zSize + 1) / 2) * data.maxYRange, 0,data.maxYRange) * data.amplitude, z);
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
    {
        GenerateVertices(data);
        for (int i = 0, z = 0; z < data.zSize; z++)
        {
            for(int x = 0; x < data.xSize; x++)
            {
                data.triangles[i * 6 + 2] = x + (data.xSize+1) * z;
                data.triangles[i * 6 + 1] = x + (data.xSize + 1) * z + 1;
                data.triangles[i * 6] = x + (data.xSize+1) * (z + 1);
                data.triangles[i * 6 + 3] = x + (data.xSize+1) * z + 1;
                data.triangles[i * 6 + 4] = x + (data.xSize+1) * (z + 1);
                data.triangles[i * 6 + 5] = x + (data.xSize+1) * (z + 1) +1;
                i++;
            }
        }
    }

    public Vector3[] ApplyOctave(Map_Data data, Vector3[] vertices, Octave octave)
    {
        octave.xSize = data.xSize;
        octave.zSize = data.zSize;
        octave.values = new Vector3[(octave.xSize + 1) * (octave.zSize + 1)];
        GenerateOctave(octave);
        Vector3[] newVertices = new Vector3[vertices.Length];
        int count = 0;
        foreach (Vector3 vertice in vertices)
        {
            Vector3 temp = new Vector3(vertice.x,(vertice.y + (octave.values[((int)data.xSize+1) * (int)vertice.z + (int)vertice.z].y))-(octave.amplitude/2), vertice.z);
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
                octave.values[i] = new Vector3(x,Mathf.PerlinNoise((float)x/octave.frequency / (octave.xSize+1),(float)z/octave.frequency/(octave.zSize+1))*octave.amplitude,z);
                i++;
            }
        }
    }

    
}
