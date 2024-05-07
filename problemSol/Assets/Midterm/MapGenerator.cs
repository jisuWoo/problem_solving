using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // 벽 프리팹
    public Transform planeTransform; // Plane의 Transform
    public TextAsset mapCSV; // 맵 정보를 담은 CSV 파일
    public Vector3 wallStartPosition;

    private int[,] mapData; // 맵 정보를 저장할 배열

    void Start()
    {
        // CSV 파일을 읽어와 맵 데이터를 초기화
        LoadMapFromCSV();

        // 맵 생성
        GenerateMap();
    }

    void LoadMapFromCSV()
    {
        string[] lines = mapCSV.text.Split('\n');

        // 가로와 세로 크기
        int width = lines[0].Trim().Split(',').Length;
        int height = lines.Length;

        // 맵 데이터 배열 생성
        mapData = new int[height, width];

        // CSV 파일에서 맵 데이터를 읽어와 배열에 저장
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
        // Plane의 크기를 가져옴
        Vector3 planeScale = planeTransform.localScale;

        // Plane의 가로세로 크기를 맵 데이터에 맞춰 조정
        float tileSizeX = planeScale.x;
        float tileSizeZ = planeScale.z;

        // 맵 데이터를 기반으로 벽을 생성하여 배치
        for (int i = 0; i < mapData.GetLength(0); i++)
        {
            for (int j = 0; j < mapData.GetLength(1); j++)
            {
                // 현재 위치의 맵 데이터 확인
                int cellType = mapData[i, j];

                // 벽을 생성하여 배치
                if (cellType == 1 || cellType == 2)
                {
                    // 벽의 위치 계산
                    Vector3 wallPosition = new Vector3(
                        wallStartPosition.x + j * tileSizeX,
                        wallStartPosition.y + (cellType == 2 ? 1f : 0.5f),
                        wallStartPosition.z - i * tileSizeZ
                    );

                    // 벽 생성 및 배치
                    GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.identity);

                    wall.transform.localScale = new Vector3(tileSizeX, cellType == 2 ? 2f : 1f, tileSizeZ);

                    // Plane을 벽의 부모로 설정
                    wall.transform.parent = planeTransform;
                }
            }
        }
    }
}
