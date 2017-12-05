/*
 * 紫海工作室程序Log：
 * 
 * BenDJoaner 2017年7月13
 * 1.调整布尔变量
 * 2.调整动画方法AnimatorControl
 * 3.开始搞巡逻函数PatrollingFunction
 * 
*/
using HighlightingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyAI : MonoBehaviour
{

    public Slider foundRateSlider;  //怀疑进度条
    public bool Dead;            //已经死亡
    public GameObject Canva;

    public bool isPatroling;            //正在巡逻
    public bool isTracking;         //发现玩家踪迹
    public bool isPursuing;            //正在追逐玩家
    public bool isCatch;          //已经抓到玩家
    //public bool shootplayer;          //瞄准并射击玩家
    //public bool firstFound;            //第一个找到
    //public bool FindCageOpened;//发现笼子打开
    //public bool stop4moment;

    float foundRate;            //怀疑度
    float delay;                     //通用延迟计时
    float timecount;            //通用计时器
    bool isme;                      // 本单位找到的
    bool standPosition;         //不走动的单位

    NavMeshAgent nav;
    Animator anim;
    Animator CanvaAnim;
    EnemiesManager EM;
    AudioSource AS;
    GameObject player;
    GameBooleanManager GBM;
    GameSceneManager GSM;
    SkillManager SKM;
    Vector3 HeadingPosition;
    Vector3 originPosition;

    string StrisWalking = "isWalking";
    string StrisRunning = "isRunning";
    string Strfound = "isFound";
    string StrCatch = "catch";
    string Strfall = "fall";
    string StrThrow = "throw";
    string StrActive = "active";
    string StrIncrease = "increase";



    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        CanvaAnim = Canva.GetComponent<Animator>();
        EM = GetComponentInParent<EnemiesManager>();

        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        SKM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SkillManager>();
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        HeadingPosition = EM.NavChanged();

        //初始化变量
        foundRate = 0;
        nav.speed = 0.7f;
        isPatroling = true;
        if (HeadingPosition.Equals(new Vector3(0, 0, 0)))
        {
            standPosition = true;
            originPosition = transform.position;
            HeadingPosition = originPosition;
        }
        nav.SetDestination(HeadingPosition);
        foundRateSlider.value = foundRate;
    }

    // Update is called once per frame
    void Update()
    {

        if (GBM.GameWin || !Dead)
        {
            HunterDead();
        }

        if (GBM.GamePause)
        {
            nav.speed = 0;
            return;
        }

        if ((GBM.EnemyStop && !isme && isPursuing) || GBM.PlayerSuper)
        {
            nav.SetDestination(transform.position);
            anim.SetBool(StrisRunning, false);
            CanvaAnim.SetBool(StrIncrease, false);
            isPursuing = false;
            anim.SetBool(StrisWalking, true);
            HeadingPosition = EM.NavChanged();
            isPatroling = true;
        }

        nav.SetDestination(HeadingPosition);
        if (!isPursuing)
        {
            PatrollingFunction();
        }
        else
        {
            PursuingFunction();
        }

        if (Dead)
        {
            timecount += Time.deltaTime;
            if (timecount > 1)
            {
                Destroy(this.gameObject);
            }
        }

        if (GBM.GameLost && !isCatch)
        {
            isPursuing = false;
            //nav.speed = 0.7f;
            anim.SetBool(StrisWalking, true);
            HeadingPosition = EM.NavChanged();
            isPatroling = true;
        }

    }

    //敌人追逐控制
    void PursuingFunction()
    {
        nav.speed = 3.3f;
        if (!GBM.PlayerHide)
        {
            HeadingPosition = player.transform.position;
        }

        if (nav.remainingDistance < 0.1)
        {
            //怀疑率减少
            anim.SetBool(Strfound, false);
            SighOnPlayerFunciton(false, 0);
            anim.SetBool(StrisRunning, false);
            anim.SetBool(StrisWalking, false);
            GBM.EnemyStop = true;
            isme = true;
            if (foundRate < 0)
            {
                isPursuing = false;
                //nav.speed = 0.7f;
                anim.SetBool(StrisWalking, true);
                HeadingPosition = EM.NavChanged();
                isPatroling = true;
                GBM.EnemyStop = false;
                isme = false;
            }
        }
    }

    //猎人巡逻控制
    void PatrollingFunction()
    {
        // 猎人与玩家坐标差的向量
        Vector3 targetDir = player.transform.position - transform.position;
        // 猎人与玩家的角度
        float sightangle = Vector3.Angle(transform.forward, targetDir);
        //猎人与玩家的距离
        float sightdistance = Vector3.Distance(player.transform.position, transform.position);

        //如果在玩家在猎人面前180度，距离signView米的时候
        if (sightangle < GameDataConfig.enemyViewAngle
            && sightdistance < GameDataConfig.enemyViewRange
            && !GBM.PlayerHide && !GBM.PlayerSuper)
        {
            //触发发现动画
            anim.SetBool(Strfound, true);
            CanvaAnim.SetBool(StrActive, true);
            AS.clip = AssetConfig.SurprisAudio;
            AS.Play();
            AS.loop = false;
            isTracking = true;
            isPatroling = false;
            delay = 0;
        }

        //猎人巡逻中
        if (isPatroling)
        {
            nav.speed = 0.7f;

            if (!AS.isPlaying)
            {
                AS.clip = AssetConfig.WalkAudio;
                AS.Play();
                AS.loop = true;
            }
            //如果猎人到达巡逻点则抽取下一个巡逻点
            if (!GBM.PlayerMoveing && nav.remainingDistance < 0.5)
            {
                if (standPosition)
                {
                    anim.SetBool(Strfound, false);
                    anim.SetBool(StrisWalking, false);
                    nav.speed = 0;
                }
                else
                {
                    anim.SetBool(StrisWalking, true);
                    HeadingPosition = EM.NavChanged();
                }
            }
        }

        //追踪玩家踪迹中
        if (isTracking)
        {
            //如果玩家不是隐藏状态而且在视野内
            if (!GBM.PlayerHide
                && sightdistance < GameDataConfig.enemyViewRange
                && sightangle < GameDataConfig.enemyViewAngle)
            {
                //设置目标点为玩家位置
                nav.speed = 0.7f;
                anim.SetBool(Strfound, true);
                HeadingPosition = player.transform.position;
                //怀疑率增加
                SighOnPlayerFunciton(true, sightdistance);
            }
            //如果玩家是隐藏状态 或者 玩家距离大于10
            else
            {
                //怀疑率减少
                SighOnPlayerFunciton(false, 0);
                //如果怀疑率少于0恢复巡逻状态
                if (foundRate < 0)
                {
                    //站立2秒后恢复巡逻状态
                    anim.SetBool(Strfound, false);
                    anim.SetBool(StrisWalking, false);
                    nav.speed = 0;
                    CanvaAnim.SetBool(StrActive, false);
                    delay += Time.deltaTime;

                    if (!AS.isPlaying)
                    {
                        AS.clip = AssetConfig.SighAudio;
                        AS.Play();
                        AS.loop = false;
                    }
                    if (delay > 2)
                    {
                        RecoverPatroling();
                    }
                }
            }

            if (foundRate >= 10)
            {
                isPursuing = true;
                anim.SetBool(StrisRunning, true);
            }
        }
    }


    //玩家出现在视野中函数
    void SighOnPlayerFunciton(bool active, float sightdistance)
    {
        if (active)
        {
            float signView = GameDataConfig.enemyViewRange;
            CanvaAnim.SetBool(StrActive, true);
            CanvaAnim.SetBool(StrIncrease, true);
            if (sightdistance < signView && foundRate < 10)
                foundRate += 10 * Time.deltaTime * (signView - sightdistance) / signView;
        }
        else
        {
            CanvaAnim.SetBool(StrIncrease, false);
            if (foundRate > 0)
                foundRate -= 2 * Time.deltaTime;
        }
        foundRateSlider.value = foundRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Dead) return;
        //如果不是坚韧活着无敌状态

        if (GSM.CharState == "MONKEY")
        {
            SKM.JSH_Enegy = 0;
            RecoverPatroling();
            return;
        }

        if (!GBM.PlayerTenacity && !GBM.PlayerInvincible)
        {
            CatchedFunction(other);
        }
        else
        {
            if (GBM.PlayerTenacity && (isPursuing || isTracking))
            {
                CatchedFunction(other);
            }
            else
            {
                Dead = true;
            }
        }
    }

    void RecoverPatroling()
    {
        anim.SetBool(StrisWalking, true);
        if (standPosition) HeadingPosition = originPosition;
        else HeadingPosition = EM.NavChanged();
        nav.speed = 0.7f;
        isTracking = false;
        isPatroling = true;
        delay = 0;
    }

    void HunterDead()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        nav.enabled = false;
        anim.SetTrigger(Strfall);
        Destroy(Canva.gameObject);
        AS.clip = AssetConfig.DeadAudio;
        AS.Play();
        AS.loop = false;
        Destroy(gameObject, 0.5f);
        return;
    }

    void CatchedFunction(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            AS.clip = AssetConfig.HaHaAudio;
            AS.Play();
            AS.loop = false;
            transform.LookAt(player.transform.position);
            anim.SetTrigger(StrCatch);
            nav.enabled = false;
            Destroy(Canva.gameObject);
            isCatch = true;
            GBM.GameLost = true;
            Destroy(this);
        }
    }
}
