using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStrucuture;

public class StackManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    public DataStrucuture.Stack<GameObject> bulletQueue = new DataStrucuture.Stack<GameObject>();
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
                GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
                bullet.SetActive(false);
                bulletQueue.Push(bullet);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        // 스택에서 다음 발사할 프리팹 가져오기 
        GameObject bullet = (GameObject)bulletQueue.Pop();
        bullet.SetActive(true);
        bullet.transform.position = transform.position;
        
    }
}
