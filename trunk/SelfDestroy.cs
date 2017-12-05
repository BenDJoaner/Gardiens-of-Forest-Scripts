using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float destroy_Time;

    private float activeTime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        activeTime += Time.deltaTime;
        if (activeTime > destroy_Time)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
