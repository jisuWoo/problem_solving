using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f; // 이동 속도
    public float moveRangeWidth = 5f; // 이동 범위의 가로 길이
    public float moveRangeHeight = 5f; // 이동 범위의 세로 길이

    private Vector3 startPosition; // 시작 위치
    private Vector3 targetPosition; // 목표 위치
    private bool movingForward = true; // 앞으로 이동 중인지 여부

    public float rotationDuration = 3f; // 회전에 걸리는 시간 (초)
    private float currentRotationTime = 0f; // 현재 회전 시간

    private Camera Ecamera;
    bool isChasing = false;
    bool isRotating90 = false;
    bool isRotating180 = false;
    private void Start()
    {
        startPosition = transform.position;
        Ecamera = GetComponent<Camera>();
        ChooseRandomTargetPosition();
    }

    private void Update()
    {
        if (IsTargetVisible(Ecamera, player) == false)
        {

            EnemyRotate();
            if (isRotating90 == false && isRotating180 == false)
            {

                EnemyMove();
            }

        }
        else
        {
            playerchase();
            isRotating90 = true;
            currentRotationTime = 0f;
        }
    }

    // 시작 위치를 중심으로 랜덤한 위치를 선택하여 목표 위치로 설정합니다.
    private void ChooseRandomTargetPosition()
    {
        float randomX = Random.Range(startPosition.x - moveRangeWidth / 2f, startPosition.x + moveRangeWidth / 2f);
        float randomZ = Random.Range(startPosition.z - moveRangeHeight / 2f, startPosition.z + moveRangeHeight / 2f);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    private void EnemyRotate()
    {

        if (isRotating90 || isRotating180)
        {
            currentRotationTime += Time.deltaTime;

            if (isRotating90)
            {
                // 90도 회전 중인 경우
                float angle = Mathf.Lerp(0, 90, currentRotationTime / rotationDuration); // 회전 각도를 시간에 따라 보간합니다.
                transform.rotation = Quaternion.Euler(0, angle, 0); // 회전 각도를 설정합니다.

                // 회전이 완료되면 -180도 회전으로 상태 전환합니다.
                if (currentRotationTime >= rotationDuration)
                {
                    isRotating90 = false;
                    currentRotationTime = 0f;
                    isRotating180 = true;
                }
            }
            else if (isRotating180)
            {
                // -180도 회전 중인 경우
                float angle = Mathf.Lerp(90, -90, currentRotationTime / rotationDuration); // 회전 각도를 시간에 따라 보간합니다.
                transform.rotation = Quaternion.Euler(0, angle, 0); // 회전 각도를 설정합니다.

                // 회전이 완료되면 회전 상태를 해제합니다.
                if (currentRotationTime >= rotationDuration)
                {
                    isRotating180 = false;
                    currentRotationTime = 0f;
                }
            }
        }
    }

    private void EnemyMove()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
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

    public bool IsTargetVisible(Camera _camera, Transform _transform)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        var point = _transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

    private void playerchase()
    {
        Vector3 playerDirection = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30f * Time.deltaTime);

        Vector3 movement = playerDirection * 5f * Time.deltaTime;
        transform.Translate(movement);
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
