using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    void OnMouseDown()
    {
        print(this.gameObject.name);
    }
}
