using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Octave
{
    public float frequency = 1f;
    public float amplitude = 1f;
    public int xSize;
    public int zSize;
    public Vector3[] values;
    public Octave(float Frequency, float Amplitude, int XSize, int ZSize) {
    this.frequency = Frequency;
        this.amplitude = Amplitude;
        this.xSize = XSize;
        this.zSize = ZSize;
        this.values = new Vector3[(XSize+1)*(ZSize+1)];
    }

}
public class Map_Data : MonoBehaviour
{
    public List<Octave> octaves = new List<Octave>();
    private int _maxYRange = 25;
    public int maxYRange => _maxYRange;
    public int xSize = 25;
    public int zSize = 25;
    public int amplitude = 2;
    private int _realY;
    public int realY => _realY;

    public int seed = 25987684;

    public Vector3[] quads;
    public Vector3[] vertices;
    public int[] triangles;
    public Color[] colors;
}