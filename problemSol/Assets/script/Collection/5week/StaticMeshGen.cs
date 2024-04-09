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

        // �ܺο� ���� �������� �����ư��� �߰��մϴ�.
        for (int i = 0; i < segments * 2; i++)
        {
            float angle = (2 * Mathf.PI / segments) * (i / 2) + Mathf.PI * (i % 2); // �ܺο� ���θ� �����ư��� �߰�
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            float y = (i % 2 == 0) ? 0 : height; // �ܺ� �������� y=0, ���� �������� y=height

            vertices.Add(new Vector3(x, y, z)); // ������ �߰�
        }

        // �� ����� �������� �����մϴ�.
        for (int i = 0; i < segments * 2; i += 2)
        {
            int baseIndex = i;
            int nextIndex = (i + 2) % (segments * 2);

            // �Ʒ� �ﰢ��
            triangles.Add(baseIndex);
            triangles.Add(baseIndex + 1);
            triangles.Add(nextIndex);

            // �� �ﰢ��
            triangles.Add(baseIndex + 1);
            triangles.Add(nextIndex + 1);
            triangles.Add(nextIndex);
        }

        // �ظ�� ������ �߽����� �߰��մϴ�.
        vertices.Add(Vector3.zero); // �ظ� �߽���
        vertices.Add(new Vector3(0, height, 0)); // ���� �߽���

        // �ظ�� ������ �ﰢ���� �����մϴ�.
        for (int i = 0; i < segments * 2; i += 2)
        {
            int baseIndex = i;
            int nextIndex = (i + 2) % (segments * 2);

            // �Ʒ� �ﰢ��
            triangles.Add(baseIndex);
            triangles.Add(nextIndex);
            triangles.Add(vertices.Count - 2); // �ظ��� �߽��� �ε���

            // �� �ﰢ��
            triangles.Add(baseIndex + 1);
            triangles.Add(vertices.Count - 1); // ������ �߽��� �ε���
            triangles.Add(nextIndex + 1);
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        // MeshFilter�� MeshRenderer �߰�
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
