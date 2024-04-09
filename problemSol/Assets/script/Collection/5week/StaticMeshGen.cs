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
            script.StartMesh(3,5,5);
        }
    }
}


public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartMesh(float radius, float height, int segments)
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // 외부와 내부 꼭지점을 번갈아가며 추가합니다.
        for (int i = 0; i < segments * 2; i++)
        {
            float angle = (2 * Mathf.PI / segments) * (i / 2) + Mathf.PI * (i % 2); // 외부와 내부를 번갈아가며 추가
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            float y = (i % 2 == 0) ? 0 : height; // 외부 꼭지점은 y=0, 내부 꼭지점은 y=height

            vertices.Add(new Vector3(x, y, z)); // 꼭지점 추가
        }

        // 별 모양의 프리즘을 생성합니다.
        for (int i = 0; i < segments * 2; i += 2)
        {
            int baseIndex = i;
            int nextIndex = (i + 2) % (segments * 2);

            // 아래 삼각형
            triangles.Add(baseIndex);
            triangles.Add(baseIndex + 1);
            triangles.Add(nextIndex);

            // 위 삼각형
            triangles.Add(baseIndex + 1);
            triangles.Add(nextIndex + 1);
            triangles.Add(nextIndex);
        }

        // 밑면과 윗면의 중심점을 추가합니다.
        vertices.Add(Vector3.zero); // 밑면 중심점
        vertices.Add(new Vector3(0, height, 0)); // 윗면 중심점

        // 밑면과 윗면의 삼각형을 생성합니다.
        for (int i = 0; i < segments * 2; i += 2)
        {
            int baseIndex = i;
            int nextIndex = (i + 2) % (segments * 2);

            // 아래 삼각형
            triangles.Add(baseIndex);
            triangles.Add(nextIndex);
            triangles.Add(vertices.Count - 2); // 밑면의 중심점 인덱스

            // 위 삼각형
            triangles.Add(baseIndex + 1);
            triangles.Add(vertices.Count - 1); // 윗면의 중심점 인덱스
            triangles.Add(nextIndex + 1);
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        // MeshFilter와 MeshRenderer 추가
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
