using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    GameObject currentTarget = null;
    NavMeshAgent nav;
    GameBooleanManager GBM;
    GameSceneManager GSM;
    AudioSource AS;
    AnimControl AC;

    // Use this for initialization

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        AC = GetComponent<AnimControl>();
        AS = GetComponent<AudioSource>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GBM.PlayerBeCatch)
        {
            AC.SetAnimState("Catch");
            this.enabled = false;
            nav.enabled = false;
            AS.Pause();
            return;
        }

        if (GBM.PlayerFall)
        {
            AC.SetAnimState("Fall");
            this.enabled = false;
            nav.enabled = false;
            AS.Pause();
            return;
        }

        if (GBM.GameWin)
        {
            AC.SetAnimState("notWalking");
            nav.enabled = false;
            this.enabled = false;
            AS.Pause();
            return;
        }

        if (GBM.GamePause)
        {
            AC.SetAnimState("notWalking");
            nav.speed = 0;
            nav.SetDestination(transform.position);
            GBM.PlayerMoveing = false;
            AS.Pause();
            return;
        }

        nav.speed = GameDataConfig.moveSpeed;

        if (!GBM.GameLost && nav.remainingDistance > 0.1)                 //行走动画控制
        {
            if (!AS.isPlaying)
            {
                AS.Play();
            }
            if (GSM.CharState == "CRANE") AC.XNMode.GetComponent<Animator>().SetBool("isHanging", true);
            AC.SetAnimState("isWalking");
            GBM.PlayerMoveing = true;
        }
        else
        {
            AS.Pause();
            if (GSM.CharState == "CRANE") AC.XNMode.GetComponent<Animator>().SetBool("isHanging", false);
            AC.SetAnimState("notWalking");
            GBM.PlayerMoveing = false;
        }

        if (GBM.PlayerUnlocking)                                //解锁动画控制
        {
            AC.SetAnimState("isUnlocking");
        }
        else
        {
            AC.SetAnimState("noUnlocking");
        }
    }

    //角色移动函数
    public void SetTarget(Transform target)
    {
        if (GBM.GameLost || GBM.GameWin || GBM.GamePause) return;
        GBM.PlayerMoveing = false;
        GSM.PlayerNextPoint = target.position;
        if (nav.enabled) nav.SetDestination(target.position);
    }
}
