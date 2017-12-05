using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HunterAI : MonoBehaviour
{
    public Slider foundRateSlider;  //怀疑进度条
    public GameObject Canva;
    public bool isDead;
    public bool beDelay;
    public bool beLocked;
    public EnemyState m_EnemyState = EnemyState.isStanding;

    Vector3 originPosition;

    //自己身上的对象
    NavMeshAgent nav;
    Animator anim;
    Animator CanvaAnim;
    EnemiesManager EM;
    AudioSource AS;

    //要获取的对象
    GameObject player;
    GameBooleanManager GBM;
    GameSceneManager GSM;
    SkillManager SKM;
    MainUIManagerSC MUM;

    //动画语句
    string StrisWalking = "isWalking";
    string StrisRunning = "isRunning";
    string Strfound = "isFound";
    string StrCatch = "catch";
    string Strfall = "fall";
    string StrThrow = "throw";
    string StrActive = "active";
    string StrIncrease = "increase";

    bool isStandPosition;         //不走动的单位
    float foundRate;            //怀疑度
    float delay;                     //通用延迟计时
    bool isme;                      // 本单位找到的
    bool iCatch;          //已经抓到玩家

    //单位状态
    public enum EnemyState
    {
        //   站立        巡逻        寻找          追捕
        isStanding, isPatroling, isTracking, isPursuing
    }

    Vector3 targetDir;         // 猎人与玩家坐标差的向量
    float viewAngle;            // 猎人与玩家的角度
    float viewDistance;        //猎人与玩家的距离

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
        MUM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainUIManagerSC>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (EM.NavChanged().Equals(new Vector3(0, 0, 0)))
        {
            isStandPosition = true;
            originPosition = transform.position;
        }
        else
            nav.SetDestination(EM.NavChanged());
    }

    // Update is called once per frame
    void Update()
    {

        if (GBM.GameWin) EnemyDeadFunc();

        if (GBM.GamePause)
        {
            nav.speed = 0;
            return;
        }

        if (beDelay)
        {
            EnemyBeDelay();
            return;
        }
        if (m_EnemyState != EnemyState.isStanding
            && m_EnemyState != EnemyState.isPatroling
            && GBM.GameLost && !iCatch)
        {
            if (isStandPosition) nav.SetDestination(originPosition);
            else nav.SetDestination(EM.NavChanged());
            m_EnemyState = EnemyState.isStanding;
        }
        else if (GBM.GameLost && iCatch) return;

        if ((GBM.EnemyStop && !isme
                                && m_EnemyState == EnemyState.isPursuing))
        {
            StandingFunc();
            nav.SetDestination(transform.position);
            return;
            //m_EnemyState = EnemyState.isStanding;
            //if (isStandPosition) nav.SetDestination(originPosition);
            //else nav.SetDestination(EM.NavChanged());
        }

        targetDir = player.transform.position - transform.position;
        viewAngle = Vector3.Angle(transform.forward, targetDir);
        viewDistance = Vector3.Distance(player.transform.position, transform.position);

        switch (m_EnemyState)
        {
            case EnemyState.isStanding:
                StandingFunc();
                break;
            case EnemyState.isPatroling:
                PatrolingFunc();
                break;
            case EnemyState.isTracking:
                TrackingFunc();
                break;
            case EnemyState.isPursuing:
                PursuingFunc();
                break;
        }
    }

    //原地站立状态
    void StandingFunc()
    {
        if (nav.remainingDistance > 0.5f)
        {
            m_EnemyState = EnemyState.isPatroling;
            return;
        }
        nav.speed = 0;
        AS.Pause();
        ViewOnPlayerFunciton(false, 0);
        anim.SetBool(StrisRunning, false);
        anim.SetBool(StrisWalking, false);
        anim.SetBool(Strfound, false);
        CanvaAnim.SetBool(StrActive, false);
        ScanedPlayerFunc();
    }

    //巡逻状态
    void PatrolingFunc()
    {
        nav.speed = 0.7f;
        anim.SetBool(StrisRunning, false);
        anim.SetBool(StrisWalking, true);
        anim.SetBool(Strfound, false);
        ViewOnPlayerFunciton(false, 0);
        CanvaAnim.SetBool(StrActive, false);
        AudioPlayFunc(AssetConfig.WalkAudio);
        if (nav.remainingDistance < 0.5f)
        {
            if (isStandPosition)
            {
                m_EnemyState = EnemyState.isStanding;
                nav.SetDestination(originPosition);
            }
            else nav.SetDestination(EM.NavChanged());
        }
        ScanedPlayerFunc();
    }

    //寻找状态
    void TrackingFunc()
    {
        nav.speed = 0.7f;
        //如果玩家不是隐藏状态而且在视野内
        if (!GBM.PlayerHide && !GBM.PlayerSuper)
        //&& viewDistance < GameDataConfig.enemyViewRange
        //&& viewAngle < GameDataConfig.enemyViewAngle)
        {
            //设置目标点为玩家位置
            anim.SetBool(Strfound, true);
            nav.SetDestination(player.transform.position);
            //怀疑率增加
            ViewOnPlayerFunciton(true, viewDistance);
            GSM.playerLastPosition = player.transform.position;
        }
        //如果玩家是隐藏状态 或者 玩家距离大于10
        else
        {
            //怀疑率减少
            ViewOnPlayerFunciton(false, 0);
            //如果怀疑率少于0恢复巡逻状态
            if (foundRate < 0)
            {
                //站立1秒后恢复巡逻状态
                nav.speed = 0;
                anim.SetBool(Strfound, false);
                anim.SetBool(StrisWalking, false);
                delay += Time.deltaTime;
                AudioPlayFunc(AssetConfig.SighAudio);
                if (delay > 1)
                {
                    m_EnemyState = EnemyState.isStanding;
                    if (isStandPosition) nav.SetDestination(originPosition);
                    else nav.SetDestination(EM.NavChanged());
                    //GSM.AlramEnemy -= 1;
                    MUM.MessageShow("敌人放松了警惕");
                    delay = 0;
                }
            }
        }

        if (foundRate >= 10)
        {
            m_EnemyState = EnemyState.isPursuing;
            MUM.MessageShow("猎人追捕中！！！");
        }
    }

    //追捕状态
    void PursuingFunc()
    {
        nav.speed = 3.3f;
        if (!GBM.PlayerHide && !GBM.PlayerSuper)
        {
            nav.SetDestination(player.transform.position);
        }

        if (nav.remainingDistance < 0.1)
        {
            //怀疑率减少
            anim.SetBool(Strfound, false);
            ViewOnPlayerFunciton(false, 0);
            anim.SetBool(StrisRunning, false);
            anim.SetBool(StrisWalking, false);
            GBM.EnemyStop = true;
            isme = true;
            if (foundRate < 0)
            {
                if (isStandPosition) nav.SetDestination(originPosition);
                else nav.SetDestination(EM.NavChanged());
                m_EnemyState = EnemyState.isStanding;
                anim.SetBool(StrisWalking, true);
                CanvaAnim.SetBool(StrActive, false);
                GBM.EnemyStop = false;
                //GSM.AlramEnemy -= 1;
                MUM.MessageShow("猎人放弃追捕");
                isme = false;
            }
        }
        else
        {
            ViewOnPlayerFunciton(true, viewDistance);
            anim.SetBool(StrisRunning, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (beDelay)
        {
            if (GBM.PlayerTenacity || GBM.PlayerInvincible) EnemyDeadFunc();
            else return;
        }


        if (!GBM.PlayerTenacity && !GBM.PlayerInvincible)
        {
            ActiviTriggerByObj(other.gameObject);
        }
        else
        {
            Instantiate(AssetConfig.ChargeEffect_3, transform.position, Quaternion.identity);
            if (GBM.PlayerTenacity
                && (m_EnemyState == EnemyState.isTracking
                    || m_EnemyState == EnemyState.isPursuing))
            {
                ActiviTriggerByObj(other.gameObject);
            }
            else
            {
                EnemyDeadFunc();
            }
        }
    }

    /// <summary>
    /// 音频播放控制
    /// </summary>
    /// <param name="clip">从GameDataConfig取</param>
    void AudioPlayFunc(AudioClip clip)
    {
        if (!AS.isPlaying)
        {
            AS.clip = clip;
            AS.Play();
            AS.loop = false;
        }
    }

    /// <summary>
    /// 碰到东西时候激活
    /// </summary>
    void ActiviTriggerByObj(GameObject obj)
    {
        if (obj.tag == "Player")
        {
            switch (GSM.CharState)
            {
                case "CRANE":
                    delay += Time.deltaTime;
                    break;
                case "MONKEY":
                    if (isStandPosition) nav.SetDestination(originPosition);
                    else nav.SetDestination(EM.NavChanged());
                    m_EnemyState = EnemyState.isStanding;
                    anim.SetBool(StrisWalking, true);
                    CanvaAnim.SetBool(StrActive, false);
                    GBM.EnemyStop = false;
                    isme = false;
                    SKM.JSH_Enegy = 0;
                    break;
                case "PANGOLIN":
                    SKM.CSJ_Enegy = 0;
                    break;
                default:
                    AS.clip = AssetConfig.HaHaAudio;
                    AS.Play();
                    AS.loop = false;
                    transform.LookAt(player.transform.position);
                    anim.SetTrigger(StrCatch);
                    nav.enabled = false;
                    Destroy(Canva.gameObject);
                    iCatch = true;
                    GBM.PlayerBeCatch = true;
                    MUM.MessageShow("被捕......");
                    Destroy(this);
                    break;
            }
        }
        else if (obj.tag == "Hunter")
        {
            if (obj.GetComponent<HunterAI>().m_EnemyState == EnemyState.isPursuing)
            {
                m_EnemyState = EnemyState.isPursuing;
                nav.SetDestination(GSM.playerLastPosition);
                foundRate = 10;
            }
        }
    }

    /// <summary>
    /// 猎人死亡方法
    /// </summary>
    public void EnemyDeadFunc()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        nav.enabled = false;
        anim.SetTrigger(Strfall);
        Destroy(Canva.gameObject);
        AS.clip = AssetConfig.DeadAudio;
        AS.Play();
        AS.loop = false;
        Destroy(gameObject, 1);
        MUM.MessageShow("消灭敌人！");
        Destroy(this);
    }

    /// <summary>
    /// 控制敌人怀疑度的方法
    /// </summary>
    /// <param name="active">true是增加，false是减少</param>
    /// <param name="distance">根据玩家距离增加，减少为0</param>
    void ViewOnPlayerFunciton(bool active, float distance)
    {
        if (active)
        {
            float signView = GameDataConfig.enemyViewRange;
            CanvaAnim.SetBool(StrActive, true);
            CanvaAnim.SetBool(StrIncrease, true);
            if (distance < signView && foundRate < 10)
                foundRate += 10 * Time.deltaTime * (signView - distance) / signView;
        }
        else
        {
            CanvaAnim.SetBool(StrIncrease, false);
            if (foundRate > 0)
                foundRate -= 2 * Time.deltaTime;
        }
        foundRateSlider.value = foundRate;
    }

    /// <summary>
    /// 该单位是否发现玩家的方法
    /// </summary>
    void ScanedPlayerFunc()
    {
        if (GBM.GameLost) return;
        //如果在玩家在猎人面前180度，距离signView米的时候
        if (viewAngle < GameDataConfig.enemyViewAngle
            && viewDistance < GameDataConfig.enemyViewRange
            && !GBM.PlayerHide && !GBM.PlayerSuper)
        {
            m_EnemyState = EnemyState.isTracking;
            anim.SetBool(Strfound, true);
            CanvaAnim.SetBool(StrActive, true);
            AS.Pause();
            MUM.MessageShow("敌人被惊动");
            //GSM.AlramEnemy += 1;
            AudioPlayFunc(AssetConfig.SurprisAudio);
        }
    }

    /// <summary>
    /// 敌人被骚扰方法
    /// </summary>
    void EnemyBeDelay()
    {
        delay += Time.deltaTime;
        if (delay < 5)
        {
            //anim
            nav.speed = 0;
        }
        else
        {
            //anim
            delay = 0;
            beDelay = false;
        }
    }
}
