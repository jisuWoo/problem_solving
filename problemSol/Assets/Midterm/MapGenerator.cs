using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // �� ������
    public Transform planeTransform; // Plane�� Transform
    public TextAsset mapCSV; // �� ������ ���� CSV ����
    public Vector3 wallStartPosition;

    private int[,] mapData; // �� ������ ������ �迭

    void Start()
    {
        // CSV ������ �о�� �� �����͸� �ʱ�ȭ
        LoadMapFromCSV();

        // �� ����
        GenerateMap();
    }

    void LoadMapFromCSV()
    {
        string[] lines = mapCSV.text.Split('\n');

        // ���ο� ���� ũ��
        int width = lines[0].Trim().Split(',').Length;
        int height = lines.Length;

        // �� ������ �迭 ����
        mapData = new int[height, width];

        // CSV ���Ͽ��� �� �����͸� �о�� �迭�� ����
        for (int i = 0; i < height; i++)
        {
            string[] row = lines[i].Trim().Split(',');
            for (int j = 0; j < width; j++)
            {
                mapData[i, j] = int.Parse(row[j]);
            }
        }
    }

    void GenerateMap()
    {
        // Plane�� ũ�⸦ ������
        Vector3 planeScale = planeTransform.localScale;

        // Plane�� ���μ��� ũ�⸦ �� �����Ϳ� ���� ����
        float tileSizeX = planeScale.x;
        float tileSizeZ = planeScale.z;

        // �� �����͸� ������� ���� �����Ͽ� ��ġ
        for (int i = 0; i < mapData.GetLength(0); i++)
        {
            for (int j = 0; j < mapData.GetLength(1); j++)
            {
                // ���� ��ġ�� �� ������ Ȯ��
                int cellType = mapData[i, j];

                // ���� �����Ͽ� ��ġ
                if (cellType == 1 || cellType == 2)
                {
                    // ���� ��ġ ���
                    Vector3 wallPosition = new Vector3(
                        wallStartPosition.x + j * tileSizeX,
                        wallStartPosition.y + (cellType == 2 ? 1f : 0.5f),
                        wallStartPosition.z - i * tileSizeZ
                    );

                    // �� ���� �� ��ġ
                    GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.identity);

                    wall.transform.localScale = new Vector3(tileSizeX, cellType == 2 ? 2f : 1f, tileSizeZ);

                    // Plane�� ���� �θ�� ����
                    wall.transform.parent = planeTransform;
                }
            }
        }
    }
}
