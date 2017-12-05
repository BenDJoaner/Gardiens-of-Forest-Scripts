using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTweenHashManager : MonoBehaviour
{

    public Hashtable args = new Hashtable();
    //public GameObject[] floor = new GameObject[] { };


    public Transform[] paths = new Transform[] { };
    // Use this for initialization
    void Start()
    {
        //floor = GameObject.FindGameObjectsWithTag("floor");

        //for (int i = 0; i < floor.Length; i++)
        //{
        //    print(i);
        //    paths[i] = floor[i].transform;
        //}

        args.Add("path", paths);
        args.Add("easeType", iTween.EaseType.linear);
        args.Add("speed", 10f);
        args.Add("movetopath", true);
        args.Add("orienttopath", true);

    }

}
