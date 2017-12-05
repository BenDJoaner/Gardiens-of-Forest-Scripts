using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class MainUIManagerSC : MonoBehaviour
{
    public string LevelName;
    public Text LevelNameText;
    public Text SumPoint;
    public Text Tips;
    public GameObject NewLevelIcon;
    public Image CharSelectBG;
    public Button SoundButton;
    public Button SelectCharBtn;

    //===========UI动画=================
    public GameObject PuaseGroup;
    public GameObject SkillGroup;
    public GameObject MessageGroup;

    Animator puaseAnim;
    Animator SkillAnim;
    Animator MessageAnim;

    //===========暂停菜单角色/技能icon===============
    public Image Slipicon;//快跑
    public Image Chargericon;//冲撞
    public Image Tunnelicon;//打洞
    public Image Bananaicon;//仍香蕉
    public Image Harassmenticon;//骚扰
    public Image Stabicon;//俯冲

    //===========角色/技能UI相关===============

    public GameObject DDHButton;
    public GameObject JSHButton;
    public GameObject CSJButton;

    bool Gamepusing;
    bool Skillopened;
    bool unlockDDH;
    bool unlockJSH;
    bool unlockCSJ;
    Blur blur;
    GameObject MainCamera;
    GameBooleanManager GBM;
    GameSceneManager GSM;
    SkillManager SKM;
    HUDManager HUD;
    AudioListener audioListener;
    Image AudioImage;

    [HideInInspector]
    public Sprite XNSkillIcon_1;
    [HideInInspector]
    public Sprite XNSkillIcon_2;
    [HideInInspector]
    public Sprite DDHSkillIcon_1;
    [HideInInspector]
    public Sprite DDHSkillIcon_2;
    [HideInInspector]
    public Sprite JSHSkillIcon_1;
    [HideInInspector]
    public Sprite JSHSkillIcon_2;
    [HideInInspector]
    public Sprite CSJSkillIcon_1;
    [HideInInspector]
    public Sprite CSJSkillIcon_2;

    // Use this for initialization
    void Start()
    {
        GBM = GetComponent<GameBooleanManager>();
        GSM = GetComponent<GameSceneManager>();
        SKM = GetComponent<SkillManager>();
        HUD = GetComponent<HUDManager>();
        MainCamera = Camera.main.gameObject;
        blur = MainCamera.GetComponent<Blur>();
        puaseAnim = PuaseGroup.GetComponent<Animator>();
        SkillAnim = SkillGroup.GetComponent<Animator>();
        MessageAnim = MessageGroup.GetComponent<Animator>();
        audioListener = GetComponent<AudioListener>();
        AudioImage = SoundButton.GetComponent<Image>();
        for (int i = 1; i <= 6; i++) SKM.useSkill[i] = PlayerPrefs.GetInt("skill" + i);//限制技能使用
        LoadSKillUnlock();
        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            audioListener.enabled = false;
            AudioImage.sprite = AssetConfig.Audio_off_Image;
        }
        else
        {
            audioListener.enabled = true;
            AudioImage.sprite = AssetConfig.Audio_on_Image;
        }
    }

    //读取已经解锁的技能和角色
    public void LoadSKillUnlock()
    {
        Slipicon.GetComponent<Image>().sprite = AssetConfig.SlipImage_2;
        Chargericon.GetComponent<Image>().sprite = AssetConfig.ChargerImage_2;
        Tunnelicon.GetComponent<Image>().sprite = AssetConfig.portalImage_2;
        Bananaicon.GetComponent<Image>().sprite = AssetConfig.BananaImage_2;
        Stabicon.GetComponent<Image>().sprite = AssetConfig.StabImage_2;
        Harassmenticon.GetComponent<Image>().sprite = AssetConfig.HarassmentImage_2;

        bool haveSkill = false;

        for (int i = 1; i <= 6; i++)
        {
            if (SKM.useSkill[i] == 1)
            {
                haveSkill = true;
                switch (i)
                {
                    case 1:
                        Slipicon.GetComponent<Image>().sprite = AssetConfig.SlipImage;
                        XNSkillIcon_1 = AssetConfig.SlipImage;
                        XNSkillIcon_2 = AssetConfig.SlipImage_2;
                        break;
                    case 2:
                        Chargericon.GetComponent<Image>().sprite = AssetConfig.ChargerImage;
                        XNSkillIcon_1 = AssetConfig.ChargerImage;
                        XNSkillIcon_2 = AssetConfig.ChargerImage_2;
                        break;
                    case 3:
                        Tunnelicon.GetComponent<Image>().sprite = AssetConfig.portalImage;
                        CSJSkillIcon_1 = AssetConfig.portalImage;
                        CSJSkillIcon_2 = AssetConfig.portalImage_2;
                        unlockCSJ = true;
                        CSJButton.SetActive(true);
                        break;
                    case 4:
                        Bananaicon.GetComponent<Image>().sprite = AssetConfig.BananaImage;
                        JSHSkillIcon_1 = AssetConfig.BananaImage;
                        JSHSkillIcon_2 = AssetConfig.BananaImage_2;
                        unlockJSH = true;
                        JSHButton.SetActive(true);
                        break;
                    case 5:
                        Harassmenticon.GetComponent<Image>().sprite = AssetConfig.HarassmentImage;
                        unlockDDH = true;
                        DDHButton.SetActive(true);
                        DDHSkillIcon_1 = AssetConfig.HarassmentImage;
                        DDHSkillIcon_2 = AssetConfig.HarassmentImage_2;
                        break;
                    case 6:
                        Stabicon.GetComponent<Image>().sprite = AssetConfig.StabImage;
                        DDHSkillIcon_1 = AssetConfig.StabImage;
                        DDHSkillIcon_2 = AssetConfig.StabImage_2;
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 1:
                        XNSkillIcon_1 = AssetConfig.SlipImage;
                        XNSkillIcon_2 = AssetConfig.SlipImage_2;
                        break;
                    case 2:
                        XNSkillIcon_1 = AssetConfig.ChargerImage;
                        XNSkillIcon_2 = AssetConfig.ChargerImage_2;
                        break;
                    case 3:
                        CSJSkillIcon_1 = AssetConfig.portalImage;
                        CSJSkillIcon_2 = AssetConfig.portalImage_2;
                        unlockCSJ = false;
                        CSJButton.SetActive(true);
                        break;
                    case 4:
                        JSHSkillIcon_1 = AssetConfig.BananaImage;
                        JSHSkillIcon_2 = AssetConfig.BananaImage_2;
                        unlockJSH = false;
                        JSHButton.SetActive(true);
                        break;
                    case 5:
                        unlockDDH = false;
                        DDHButton.SetActive(true);
                        DDHSkillIcon_1 = AssetConfig.HarassmentImage;
                        DDHSkillIcon_2 = AssetConfig.HarassmentImage_2;
                        break;
                    case 6:
                        DDHSkillIcon_1 = AssetConfig.StabImage;
                        DDHSkillIcon_2 = AssetConfig.StabImage_2;
                        break;
                }
            }
            if (!haveSkill)
            {
                SkillGroup.SetActive(false);
            }
            else
            {
                SkillGroup.SetActive(true);
            }
        }
    }

    public void MessageShow(string conten)
    {
        HUD.AlramMessage.text = conten;
        MessageAnim.SetTrigger("show");
    }

    //发动技能按钮
    public void SkillluanchHandler()
    {
        if (Skillopened) SkillAnim.SetBool("open", false);
        SelectCharBtn.enabled = SKM.SkillluanchHandler();
    }


    //暂停按钮功能
    public void Btn_Puase()
    {
        if (!Gamepusing)
        {
            puasePanelShow();
        }
        else
        {
            GBM.GamePause = false;
            blur.enabled = false;
            puaseAnim.SetTrigger("close");
            SkillAnim.SetTrigger("resum");
        }
        Gamepusing = !Gamepusing;
    }

    //暂停菜单相关
    private void puasePanelShow()
    {
        puaseAnim.SetTrigger("open");
        SkillAnim.SetTrigger("puase");
        GBM.GamePause = true;
        blur.enabled = true;
        var tipsLanguage = Language_CN.tips_arg;
        int RandNum = Random.Range(0, tipsLanguage.Length - 1);
        Tips.text = tipsLanguage[RandNum];
        if (PlayerPrefs.HasKey(LevelName)) NewLevelIcon.SetActive(false);
        else NewLevelIcon.SetActive(true);
    }

    //点开角色选择
    public void Btn_CharBtn()
    {
        if (!unlockDDH && !unlockJSH && !unlockCSJ) return;//一个角色都没有解锁，返回
        if (SKM.onSkillLaunch) return;
        if (!Skillopened)
        {
            SkillAnim.SetBool("open", true);
            Skillopened = true;
        }
        else
        {
            SkillAnim.SetBool("open", false);
            Skillopened = false;
        }
    }

    //回复犀牛
    public void ResumCharSelect()
    {
        SKM.UpdataSkillButton("RHINOCEROS");
        CharSelectBG.GetComponent<Image>().sprite = AssetConfig.XN_BG;
        SelectCharBtn.enabled = true;
    }

    //选择丹顶鹤
    public void Btn_Select_DDH()
    {
        if (SKM.DDH_Enegy < SKM.MaxEnegy) return;
        SKM.UpdataSkillButton("CRANE");
        CharSelectBG.GetComponent<Image>().sprite = AssetConfig.DDH_BG;
        SkillAnim.SetBool("open", false);
        SelectCharBtn.enabled = false;

    }

    //选择金丝猴
    public void Btn_Select_JSH()
    {
        if (SKM.JSH_Enegy < SKM.MaxEnegy) return;
        SKM.UpdataSkillButton("MONKEY");
        CharSelectBG.GetComponent<Image>().sprite = AssetConfig.JSH_BG;
        SkillAnim.SetBool("open", false);
        SelectCharBtn.enabled = false;
    }

    //选择穿山甲
    public void Btn_Select_CSJ()
    {
        if (SKM.CSJ_Enegy < SKM.MaxEnegy) return;
        SKM.UpdataSkillButton("PANGOLIN");
        CharSelectBG.GetComponent<Image>().sprite = AssetConfig.CSJ_BG;
        SkillAnim.SetBool("open", false);
        SelectCharBtn.enabled = false;
    }

    //重新开始按钮
    public void Btn_Restar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //退出按钮
    public void Btn_Exit()
    {
        GlobleData.loadName = "MainScen";
        SceneManager.LoadScene("Async");
    }

    //
    public void Btn_Sound()
    {
        if (PlayerPrefs.GetInt("SoundOn") == 1)
        {
            PlayerPrefs.SetInt("SoundOn", 0);
            audioListener.enabled = false;
            AudioImage.sprite = Resources.Load<Sprite>("HUD/AudioOff");
        }
        else
        {
            PlayerPrefs.SetInt("SoundOn", 1);
            audioListener.enabled = true;
            AudioImage.sprite = Resources.Load<Sprite>("HUD/AudioOn");
        }
    }
}