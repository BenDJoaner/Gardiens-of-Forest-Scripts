using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCS : MonoBehaviour
{

    GameSceneManager SM;
    Animator anim;
    GameBooleanManager GBM;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        SM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        transform.position = new Vector3(100, 100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (GBM.PlayerOnPosition)
        {
            anim.SetBool("active", false);
        }
        else
        {
            anim.SetBool("active", true);
        }

        if (GBM.GameLost)
        {
            anim.SetBool("active", false);
        }

        Vector3 fixposition;
        fixposition = SM.PlayerNextPoint;
        fixposition.y = fixposition.y +1;
        transform.position = fixposition;
    }

}
