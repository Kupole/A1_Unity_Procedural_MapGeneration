using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Map_Data))]
public class Map_Generation : MonoBehaviour
{
    [SerializeField] Map_Data data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data.vertices = new Vector3[];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
