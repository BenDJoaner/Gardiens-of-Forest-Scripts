using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{

    //public GameObject[] Enemies = new GameObject[] { };
    public Transform[] NavPoint = new Transform[] { };
    public int GlobleRandom;
    //public bool isRandom;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        GlobleRandom = 1;
    }



    public Vector3 NavChanged()
    {
        Vector3 temp;
        for (int i = 0; i < NavPoint.Length; i++)
        {
            int RandNum = Random.Range(0, NavPoint.Length);
            if (GlobleRandom != RandNum)
            {
                GlobleRandom = RandNum;
                break;
            }
        }
        if (NavPoint.Length == 0) temp = new Vector3(0, 0, 0);
        else temp = NavPoint[GlobleRandom].position;
        return temp;
    }

}
