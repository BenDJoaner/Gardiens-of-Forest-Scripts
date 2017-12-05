using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevelopDebuger : MonoBehaviour
{
    public GameObject Panel;
    public Text Msg;

    public Image Slipicon;//快跑
    public Image Chargericon;//冲撞
    public Image Tunnelicon;//打洞
    public Image Bananaicon;//仍香蕉
    public Image Harassmenticon;//骚扰
    public Image Stabicon;//俯冲

    GameBooleanManager GBM;
    GameSceneManager SM;
    MainUIManagerSC MUM;

    [HideInInspector]
    public Sprite SlipImage;
    [HideInInspector]
    public Sprite SlipImage_2;
    [HideInInspector]
    public Sprite ChargerImage;
    [HideInInspector]
    public Sprite ChargerImage_2;
    [HideInInspector]
    public Sprite portalImage;
    [HideInInspector]
    public Sprite portalImage_2;
    [HideInInspector]
    public Sprite BananaImage;
    [HideInInspector]
    public Sprite BananaImage_2;
    [HideInInspector]
    public Sprite HarassmentImage;
    [HideInInspector]
    public Sprite HarassmentImage_2;
    [HideInInspector]
    public Sprite StabImage;
    [HideInInspector]
    public Sprite StabImage_2;
    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("debuger") != this.gameObject)
            GameObject.Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        Panel.SetActive(false);
        LoadResource();
        LoadSKillUnlock();
    }

    // Update is called once per frame
    void Update()
    {
        CouculateFPS();
        Msg.text = GetMsg();
    }

    public void showDebug()
    {
        if (Panel.activeSelf)
        {
            Panel.SetActive(false);
        }
        else
        {
            GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
            SM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
            MUM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainUIManagerSC>();
            Panel.SetActive(true);
        }
    }

    //读取图片资源
    void LoadResource()
    {
        SlipImage = Resources.Load<Sprite>("HUD/Snip");
        SlipImage_2 = Resources.Load<Sprite>("HUD/Snip_2");
        ChargerImage = Resources.Load<Sprite>("HUD/Charger");
        ChargerImage_2 = Resources.Load<Sprite>("HUD/Charger_2");
        portalImage = Resources.Load<Sprite>("HUD/portal");
        portalImage_2 = Resources.Load<Sprite>("HUD/portal_2");
        BananaImage = Resources.Load<Sprite>("HUD/Banana_3");
        BananaImage_2 = Resources.Load<Sprite>("HUD/Banana_4");
        HarassmentImage = Resources.Load<Sprite>("HUD/Harassment");
        HarassmentImage_2 = Resources.Load<Sprite>("HUD/Harassment_2");
        StabImage = Resources.Load<Sprite>("HUD/Stab");
        StabImage_2 = Resources.Load<Sprite>("HUD/Stab_2");
    }

    //读取已经解锁的技能和角色
    public void LoadSKillUnlock()
    {
        Slipicon.GetComponent<Image>().sprite = SlipImage_2;
        Chargericon.GetComponent<Image>().sprite = ChargerImage_2;
        Tunnelicon.GetComponent<Image>().sprite = portalImage_2;
        Bananaicon.GetComponent<Image>().sprite = BananaImage_2;
        Stabicon.GetComponent<Image>().sprite = StabImage_2;
        Harassmenticon.GetComponent<Image>().sprite = HarassmentImage_2;
        for (int i = 1; i <= 6; i++)
        {
            if (PlayerPrefs.GetInt("skill" + i) == 1)
            {
                switch (i)
                {
                    case 1:
                        Slipicon.GetComponent<Image>().sprite = SlipImage;
                        break;
                    case 2:
                        Chargericon.GetComponent<Image>().sprite = ChargerImage;
                        break;
                    case 3:
                        Tunnelicon.GetComponent<Image>().sprite = portalImage;
                        break;
                    case 4:
                        Bananaicon.GetComponent<Image>().sprite = BananaImage;
                        break;
                    case 5:
                        Harassmenticon.GetComponent<Image>().sprite = HarassmentImage;
                        break;
                    case 6:
                        Stabicon.GetComponent<Image>().sprite = StabImage;
                        break;
                }
            }
        }
    }

    //计算帧数
    float fps;
    float current;
    float dtime;
    float timesum;
    string tip;

    void CouculateFPS()
    {
        current++;
        dtime += Time.deltaTime;
        timesum += Time.deltaTime;

        if (dtime > 1)
        {
            fps = current / dtime;
            current = 0;
            dtime = 0;
        }
    }

    string GetMsg()
    {
        if (!GBM || !SM) return "无关卡数据";

        return
            (int)fps + " fps" + "\n" +
            "玩家解锁笼子：" + GBM.PlayerUnlocking + "\n" +
            "打开所有笼子：" + GBM.OpenAllCage + "\n" +
            "目前坐标：X：" + SM.playerNowPoint.x + "，Y:" + SM.playerNowPoint.z + "\n" +
            "下一个坐标：X：" + SM.PlayerNextPoint.x + "，Y:" + SM.PlayerNextPoint.z;
    }

    public void SkillButton(int typ)
    {
        if (!GBM || !SM) return;

        //四个特殊技能处理=======start
        if (typ == 1 && PlayerPrefs.GetInt("skill" + 2) == 1) return;
        if (typ == 2 && PlayerPrefs.GetInt("skill" + 1) != 1) return;
        if (typ == 5 && PlayerPrefs.GetInt("skill" + 6) == 1) return;
        if (typ == 6 && PlayerPrefs.GetInt("skill" + 5) != 1) return;
        //两个特殊技能处理=======end

        if (PlayerPrefs.GetInt("skill" + typ) == 1)
        {
            PlayerPrefs.SetInt("skill" + typ, 0);
        }
        else
        {
            PlayerPrefs.SetInt("skill" + typ, 1);
        }
        MUM.LoadSKillUnlock();
        LoadSKillUnlock();
    }

    public void clearData()
    {
        PlayerPrefs.DeleteAll();
        Application.LoadLevel(Application.loadedLevel);
    }
}
