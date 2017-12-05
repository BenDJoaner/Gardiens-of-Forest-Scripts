using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour {
    public float Rate;
    GameObject stoneObj;
    float cdTime;
	// Use this for initialization
	void Start () {
        stoneObj = AssetConfig.StoneObj;
	}
	
	// Update is called once per frame
	void Update () {
		if(cdTime <= Rate)
        {
            cdTime += Time.deltaTime;
        }
        else
        {
            cdTime = 0;
            Instantiate(stoneObj, transform.position,Quaternion.identity);
        }
	}
}
