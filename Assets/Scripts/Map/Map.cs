using UnityEngine;

[RequireComponent(typeof(Map_Data))]
[RequireComponent(typeof(Map_Generation))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class Map : MonoBehaviour
{
    [SerializeField] private Map_Data data;
    [SerializeField] private Map_Generation gen;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;
    public Mesh mesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrepareMesh(mesh, meshFilter);
        AssignComponents();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Generate()
    {
        PrepareMesh(mesh, meshFilter);
        AssignComponents(); //Delete this later
        gen.InitiateArrays(data);
        gen.GenerateTriangles(data);

        UpdateMesh();
        
    }

    private void AssignComponents()
    {
        data = GetComponent<Map_Data>();
        gen = GetComponent<Map_Generation>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    public void UpdateMesh()
    {
        if(mesh == null)
        {
            mesh = new Mesh();
        }
        mesh.Clear();

        mesh.vertices = data.vertices;
        if (data.triangles != null)
        {
            mesh.triangles = data.triangles;
            mesh.RecalculateNormals();
        }

        meshFilter.mesh = mesh; 
        meshRenderer.material = new Material(Shader.Find("Standard"));

    }

    public void PrepareMesh(Mesh mesh, MeshFilter meshFilter)
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;
    }
    private void OnDrawGizmos()
    {
        if (data.vertices != null) {
            for (int i = 0; i < data.vertices.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(data.vertices[i], 0.02f);
            }
        }
    }

}
