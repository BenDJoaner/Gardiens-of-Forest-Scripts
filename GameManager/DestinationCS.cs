using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationCS : MonoBehaviour
{
    public GameObject DestinationCanva;
    GameObject player;
    GameBooleanManager GBM;
    GameSceneManager GSM;
    Vector3 currenPosition;
    //SkillManager SKM;
    bool flag;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        currenPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (GBM.GameLost)
        {
            Destroy(gameObject);
        }

        currenPosition.y = player.transform.position.y;

        if (flag)
        {
            player.GetComponent<PlayerMovement>().SetTarget(transform);
            Vector3 temp = GSM.playerNowPoint;
            temp.y = transform.position.y;
            if (Vector3.Distance(temp, transform.position) < 0.1)
            {
                GBM.GameWin = true;
            }
        }

        if (GSM.CharState != "RHINOCEROS")
        {
            DestinationCanva.GetComponent<Animator>().SetBool("show", true);
            return;
        }
        else
        {
            DestinationCanva.GetComponent<Animator>().SetBool("show", false);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 1.5)
        {
            flag = true;
        }
    }
}
