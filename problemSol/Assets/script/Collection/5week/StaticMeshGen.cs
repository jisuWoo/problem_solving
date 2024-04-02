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
{
    // Start is called before the first frame update
    public void StartMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, 0f, 0f),                    // 중앙
            new Vector3(0.5f, 1f, 0f),                   // 뾰족한 부분 1
            new Vector3(0.5f, 0.5f, 0f),                 // 뾰족한 부분 1과 중앙 사이
            new Vector3(1f, 1f, 0f),                     // 뾰족한 부분 2
            new Vector3(0.5f, 0.5f, 0f),                 // 뾰족한 부분 2와 중앙 사이
            new Vector3(1.5f, 0f, 0f),                   // 뾰족한 부분 3
            new Vector3(1.0f, 0.5f, 0f),                 // 뾰족한 부분 3과 중앙 사이
            new Vector3(2.0f, 0f, 0f),                   // 뾰족한 부분 4
            new Vector3(1.5f, -0.5f, 0f),                // 뾰족한 부분 4와 중앙 사이
            new Vector3(0.5f, -1f, 0f)
        };
        //점 넣음
        mesh.vertices = vertices;

        int[] triangledrices = new int[]
        {
            0, 1, 2,
            // 두 번째 뾰족한 부분
            0, 2, 3,
            // 세 번째 뾰족한 부분
            0, 3, 4,
            // 네 번째 뾰족한 부분
            0, 4, 5,
            // 다섯 번째 뾰족한 부분
            0, 5, 6,
            0, 6, 7,
            0,7,8,
            0, 8, 9,
            0, 9, 1,
        };
        mesh.triangles = triangledrices;

        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();
        

        mf.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
