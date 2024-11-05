using UnityEngine;

public class AsteroidFieldGenerator : MonoBehaviour
{
    public int asteroidCount = 50; // Number of asteroids to generate
    public float fieldRadius = 50f; // Radius of the area in which asteroids are distributed
    public int vertexCount = 100; // Number of vertices for each asteroid
    public float baseRadius = 1f; // Base radius of each asteroid
    public float displacement = 0.5f; // Maximum displacement from the base radius

    public Texture asteroidTexture; // Reference to the asteroid texture

    void Start()
    {
        // Load the texture from resources (make sure the texture is in the Resources folder)
        asteroidTexture = Resources.Load<Texture>("space_asteroids_02_l_0008");
        GenerateAsteroidField();
    }

    void GenerateAsteroidField()
    {
        for (int i = 0; i < asteroidCount; i++)
        {
            // Create a new empty GameObject for each asteroid
            GameObject asteroid = new GameObject("Asteroid_" + i);
            asteroid.transform.position = Random.insideUnitSphere * fieldRadius; // Random position within the field radius
            asteroid.transform.parent = transform; // Set the parent to keep the hierarchy organized

            // Add necessary components
            MeshFilter meshFilter = asteroid.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = asteroid.AddComponent<MeshRenderer>();

            // Create and assign a new material with the asteroid texture
            Material material = new Material(Shader.Find("Standard"));
            material.mainTexture = asteroidTexture; // Set the asteroid texture
            meshRenderer.material = material;

            // Generate the asteroid mesh
            Mesh mesh = GenerateAsteroidMesh();
            meshFilter.mesh = mesh;
        }
    }

    Mesh GenerateAsteroidMesh()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        // Generate random vertices on a sphere
        for (int i = 0; i < vertexCount; i++)
        {
            Vector3 randomPoint = Random.onUnitSphere * baseRadius;
            randomPoint += Random.insideUnitSphere * displacement; // Displace the point randomly
            vertices[i] = randomPoint;
        }

        // Triangulate to create faces
        for (int i = 1; i < vertexCount - 1; i++)
        {
            triangles[(i - 1) * 3] = 0;
            triangles[(i - 1) * 3 + 1] = i;
            triangles[(i - 1) * 3 + 2] = i + 1;
        }

        // Set the mesh properties
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // Ensure proper lighting
        return mesh;
    }
}
