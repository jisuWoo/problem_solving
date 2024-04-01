using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletManager : MonoBehaviour
    {
        public GameObject bulletPrefab; // 발사할 프리팹

        public LinkedQueue<GameObject> bulletQueue = new LinkedQueue<GameObject>(); // 프리팹을 저장할 큐

        void Start()
        {
            // 큐에 프리팹을 추가
            for (int i = 0; i < 10; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
                bullet.SetActive(false);
                bulletQueue.Enqueue(bullet);
            }
        }

        void Update()
        {
            // 마우스 클릭 시 발사
            if (Input.GetMouseButtonDown(0))
            {
                FireBullet();
            }
        }

        void FireBullet()
        {
            // 큐에서 다음 발사할 프리팹 가져오기
            if (!bulletQueue.IsEmpty())
            {
                GameObject bullet = (GameObject)bulletQueue.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = transform.position;
            }
            else
            {
                Debug.LogWarning("No more bullets available.");
            }
        }
        
    }