using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // �̵� �ӵ�
    public float moveRangeWidth = 5f; // �̵� ������ ���� ����
    public float moveRangeHeight = 5f; // �̵� ������ ���� ����

    private Vector3 startPosition; // ���� ��ġ
    private Vector3 targetPosition; // ��ǥ ��ġ
    private bool movingForward = true; // ������ �̵� ������ ����

    private void Start()
    {
        startPosition = transform.position;
        ChooseRandomTargetPosition();
    }

    private void Update()
    {
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

    // ���� ��ġ�� �߽����� ������ ��ġ�� �����Ͽ� ��ǥ ��ġ�� �����մϴ�.
    private void ChooseRandomTargetPosition()
    {
        float randomX = Random.Range(startPosition.x - moveRangeWidth / 2f, startPosition.x + moveRangeWidth / 2f);
        float randomZ = Random.Range(startPosition.z - moveRangeHeight / 2f, startPosition.z + moveRangeHeight / 2f);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
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
