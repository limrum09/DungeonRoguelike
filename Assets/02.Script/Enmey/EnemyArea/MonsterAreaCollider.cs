using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAreaCollider : MonoBehaviour
{
    [SerializeField]
    private BoxCollider areaCollider;

    public Material cubeMaterial;

    private void Update()
    {
        // 편의성을 위해 만듬
        DrawDebugCube(transform.position, areaCollider.size, Color.red);
    }

    private void DrawDebugCube(Vector3 position, Vector3 size, Color color)
    {
        // Create a cube mesh
        Mesh cubeMesh = CreateCubeMesh();

        // Draw the cube mesh
        Graphics.DrawMesh(cubeMesh, Matrix4x4.TRS(position, Quaternion.identity, size), cubeMaterial, 0);
    }

    private Mesh CreateCubeMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = {
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),
        };

        int[] triangles = {
            0, 2, 1, 0, 3, 2,
            2, 3, 6, 3, 7, 6,
            0, 7, 3, 0, 4, 7,
            1, 6, 5, 1, 2, 6,
            4, 5, 6, 4, 6, 7,
            0, 1, 5, 0, 5, 4
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyCollider>().CheckMosterArea(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyCollider>().CheckMosterArea(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyCollider>().CheckMosterArea(false);
        }
    }
}
