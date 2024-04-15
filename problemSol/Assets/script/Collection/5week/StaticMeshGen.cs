using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(StaticMeshGen))]

public class StaticMeshGenEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen script = (StaticMeshGen)target;

        if (GUILayout.Button("Generate Medsh"))
        {
            script.StartMesh();
        }
    }
}


public class StaticMeshGen : MonoBehaviour
{   public void StartMesh()
    {
        float moveDistance = 0.5f;
        float Zoffset = 1.0f;

        Mesh mesh = new Mesh();

        float R = 1f;
        float r = R * Mathf.Cos(36f * Mathf.Deg2Rad);

        Vector3[] vertices = new Vector3[5];

        for (int i = 0; i < 5; i++)
        {
            float angle = i * (360f / 5) * Mathf.Deg2Rad;
            vertices[i] = new Vector3(R * Mathf.Cos(angle), R * Mathf.Sin(angle), 0);
        }

        Vector3 center = Vector3.zero;

        Vector3[] midpoints = new Vector3[5];

        for (int i = 0; i < 5; i++)
        {
            int nextIndex = (i + 1) % 5;
            midpoints[i] = Vector3.Lerp((vertices[i] + vertices[nextIndex]) / 2f, center, moveDistance); //+ new Vector3(0, 0, Zoffset);
        }

        Vector3[] allVertices = new Vector3[vertices.Length + midpoints.Length];
        vertices.CopyTo(allVertices, 0);
        midpoints.CopyTo(allVertices, vertices.Length);

        Vector3[] m_Vertices = new Vector3[allVertices.Length];
        Vector3[] p_Vertices = new Vector3[allVertices.Length];

        for (int i = 0;i < allVertices.Length;i++)
        {
            m_Vertices[i] = allVertices[i] - new Vector3(0, 0, Zoffset);
            p_Vertices[i] = allVertices[i] + new Vector3(0, 0, Zoffset);
        }

        Vector3[] result = new Vector3[allVertices.Length * 2];
        m_Vertices.CopyTo(result, 0);
        p_Vertices.CopyTo(result, m_Vertices.Length);

        int[] triangles = new int[]
        {
            1, 5, 6,
            0, 9, 5,
            5, 9, 8,
            5, 8, 6,
            6, 8, 7,
            6, 7, 2,
            7, 8, 3,
            4, 8, 9,//별1

            10, 15, 19,
            19, 18, 14,
            18, 17, 13,
            17, 16, 12,
            16, 15, 11,
            15, 16, 18,
            16, 17, 18,
            15, 18, 19,//별2

            0,10,9,
            9,10,19,
            5,15,0,
            0,15,10,

            1,11,5,
            5,11,15,
            6,16,1,
            1,16,11,
            
            2,12,6,
            6,12,16,
            7,17,2,
            2,17,12,
            
            3,13,7,
            7,13,17,
            8,18,3,
            3,18,13,
            
            4,14,8,
            8,14,18,
            9,19,4,
            4,19,14,                 
        };

        mesh.vertices = result;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (!meshFilter)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (!meshRenderer)
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        meshRenderer.sharedMaterial.color = Color.yellow;

    }
}
