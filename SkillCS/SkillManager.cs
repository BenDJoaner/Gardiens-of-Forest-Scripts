using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Button SkillBtn;
    public Slider EnegySlider;
    public Slider DDHSlider;
    public Slider JSHSlider;
    public Slider CSJSlider;
    public Text DDHText;
    public Text JSHText;
    public Text CSJText;

    //=======特效========
    public GameObject SlipEffect;
    public GameObject ChargeEffect;

    [HideInInspector]
    public float MaxEnegy = 100, MinEnegy = 0;

    [HideInInspector]
    public float BananaNum = 0;

    [HideInInspector]
    public float XN_Enegy, DDH_Enegy, JSH_Enegy, CSJ_Enegy;

    [HideInInspector]
    public bool onSkillLaunch;
    bool launchFlag;

    public int[] useSkill = new int[7] { 0, 0, 0, 0, 0, 0, 0 };//限制

    MainUIManagerSC MUM;
    AnimControl AC;
    GameObject player;
    GameSceneManager GSM;
    GameBooleanManager GBM;
    PortalManagerSC PM;
    Camera cam;
    GameObject selectEnemy;


    /// <summary>
    /// RHINOCEROS 犀牛 
    /// MONKEY 金丝猴
    /// CRANE 丹顶鹤
    /// PANGOLIN 穿山甲
    /// </summary>
    public enum CharectoerMode
    {
        RHINOCEROS, MONKEY, CRANE, PANGOLIN
    }

    public CharectoerMode m_CharectorMode = CharectoerMode.RHINOCEROS;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MUM = GetComponent<MainUIManagerSC>();
        GSM = GetComponent<GameSceneManager>();
        GBM = GetComponent<GameBooleanManager>();
        PM = GetComponent<PortalManagerSC>();
        AC = player.GetComponent<AnimControl>();
        XN_Enegy = GameDataConfig.XN_MaxEnegy;
        DDH_Enegy = GameDataConfig.DDH_MaxEnegy;
        JSH_Enegy = GameDataConfig.JSH_MaxEnegy;
        CSJ_Enegy = GameDataConfig.CSJ_MaxEnegy;
        cam = Camera.main.GetComponent<Camera>();
        GSM.CharState = "RHINOCEROS";
        UpdataSkillButton("RHINOCEROS");
    }


    // Update is called once per frame
    void Update()
    {
        if (GBM.GameWin || GBM.GameLost) CancelAllSkill();
        if (GBM.GamePause) return;
        DDHSlider.value = DDH_Enegy;
        JSHSlider.value = JSH_Enegy;
        CSJSlider.value = CSJ_Enegy;
        DDHText.text = "";
        JSHText.text = "";
        CSJText.text = "";
        RecoverEnegy();
    }

    private void FixedUpdate()
    {
        if (GBM.GamePause || GBM.GameLost || GBM.GameWin) return;
        switch (m_CharectorMode)
        {
            case CharectoerMode.RHINOCEROS:
                RHINOCEROS_Function();
                break;
            case CharectoerMode.MONKEY:
                MONKEY_Function();
                break;
            case CharectoerMode.CRANE:
                CRANE_Function();
                break;
            case CharectoerMode.PANGOLIN:
                PANGOLIN_Function();
                break;
        }
    }

    /// <summary>
    /// 能量条数值
    /// </summary>
    /// <returns>The slider valu.</returns>
    /// <param name="value">Value.</param>
    float SetSliderValu(float value)
    {
        if (value < MinEnegy)
        {
            value = MinEnegy;
            EnegySlider.value = XN_Enegy;
            switch (m_CharectorMode)
            {
                case CharectoerMode.RHINOCEROS:
                    //if (PlayerPrefs.GetInt("skill2") == 1) AC.OnThrowSometing(AssetConfig.ChargeEffect_3);
                    XN_Enegy = MinEnegy;
                    CancelAllSkill();
                    break;
                default:
                    MUM.ResumCharSelect();//恢复
                    break;
            }
        }
        if (value >= MaxEnegy)
        {
            value = MaxEnegy;
        }
        return value;
    }


    /// <summary>
    /// 技能回复（默认不停调用）
    /// </summary>
    void RecoverEnegy()
    {
        switch (m_CharectorMode)
        {
            case CharectoerMode.CRANE:
                if (XN_Enegy < MaxEnegy) XN_Enegy += GameDataConfig.XN_RECOVER_SPEED * Time.deltaTime;
                if (JSH_Enegy < MaxEnegy) JSH_Enegy += GameDataConfig.JSH_RECOVER_SPEED * Time.deltaTime;
                if (CSJ_Enegy < MaxEnegy) CSJ_Enegy += GameDataConfig.CSJ_RECOVER_SPEED * Time.deltaTime;
                break;
            case CharectoerMode.MONKEY:
                if (XN_Enegy < MaxEnegy) XN_Enegy += GameDataConfig.XN_RECOVER_SPEED * Time.deltaTime;
                if (DDH_Enegy < MaxEnegy) DDH_Enegy += GameDataConfig.DDH_RECOVER_SPEED * Time.deltaTime;
                if (CSJ_Enegy < MaxEnegy) CSJ_Enegy += GameDataConfig.CSJ_RECOVER_SPEED * Time.deltaTime;
                break;
            case CharectoerMode.PANGOLIN:
                if (XN_Enegy < MaxEnegy) XN_Enegy += GameDataConfig.XN_RECOVER_SPEED * Time.deltaTime;
                if (DDH_Enegy < MaxEnegy) DDH_Enegy += GameDataConfig.DDH_RECOVER_SPEED * Time.deltaTime;
                if (JSH_Enegy < MaxEnegy) JSH_Enegy += GameDataConfig.JSH_RECOVER_SPEED * Time.deltaTime;
                break;
            default:
                if (DDH_Enegy < MaxEnegy) DDH_Enegy += GameDataConfig.DDH_RECOVER_SPEED * Time.deltaTime;
                if (JSH_Enegy < MaxEnegy) JSH_Enegy += GameDataConfig.JSH_RECOVER_SPEED * Time.deltaTime;
                if (CSJ_Enegy < MaxEnegy) CSJ_Enegy += GameDataConfig.CSJ_RECOVER_SPEED * Time.deltaTime;
                if (XN_Enegy < MaxEnegy && !onSkillLaunch)
                {
                    XN_Enegy += GameDataConfig.XN_RECOVER_SPEED * Time.deltaTime;
                    SkillBtn.GetComponent<Image>().sprite = MUM.XNSkillIcon_2;
                }
                else SkillBtn.GetComponent<Image>().sprite = MUM.XNSkillIcon_1;
                break;
        }
        DDHText.text = DDH_Enegy >= MaxEnegy || DDH_Enegy <= MinEnegy ? "" : (int)((MaxEnegy - DDH_Enegy) / GameDataConfig.DDH_RECOVER_SPEED) + "";
        JSHText.text = JSH_Enegy >= MaxEnegy || DDH_Enegy <= MinEnegy ? "" : (int)((MaxEnegy - JSH_Enegy) / GameDataConfig.JSH_RECOVER_SPEED) + "";
        CSJText.text = CSJ_Enegy >= MaxEnegy || DDH_Enegy <= MinEnegy ? "" : (int)((MaxEnegy - CSJ_Enegy) / GameDataConfig.CSJ_RECOVER_SPEED) + "";
    }

    /// <summary>
    /// 犀牛技能
    /// </summary>
    void RHINOCEROS_Function()
    {
        if (XN_Enegy == MinEnegy || useSkill[1] != 1 && useSkill[2] != 1)
        {
            return;
        }
        EnegySlider.value = SetSliderValu(XN_Enegy);
        if (onSkillLaunch)
        {
            GameDataConfig.moveSpeed = GameDataConfig.SlipSpeed;
            //=======疾跑技能========
            if (useSkill[2] != 1)
            {
                useSkill[1] = 0;
                XN_Enegy -= GameDataConfig.XN_Enegy_Speed1 * Time.deltaTime;
                SlipEffect.SetActive(true);
            }
            //=======解锁冲撞个技能========
            else
            {
                if (!launchFlag)
                {
                    SlipEffect.SetActive(true);
                    XN_Enegy -= GameDataConfig.XN_Enegy_Speed1 * Time.deltaTime;
                    if ((Input.GetMouseButtonDown(0) || Input.touchCount >= 1)
                        && TargetingRaycast() && TargetingRaycast().tag == "Hunter")
                    {
                        selectEnemy = TargetingRaycast();
                        selectEnemy.GetComponent<HunterAI>().beLocked = true;
                        AC.SetAnimState("Charge");
                        launchFlag = true;
                        useSkill[2] = 0;

                    }
                }
                else
                {
                    XN_Enegy -= GameDataConfig.XN_Enegy_Speed2 * Time.deltaTime;
                    ChargeEffect.SetActive(true);
                    SlipEffect.SetActive(false);
                    GBM.PlayerTenacity = true;
                    GBM.PlayerMoveing = true;
                    if (selectEnemy)
                    {
                        player.transform.LookAt(selectEnemy.transform.position);
                        player.GetComponent<PlayerMovement>().SetTarget(selectEnemy.transform);
                        player.transform.Translate(Vector3.forward * Time.deltaTime * 10);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 金丝猴技能
    /// </summary>
    void MONKEY_Function()
    {
        if (JSH_Enegy >= MinEnegy)
        {
            JSH_Enegy -= GameDataConfig.JSH_Enegy_Speed * Time.deltaTime;
            EnegySlider.value = SetSliderValu(JSH_Enegy);
        }
        if (BananaNum > 0)
        {
            SkillBtn.GetComponent<Image>().sprite = MUM.JSHSkillIcon_1;
            if (onSkillLaunch)
            {
                onSkillLaunch = false;
                BananaNum -= 1;
                AC.OnThrowSometing(AssetConfig.BananaPeel);
            }
        }
        else
        {
            SkillBtn.GetComponent<Image>().sprite = MUM.JSHSkillIcon_2;
        }
    }

    /// <summary>
    /// 丹顶鹤技能
    /// </summary>
    void CRANE_Function()
    {
        if (DDH_Enegy >= MinEnegy)
        {
            DDH_Enegy -= GameDataConfig.DDH_Enegy_Speed * Time.deltaTime;
            EnegySlider.value = SetSliderValu(DDH_Enegy);
        }

        if (onSkillLaunch)
        {
            if (!launchFlag)
            {
                if ((Input.GetMouseButtonDown(0) || Input.touchCount >= 1)
        && TargetingRaycast() && TargetingRaycast().tag == "Hunter")
                {
                    selectEnemy = TargetingRaycast();
                    AC.OutNavmeshTranslate(selectEnemy);
                    DDH_Enegy = MinEnegy;
                    launchFlag = true;
                    useSkill[5] = 0;
                }
            }
        }
    }

    /// <summary>
    /// 穿山甲技能
    /// </summary>
    void PANGOLIN_Function()
    {
        if (CSJ_Enegy >= MinEnegy && !launchFlag)
        {
            CSJ_Enegy -= GameDataConfig.CSJ_Enegy_Speed * Time.deltaTime;
            EnegySlider.value = SetSliderValu(CSJ_Enegy);
        }

        if (onSkillLaunch)
        {
            if (!launchFlag)
            {
                if (GSM.playerNowPoint.Equals(GSM.PlayerNextPoint))
                {
                    launchFlag = false;
                    onSkillLaunch = false;
                    AC.OnDeegAHole(3);
                    return;
                }
                if (PM.index == GameDataConfig.MaxPortalNum) PM.ResetPortal();
                SkillBtn.GetComponent<Image>().sprite = MUM.CSJSkillIcon_1;
                PM.PutaPortal(GSM.playerNowPoint);
                player.transform.position = GSM.playerNowPoint;
                AC.OnDeegAHole(1);
                launchFlag = true;
                onSkillLaunch = false;
            }
            else
            {
                AC.OnDeegAHole(2);
                SkillBtn.GetComponent<Image>().sprite = MUM.CSJSkillIcon_2;
                launchFlag = false;
                onSkillLaunch = false;
                PM.PutaPortal(GSM.PlayerNextPoint);
                if (PM.index == GameDataConfig.MaxPortalNum) CSJ_Enegy = 0;
                useSkill[3] = 0;
            }
        }
    }

    /// <summary>
    /// 发动技能触发器
    /// </summary>
    /// <returns><c>true</c>, if handler was skillluanched, <c>false</c> otherwise.</returns>
    public bool SkillluanchHandler()
    {
        bool flag = true;
        switch (m_CharectorMode)
        {
            case CharectoerMode.CRANE:
                SkillBtn.GetComponent<Image>().sprite = MUM.DDHSkillIcon_2;
                flag = false;
                break;
            case CharectoerMode.MONKEY:
                if (BananaNum < GameDataConfig.MaxBananaNum) return false;
                flag = false;
                break;
            case CharectoerMode.PANGOLIN:
                SkillBtn.GetComponent<Image>().sprite = MUM.CSJSkillIcon_1;
                flag = false;
                break;
            default:
                if (launchFlag) return false;
                if (XN_Enegy < MaxEnegy) return true;
                SkillBtn.GetComponent<Image>().sprite = MUM.XNSkillIcon_2;
                flag = false;
                break;
        }
        onSkillLaunch = true;
        return flag;
    }

    /// <summary>
    /// 外部/内部传入，改变技能状态
    /// </summary>
    /// <param name="Data">Data为四个角色的英文名.</param>
    public void UpdataSkillButton(string Data)
    {
        if (GBM.GameWin || GBM.GameLost || GBM.GamePause) return;
        AC.setChar(Data);
        GSM.CharState = Data;
        CancelAllSkill();
        switch (Data)
        {
            //切换到犀牛
            case "RHINOCEROS":
                m_CharectorMode = CharectoerMode.RHINOCEROS;
                if (XN_Enegy >= MaxEnegy)
                {
                    SkillBtn.GetComponent<Image>().sprite = MUM.XNSkillIcon_1;
                }
                break;
            //切换到丹顶鹤
            case "CRANE":
                m_CharectorMode = CharectoerMode.CRANE;
                GBM.PlayerSuper = true;
                GBM.PlayerHide = false;
                SkillBtn.GetComponent<Image>().sprite = MUM.DDHSkillIcon_1;
                GameDataConfig.moveSpeed = GameDataConfig.FlyingSpeed;
                break;
            //切换到金丝猴
            case "MONKEY":
                m_CharectorMode = CharectoerMode.MONKEY;
                SkillBtn.GetComponent<Image>().sprite = MUM.JSHSkillIcon_1;
                break;
            //切换到穿山甲
            case "PANGOLIN":
                m_CharectorMode = CharectoerMode.PANGOLIN;
                GameDataConfig.moveSpeed = GameDataConfig.DeggingSpeed;
                GBM.PlayerSuper = true;
                SkillBtn.GetComponent<Image>().sprite = MUM.CSJSkillIcon_1;
                break;
        }
    }


    float targetingRayLength = 100;
    public LayerMask targetingLayerMask = -1;
    /// <summary>
    /// 射线接收方法
    /// </summary>
    GameObject TargetingRaycast()
    {
        GameObject obj = null;
        if (cam != null)
        {
            RaycastHit hitInfo;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, targetingRayLength, targetingLayerMask.value))
            {
                obj = hitInfo.collider.gameObject;
            }
        }
        return obj;
    }

    /// <summary>
    /// 取消所有技能效果
    /// </summary>
    void CancelAllSkill()
    {
        MUM.LoadSKillUnlock();
        onSkillLaunch = false;
        launchFlag = false;
        GameDataConfig.moveSpeed = GameDataConfig.NormalSpeed;
        GBM.PlayerTenacity = false;
        GBM.PlayerSuper = false;
        EnegySlider.value = 0;
        SlipEffect.SetActive(false);
        ChargeEffect.SetActive(false);
        GBM.PlayerMoveing = true;
        selectEnemy = null;
        MUM.SelectCharBtn.enabled = true;
        return;
    }
}
