using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update

    public BulletManager theBM = null;

    void Start()
    {
        theBM = GameObject.Find("Start").GetComponent<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(5f * Time.deltaTime,0,0));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("End"))
        {
            // 충돌한 총알을 큐에 다시 추가
            theBM.bulletQueue.Enqueue(this.gameObject);
            this.gameObject.SetActive(false); // 총알 비활성화
        }
    }
}
