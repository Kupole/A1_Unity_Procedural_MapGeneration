using UnityEngine;

public class Map_Data : MonoBehaviour
{
    private int _maxYRange = 25;
    public int maxYRange => _maxYRange;
    private int _xSize = 25;
    public int xSize => _xSize;
    private int _zSize = 25;
    public int zSize => _zSize;
    private int _realY;
    public int realY => _realY;

    public Vector3[] tiles;
    public int[] triangles;
    public Color[] colors;

}
