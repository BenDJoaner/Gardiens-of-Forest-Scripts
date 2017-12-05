using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataConfig
{
    //角色属性记录
    public static float moveSpeed = 2.5f;//角色移动速度
    public static float NormalSpeed = 2.5f;//正常速度
    public static float SlipSpeed = 4;//是用“快跑”时候的速度
    public static float FlyingSpeed = 5;//飞行时候的速度
    public static float DeggingSpeed = 2;//挖掘时候的速度

    public static float ChargeSpeed = 10;//角色冲撞速度

    //敌人属性记录
    public static float enemyViewAngle = 180 / 2;//敌人可视角
    public static float enemyViewAngle1 = 180 / 2;//敌人可视角
    public static float enemyViewAngle2 = 120 / 2;//敌人可视角
    public static float enemyViewAngle3 = 90 / 2;//敌人可视角

    public static float enemyViewRange = 7;//敌人可视范围
    public static float enemyViewRange1 = 7;//敌人可视范围
    public static float enemyViewRange2 = 6;//敌人可视范围
    public static float enemyViewRange3 = 5;//敌人可视范围


    //技能记录
    public static float XN_MaxEnegy = 100;//不能修改
    public static float DDH_MaxEnegy = 100;//不能修改
    public static float JSH_MaxEnegy = 100;//不能修改
    public static float CSJ_MaxEnegy = 100;//不能修改

    public static float XN_Enegy_Speed1 = 20;//犀牛冲刺消耗速度
    public static float XN_Enegy_Speed2 = 200;//犀牛冲撞消耗速度

    public static float DDH_Enegy_Speed = 5;//丹顶鹤消耗速度
    public static float JSH_Enegy_Speed = 5;//金丝猴消耗速度
    public static float CSJ_Enegy_Speed = 10;//穿山甲消耗速度

    public static float XN_RECOVER_SPEED = 20;//犀牛回复速度
    public static float DDH_RECOVER_SPEED = 5;//丹顶鹤回复速度
    public static float JSH_RECOVER_SPEED = 20;//金丝猴回复速度
    public static float CSJ_RECOVER_SPEED = 5;//穿山甲回复速度

    public static float PortalExistTime = 10;//洞口持续时间

    public static int MaxBananaNum = 1;//最大可携带香蕉数
    public static int MaxPortalNum = 2;//最大可挖掘洞数


    //颜色记录（因为滤镜原因有可能会变色，需要在游戏中运行测试）
    public static Color ColorGreen = new Color(0.5f, 1, 0.5f);             //绿色
    public static Color ColorRed = new Color(1, 0.5f, 0.5f);                //红色
    public static Color ColorBlue = new Color(0.5f, 0.5f, 1);               //蓝色
    public static Color ColorWite = new Color(1, 1, 1);                         //白色
    public static Color ColorBlack = new Color(0, 0, 0);                     //黑色
    public static Color ColorGray = new Color(0.5f, 0.5f, 0.5f);          //灰色

}
