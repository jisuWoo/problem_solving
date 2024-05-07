using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public Transform Startpoint;
    public Transform player; // 플레이어 Transform
    public Transform cube;
    public Rigidbody rb;

    public float rotationSpeed = 90f; // 회전 속도 (각도/초)
    public float moveSpeed = 5f; // 이동 속도

    private bool isRotating = false; // 회전 중인지 여부를 나타내는 변수
    private float targetAngle = 0f; // 목표 회전 각도를 저장하는 변수
    private float currentAngle = 0f; // 현재 회전 각도를 저장하는 변수
    private float rotateTime = 1f; // 회전에 걸리는 시간


   
    bool GameEnd = false;

    private void Start()
    {
        this.transform.position = new Vector3(Startpoint.position.x, transform.position.y, Startpoint.position.z);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Q 키를 누를 때 카메라를 45도 회전
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

        // 플레이어를 이동 방향으로 이동
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        //Vector3 worldMovement = player.TransformDirection(localMovement);
        rb.MovePosition(transform.position + (movement * 10f));

        // 회전 중일 때 회전 처리
        if (isRotating)
        {
            float rotationAmount = (targetAngle - currentAngle) / rotateTime * Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.up, rotationAmount);
            currentAngle += rotationAmount;
            //cube.Rotate(Vector3.up, rotationAmount);

            // 회전이 완료되면 회전 종료
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("End"))
        {
           GameEnd = true;
        }
    }
}
