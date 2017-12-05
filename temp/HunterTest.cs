using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterTest : MonoBehaviour
{
    EnemiesManager EM;
    NavMeshAgent nav;
    Animator anim;
    Vector3 HeadingPosition;

    // Use this for initialization
    void Start()
    {
        nav = GetComponentInChildren<NavMeshAgent>();
        EM = GetComponent<EnemiesManager>();
        anim = GetComponent<Animator>();
        HeadingPosition = EM.NavChanged();
        nav.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.remainingDistance < 0.1)
        {
            HeadingPosition = EM.NavChanged();
        }
        nav.SetDestination(HeadingPosition);
    }

    public void Running()
    {
        nav.speed = 3;

    }

    public void Walking()
    {
        nav.speed = 0.7f;
    }


    public void Standing()
    {
        nav.speed = 0;

    }
}
