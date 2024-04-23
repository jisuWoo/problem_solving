using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public Transform cube;

    public float rotationSpeed = 90f; // ȸ�� �ӵ� (����/��)
    public float moveSpeed = 5f; // �̵� �ӵ�

    private bool isRotating = false; // ȸ�� ������ ���θ� ��Ÿ���� ����
    private float targetAngle = 0f; // ��ǥ ȸ�� ������ �����ϴ� ����
    private float currentAngle = 0f; // ���� ȸ�� ������ �����ϴ� ����
    private float rotateTime = 1f; // ȸ���� �ɸ��� �ð�

    void Update()
    {
        // Q Ű�� ���� �� ī�޶� 45�� ȸ��
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartRotation(-90f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartRotation(90f);
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �÷��̾ �̵� �������� �̵�
        Vector3 localMovement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        //Vector3 worldMovement = player.TransformDirection(localMovement);
        transform.Translate(localMovement);

        // ȸ�� ���� �� ȸ�� ó��
        if (isRotating)
        {
            float rotationAmount = (targetAngle - currentAngle) / rotateTime * Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.up, rotationAmount);
            currentAngle += rotationAmount;
            //cube.Rotate(Vector3.up, rotationAmount);

            // ȸ���� �Ϸ�Ǹ� ȸ�� ����
            if (Mathf.Abs(currentAngle - targetAngle) < 0.01f)
            {
                isRotating = false;
            }
        }
    }

    void StartRotation(float angle)
    {
        isRotating = true;
        targetAngle = currentAngle + angle;
    }
}
