using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // 이동 속도
    public float moveRangeWidth = 5f; // 이동 범위의 가로 길이
    public float moveRangeHeight = 5f; // 이동 범위의 세로 길이

    private Vector3 startPosition; // 시작 위치
    private Vector3 targetPosition; // 목표 위치
    private bool movingForward = true; // 앞으로 이동 중인지 여부

    private void Start()
    {
        startPosition = transform.position;
        ChooseRandomTargetPosition();
    }

    private void Update()
    {
        // 이동 방향과 속도를 곱하여 이동 벡터를 구함
        Vector3 movement = (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
        // 현재 위치에서 목표 위치로 이동
        transform.Translate(movement);

        // 만약 목표 위치에 도달하면 이동 방향을 반대로 변경하여 다시 시작 위치로 이동
        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            movingForward = !movingForward;
            ChooseRandomTargetPosition();
        }
    }

    // 시작 위치를 중심으로 랜덤한 위치를 선택하여 목표 위치로 설정합니다.
    private void ChooseRandomTargetPosition()
    {
        float randomX = Random.Range(startPosition.x - moveRangeWidth / 2f, startPosition.x + moveRangeWidth / 2f);
        float randomZ = Random.Range(startPosition.z - moveRangeHeight / 2f, startPosition.z + moveRangeHeight / 2f);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    // 에디터 상에서 이동 범위를 표시하기 위해 OnDrawGizmos 함수를 사용합니다.
    private void OnDrawGizmos()
    {
        // 파란색으로 이동 범위를 표시합니다.
        Gizmos.color = Color.yellow;
        Vector3 topLeft = startPosition + new Vector3(-moveRangeWidth / 2f, 0f, moveRangeHeight / 2f);
        Vector3 topRight = startPosition + new Vector3(moveRangeWidth / 2f, 0f, moveRangeHeight / 2f);
        Vector3 bottomLeft = startPosition + new Vector3(-moveRangeWidth / 2f, 0f, -moveRangeHeight / 2f);
        Vector3 bottomRight = startPosition + new Vector3(moveRangeWidth / 2f, 0f, -moveRangeHeight / 2f);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
