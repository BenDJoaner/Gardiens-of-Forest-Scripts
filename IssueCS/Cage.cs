/*
 * BenDJoaner 2017年7月11修改
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cage : MonoBehaviour
{
    private bool unlocking;               //是否解锁中
    private float prosses;                  // 解锁的进度

    public bool unlocked;                 //是否已经解锁
    public float UnlockTime;            //解锁时间
    public bool InRange;                  //玩家在范围内
    public Slider ProssesSlider;        //进度条
    public Text msg;
    public GameObject Canva;
    public GameObject MiniMapObj;
    Animator Canvanim;                 //HUD 面板动画
    Animator anim;                          //自己的动画
    GameBooleanManager GBM;   //状态记录管理器
    GameObject player;
    AudioSource AS;
    GameSceneManager GSM;                //场景管理器
    SkillManager SKM;

    private string openAudio = "Audio/CageOpen";

    // Use this for initialization
    void Start()
    {
        UnlockTime = 3;
        Canvanim = Canva.GetComponent<Animator>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        AS = GetComponent<AudioSource>();
        SKM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SkillManager>();
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //oldFunction();
        CageFunc();
    }

    public void CageFunc()
    {
        if (GBM.GamePause || GBM.GameLost) return;

        if (!unlocked)                                                                    //在没解锁的情况下
        {
            ProssesSlider.value = prosses;                      //进度条数值

            if (InRange)
            {
                if (!GBM.PlayerMoveing && !SKM.onSkillLaunch && !GBM.PlayerSuper)
                {
                    unlocking = true;
                }
                else
                {
                    GBM.PlayerUnlocking = false;
                    unlocking = false;
                    if (AS.isPlaying)
                        AS.Pause();
                    if (GSM != null)
                    {
                        switch (GSM.CharState)
                        {
                            case "CRANE":
                                msg.text = "在空中" + "\n" + " 不能解锁";
                                break;
                            case "PANGOLIN":
                                msg.text = "在地底" + "\n" + " 不能解锁";
                                break;
                            default:
                                msg.text = "停止走动" + "\n" + " 以解锁";
                                break;
                        }
                    }
                }
            }
            if (unlocking)                                                                //在解锁的情况下
            {
                if (prosses < UnlockTime)                                         //解锁时间没达到3秒
                {
                    GBM.PlayerUnlocking = true;
                    prosses += Time.deltaTime;
                    Canvanim.SetBool("active", true);
                    Vector3 lookpiont = transform.position;
                    lookpiont.y = player.transform.position.y;
                    player.transform.LookAt(lookpiont);
                    msg.text = "解锁中";
                    if (!AS.isPlaying)
                        AS.Play();
                }

                else
                {
                    GBM.PlayerUnlocking = false;
                    unlocked = true;                                                  //达到3秒，已经解锁为 true
                    Canvanim.SetBool("active", false);
                    anim.SetTrigger("active");
                    GSM.OPENEDCAGE();
                    msg.text = "解锁完成";
                    AS.clip = (AudioClip)Resources.Load(openAudio);
                    MiniMapObj.SetActive(false);
                    if (!AS.isPlaying)
                        AS.Play();
                }
            }
            else                                                                              //在离开范围的情况下
            {
                if (prosses > 0)
                {
                    prosses -= UnlockTime * Time.deltaTime;          //以2*unlocktime的倍的速度减少进度
                }
                else
                {
                    prosses = 0;                                                          //低过0的时候设为0
                    Canvanim.SetBool("active", false);
                }
            }

            //===========用范围判断star===========
            if (Vector3.Distance(transform.position, player.transform.position) < 1.5)
            {
                InRange = true;
            }
            else
            {
                InRange = false;
            }
            //============    end    ===============

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hunter")
        {

        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player" && !unlocked)
    //    {
    //        InRange = false;
    //        msg.text = "离开";
    //    }
    //}


}
