using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MenuPlayerMovement : MonoBehaviour
{
    NavMeshAgent nav;
    Animator anim;
    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        if (!PlayerPrefs.HasKey("LastPoint"))
        {
            nav.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.enabled)
        {
            if (nav.remainingDistance > 0.1)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }
    }

    //角色移动函数
    public void SetTarget(Transform target)
    {
        if (nav.enabled) nav.SetDestination(target.position);
    }
}
