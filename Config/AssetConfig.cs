using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetConfig
{
    /*===============================================
                     动态图片资源config
    ================================================*/
    
    //角色按钮背景图
    public static Sprite XN_BG = Resources.Load<Sprite>("HUD/XNSelect");
    public static Sprite CSJ_BG = Resources.Load<Sprite>("HUD/CSJSelect");
    public static Sprite JSH_BG = Resources.Load<Sprite>("HUD/JSHSelect");
    public static Sprite DDH_BG = Resources.Load<Sprite>("HUD/DDHSelect");

    //角色技能icon
    public static Sprite SlipImage = Resources.Load<Sprite>("HUD/Snip");
    public static Sprite SlipImage_2 = Resources.Load<Sprite>("HUD/Snip_2");
    public static Sprite ChargerImage = Resources.Load<Sprite>("HUD/Charger");
    public static Sprite ChargerImage_2 = Resources.Load<Sprite>("HUD/Charger_2");
    public static Sprite portalImage = Resources.Load<Sprite>("HUD/portal");
    public static Sprite portalImage_2 = Resources.Load<Sprite>("HUD/portal_2");
    public static Sprite BananaImage = Resources.Load<Sprite>("HUD/Banana_3");
    public static Sprite BananaImage_2 = Resources.Load<Sprite>("HUD/Banana_4");
    public static Sprite HarassmentImage = Resources.Load<Sprite>("HUD/Harassment");
    public static Sprite HarassmentImage_2 = Resources.Load<Sprite>("HUD/Harassment_2");
    public static Sprite StabImage = Resources.Load<Sprite>("HUD/Stab");
    public static Sprite StabImage_2 = Resources.Load<Sprite>("HUD/Stab_2");

    //静音控制按钮
    public static Sprite Audio_off_Image = Resources.Load<Sprite>("HUD/AudioOff");
    public static Sprite Audio_on_Image = Resources.Load<Sprite>("HUD/AudioOn");



    /*===============================================
                                            动态音频资源config
    ================================================*/

    //游戏胜利/失败音频
    public static AudioClip LostAudio = Resources.Load<AudioClip>("Audio/gamelost");
    public static AudioClip WinAudio = Resources.Load<AudioClip>("Audio/gamewin");

    //猎人音频
    public static AudioClip SurprisAudio = Resources.Load<AudioClip>("Audio/EnemySurprise");
    public static AudioClip SighAudio = Resources.Load<AudioClip>("Audio/EnemySigh");
    public static AudioClip WalkAudio = Resources.Load<AudioClip>("Audio/EnemyWalking");
    public static AudioClip DeadAudio = Resources.Load<AudioClip>("Audio/EnemyDead");
    public static AudioClip HaHaAudio = Resources.Load<AudioClip>("Audio/EnemyHAHA");


    /*===============================================
                                            粒子特效资源config
    ================================================*/

    public static GameObject CharChangeEffect = Resources.Load<GameObject>("Effect/SkillEffectPick/CFX3_Hit_SmokePuff");

    //技能粒子特效
    //public static GameObject SlipEffect = Resources.Load<GameObject>("Effect/SkillEffectPick/CFXM3_Shield_Leaves_Raw");//疾跑
    //public static GameObject ChargeEffect_1 = Resources.Load<GameObject>("Effect/SkillEffectPick/EnergyBall_Loop");
    //public static GameObject ChargeEffect_2 = Resources.Load<GameObject>("Effect/SkillEffectPick/Shot01_Loop");
    public static GameObject ChargeEffect_3 = Resources.Load<GameObject>("Effect/SkillEffectPick/CFX2_GroundRockHitBrown");
    public static GameObject ProtalEffect = Resources.Load<GameObject>("Effect/portal");
    public static GameObject DeegEffect = Resources.Load<GameObject>("Effect/SkillEffectPick/DeegEffect");
    public static GameObject MainLineLight = Resources.Load<GameObject>("Effect/MainLineLight");


    /*===============================================
                                            其他游戏对象资源config
    ================================================*/

    //点击特效
    public static GameObject PointEffect = Resources.Load<GameObject>("Effect/PointEffect");

    //丹顶鹤（技能）模型
    public static GameObject DDHObj = Resources.Load<GameObject>("Prefab/DDHObj");

    //香蕉以及香蕉皮
    public static GameObject Banana = Resources.Load<GameObject>("Prefab/Banana");
    public static GameObject BananaPeel = Resources.Load<GameObject>("Prefab/BananaPeel");

    //石头
    public static GameObject StoneObj=Resources. Load<GameObject>("Prefab/stone");
}
