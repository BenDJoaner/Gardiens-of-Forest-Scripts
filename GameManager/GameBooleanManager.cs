using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameBooleanManager : MonoBehaviour
{
    public bool PlayerUnlocking;            //玩家正在解锁
    public bool PlayerMoveing;              //玩家在移动中
    public bool PlayerBeCatch;              //玩家被抓住
    public bool PlayerFall;                         //玩家掉下悬崖
    public bool OpenAllCage;                //玩家已经打开所有笼子
    public bool GameWin;                    //游戏胜利
    public bool GameLost;                    //游戏失败
    public bool PlayerOnPosition;        //玩家到达指定位置
    public bool GamePause;                 //游戏暂停
    public bool EnemyStop;                  //敌人停止追逐
    public bool SoundOn;                     //声音开关

    //==============法典规则封装(Main)================
    private bool playerExpose;              //暴露状态
    public bool PlayerExpose
    {
        get { return playerExpose; }
        set { playerExpose = value; }
    }

    private bool playerHide;                  //隐藏状态
    public bool PlayerHide
    {
        get { return playerHide; }
        set { playerHide = value; }
    }

    private bool playerTenacity;            //坚韧状态
    public bool PlayerTenacity
    {
        get { return playerTenacity; }
        set { playerTenacity = value; }
    }

    private bool playerInvincible;           //无敌状态
    public bool PlayerInvincible
    {
        get { return playerInvincible; }
        set { playerInvincible = value; }
    }

    private bool playerSuper;                //超体状态
    public bool PlayerSuper
    {
        get { return playerSuper; }
        set { playerSuper = value; }
    }
    //==============法典规则封装================

    GameObject _player;

    // Use this for initialization
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        SoundOn = PlayerPrefs.GetInt("SoundOn") == 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameWin || GameLost) return;
        if (PlayerBeCatch || PlayerFall) GameLost = true;
        //如果玩家离目的位置1.5米则玩家到达目的点

        if (_player.GetComponent<NavMeshAgent>().enabled&&
            _player.GetComponent<NavMeshAgent>().remainingDistance < 1.5f)
        {
            PlayerOnPosition = true;
        }
        else
        {
            PlayerOnPosition = false;
        }
    }


}
