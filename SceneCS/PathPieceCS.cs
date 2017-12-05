
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathPieceCS : MonoBehaviour
{

    private Vector3 currentPosition;

    PlayerMovement movement;
    GameBooleanManager GBM;
    GameSceneManager SM;

    float risetime;
    float falltime;
    Vector3 playerposition;
    // Use this for initialization
    void Start()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        SM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();

        currentPosition = transform.position;                                           //初始位置

        if (!(currentPosition.x == 10 && currentPosition.z == 10))
        {
            transform.position = new Vector3(currentPosition.z, -10, currentPosition.z);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (!GBM.GameWin && !GBM.GameLost)
        {
            if (transform.position == currentPosition) return;
            riseFunction();
        }
        else
        {
            if (GBM.GameWin)
            {
                if (SM.playerNowPoint.x != currentPosition.x || SM.playerNowPoint.z != currentPosition.z)
                {
                    fallFunction();
                }
            }
        }
    }

    void riseFunction()
    {
        if (risetime < 100)
        {
            risetime += 10 * Time.deltaTime;
        }
        playerposition = SM.playerNowPoint;
        if (Mathf.Abs(playerposition.x - currentPosition.x) < risetime && Mathf.Abs(playerposition.z - currentPosition.z) < risetime)
        {
            iTween.MoveTo(gameObject, currentPosition, 1f);
        }
    }

    void fallFunction()
    {
        currentPosition.y = -10;
        if (falltime < 100)
        {
            falltime += 10 * Time.deltaTime;
        }
        playerposition = SM.playerNowPoint;
        if (Mathf.Abs(playerposition.x - currentPosition.x) < falltime && Mathf.Abs(playerposition.z - currentPosition.z) < falltime)
        {
            iTween.MoveTo(gameObject, currentPosition, 4f);
        }

    }
}
