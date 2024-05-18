using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    int rayCastLenght = 5;
    RaycastHit hit;
    public Animator animator;

    public PlayerController thepc;

    void Start()
    {
        Animator animator = GetComponent<Animator>();
        thepc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Physics.Raycast(transform.position, -transform.right, out hit, rayCastLenght))
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            { 
                if(thepc.key >= 4){
                    animator.SetBool("isOpen",true);        
                }
            }
        }

        
    }

}
