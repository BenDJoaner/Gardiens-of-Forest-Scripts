using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class targetgroup : MonoBehaviour
{
    CinemachineTargetGroup t_group;
    GameObject[] enemis = { };
    // Use this for initialization
    void Start()
    {
        t_group = GetComponent<CinemachineTargetGroup>();

    }

    // Update is called once per frame
    void Update()
    {
        print(t_group);
    }
}
