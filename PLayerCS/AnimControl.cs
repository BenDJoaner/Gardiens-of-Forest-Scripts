using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimControl : MonoBehaviour
{

    public GameObject XNMode;
    public GameObject DDHMode;
    public GameObject JSHMode;
    public GameObject CSJMode;

    public Transform CamFollowPiont;
    public Transform CamFollowObj;

    Animator anim;
    SkillManager SKM;
    GameSceneManager GSM;
    GameBooleanManager GBM;
    HUDManager HUD;

    Vector3 Record = new Vector3(1, 1, 1);//纪录恢复犀牛后玩家的位置
    Vector3 CameraPosition;
    Vector3 XNPosition;

    GameObject camera;

    // Use this for initialization
    void Start()
    {
        anim = XNMode.GetComponent<Animator>();
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
        SKM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SkillManager>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        HUD = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDManager>();
        camera = Camera.main.gameObject;
        DDHMode.SetActive(false);
        JSHMode.SetActive(false);
        CSJMode.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GSM.CharState == "CRANE" || GSM.CharState == "RHINOCEROS")
        {
            Record = transform.position;
        }
    }

    /// <summary>
    /// 设置当前角色
    /// </summary>
    /// <param name="data">Data.</param>
    public void setChar(string data = "RHINOCEROS")
    {
        if (GBM.GameWin || GBM.GameLost || GBM.GamePause) return;
        Instantiate(AssetConfig.CharChangeEffect, transform.position, Quaternion.identity);
        switch (data)
        {
            case "CRANE":
                XNMode.SetActive(true);
                DDHMode.SetActive(true);
                SetAnimState("notWalking");
                SetAnimState("onUnlocking");
                anim = DDHMode.GetComponent<Animator>();
                GetComponent<CapsuleCollider>().enabled = false;
                XNPosition = transform.position;
                XNPosition.y = transform.position.y + 2.5f;
                XNMode.transform.position = XNPosition;
                OnCameraChange(-1, 5, 3);
                HUD.setPCMessage("丹顶鹤", GameDataConfig.ColorWite);
                GSM.BabiesVisible(false);
                break;
            case "MONKEY":
                XNMode.SetActive(false);
                JSHMode.SetActive(true);
                anim = JSHMode.GetComponent<Animator>();
                Record = transform.position;
                HUD.setPCMessage("金丝猴", GameDataConfig.ColorWite);
                break;
            case "PANGOLIN":
                XNMode.SetActive(false);
                CSJMode.SetActive(true);
                anim = CSJMode.GetComponent<Animator>();
                Record = transform.position;
                HUD.setPCMessage("穿山甲", GameDataConfig.ColorWite);
                OnCameraChange(1, -3, -3);
                GSM.BabiesVisible(false);
                break;
            default:
                XNMode.SetActive(true);
                DDHMode.SetActive(false);
                JSHMode.SetActive(false);
                CSJMode.SetActive(false);
                GSM.BabiesVisible(true);
                CameraPosition = transform.position;
                CamFollowPiont.position = CamFollowObj.position;
                GetComponent<NavMeshAgent>().enabled = false;
                transform.position = Record;
                GSM.MoveBabies(Record);
                XNPosition = transform.position;
                XNMode.transform.position = XNPosition;
                GetComponent<CapsuleCollider>().enabled = true;
                GetComponent<NavMeshAgent>().enabled = true;
                GetComponent<NavMeshAgent>().SetDestination(Record);
                anim = XNMode.GetComponent<Animator>();
                SetAnimState("NoHanging");
                GSM.PlayerNextPoint = Record;
                break;
        }
    }

    /// <summary>
    /// 摄像机位置
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="z">The z coordinate.</param>
    public void OnCameraChange(float x, float y, float z)
    {
        CameraPosition.x = CamFollowObj.position.x + x;
        CameraPosition.y = CamFollowObj.position.y + y;
        CameraPosition.z = CamFollowObj.position.z + z;
        CamFollowPiont.position = CameraPosition;
    }

    /// <summary>
    /// 穿山甲
    /// </summary>
    public void OnDeegAHole(int step)
    {
        switch (step)
        {
            case 1:
                OnCameraChange(-3, 5, 7);
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                HUD.ShowRangAnim(true);
                break;
            case 2:
                OnCameraChange(0, 0, 0);
                transform.position = GSM.PlayerNextPoint;
                GetComponent<NavMeshAgent>().enabled = true;
                GetComponent<PlayerMovement>().enabled = true;
                HUD.ShowRangAnim(false);
                break;
            case 3:
                OnCameraChange(0, 0, 0);
                GetComponent<NavMeshAgent>().enabled = true;
                GetComponent<PlayerMovement>().enabled = true;
                HUD.ShowRangAnim(false);
                break;
        }
    }

    /// <summary>
    /// 猴子原地放香蕉皮
    /// </summary>
    /// <param name="obj">Object.</param>
    public void OnThrowSometing(GameObject obj)
    {
        GameObject temp = Instantiate(obj);
        temp.transform.position = transform.position;
    }

    /// <summary>
    /// 丹顶鹤攻击并移动到角色和敌人之间随机位置
    /// </summary>
    /// <param name="obj">Object.</param>
    public void OutNavmeshTranslate(GameObject obj)
    {
        obj.GetComponent<HunterAI>().beDelay = true;
        //复制一个丹顶鹤骚扰模型
        GameObject DDHObj = Instantiate(AssetConfig.DDHObj);
        DDHObj.transform.position = obj.transform.position;
        Destroy(DDHObj, 4);
    }

    /// <summary>
    /// 动画控制
    /// </summary>
    /// <param name="state">State.</param>
    public void SetAnimState(string state)
    {
        switch (state)
        {
            case "isWalking":
                anim.SetBool("isWalking", true);
                break;
            case "notWalking":
                anim.SetBool("isWalking", false);
                break;

            case "isUnlocking":
                anim.SetBool("isUnlocking", true);
                break;
            case "noUnlocking":
                anim.SetBool("isUnlocking", false);
                break;

            case "isRunning":
                anim.SetBool("isRunning", true);
                break;
            case "NoRunning":
                anim.SetBool("isRunning", false);
                break;

            case "isHanging":
                anim.SetBool("isHanging", true);
                break;
            case "NoHanging":
                anim.SetBool("isHanging", false);
                break;

            case "Charge":
                anim.SetTrigger("Charge");
                break;
            case "Catch":
                anim.SetTrigger("Catch");
                break;
            case "Fall":
                anim.SetTrigger("Fall");
                break;
        }
    }
}
