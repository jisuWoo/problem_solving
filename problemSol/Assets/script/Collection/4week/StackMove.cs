using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMove : MonoBehaviour
{
    // Start is called before the first frame update
    public StackManager theSM = null;
    void Start()
    {
        theSM = GameObject.Find("Start").GetComponent<StackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(5f * Time.deltaTime,0,0));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("End"))
        {
            // 충돌한 총알을 스택에 다시 추가
            theSM.bulletQueue.Push(this.gameObject);
            this.gameObject.SetActive(false); // 총알 비활성화
        }
    }
}
