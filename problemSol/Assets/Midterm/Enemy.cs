using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f; // �̵� �ӵ�
    public float moveRangeWidth = 5f; // �̵� ������ ���� ����
    public float moveRangeHeight = 5f; // �̵� ������ ���� ����

    private Vector3 startPosition; // ���� ��ġ
    private Vector3 targetPosition; // ��ǥ ��ġ
    private bool movingForward = true; // ������ �̵� ������ ����

    public float rotationDuration = 3f; // ȸ���� �ɸ��� �ð� (��)
    private float currentRotationTime = 0f; // ���� ȸ�� �ð�

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

    // ���� ��ġ�� �߽����� ������ ��ġ�� �����Ͽ� ��ǥ ��ġ�� �����մϴ�.
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
                // 90�� ȸ�� ���� ���
                float angle = Mathf.Lerp(0, 90, currentRotationTime / rotationDuration); // ȸ�� ������ �ð��� ���� �����մϴ�.
                transform.rotation = Quaternion.Euler(0, angle, 0); // ȸ�� ������ �����մϴ�.

                // ȸ���� �Ϸ�Ǹ� -180�� ȸ������ ���� ��ȯ�մϴ�.
                if (currentRotationTime >= rotationDuration)
                {
                    isRotating90 = false;
                    currentRotationTime = 0f;
                    isRotating180 = true;
                }
            }
            else if (isRotating180)
            {
                // -180�� ȸ�� ���� ���
                float angle = Mathf.Lerp(90, -90, currentRotationTime / rotationDuration); // ȸ�� ������ �ð��� ���� �����մϴ�.
                transform.rotation = Quaternion.Euler(0, angle, 0); // ȸ�� ������ �����մϴ�.

                // ȸ���� �Ϸ�Ǹ� ȸ�� ���¸� �����մϴ�.
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
        // �̵� ����� �ӵ��� ���Ͽ� �̵� ���͸� ����
        Vector3 movement = (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
        // ���� ��ġ���� ��ǥ ��ġ�� �̵�
        transform.Translate(movement);

        // ���� ��ǥ ��ġ�� �����ϸ� �̵� ������ �ݴ�� �����Ͽ� �ٽ� ���� ��ġ�� �̵�
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

    // ������ �󿡼� �̵� ������ ǥ���ϱ� ���� OnDrawGizmos �Լ��� ����մϴ�.
    private void OnDrawGizmos()
    {
        // �Ķ������� �̵� ������ ǥ���մϴ�.
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
