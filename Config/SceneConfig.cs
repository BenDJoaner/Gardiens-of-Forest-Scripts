using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConfig
{
    public static SceneConfig Instance = new SceneConfig();
    public SceneConfig() { }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>根据编号选择相应读取的关卡名</returns>
    /// <param name="typ">Typ.</param>
    /// <param name="id">Identifier.</param>
    public string GetLoadSceneByID(int typ, int id)
    {
        switch (typ)
        {
            case 1:
                return "hide_" + id;
            case 2:
                return "boss_" + id;
            default:
                return "level_" + id;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>显示关卡名</returns>
    /// <param name="typ">1为主线关卡，2为特殊关卡，3为BOSS关卡.</param>
    /// <param name="id">Identifier.</param>
    public string NameText(int typ, int id)
    {
        if (PlayerPrefs.GetInt("language") == 0)
        {
            switch (typ)
            {
                case 1:
                    return "隐藏 " + id;
                case 2:
                    return "终章 " + id;
                default:
                    return "主线" + id;
            }
        }
        else
        {
            switch (typ)
            {
                case 1:
                    return "Secret" + id;
                case 2:
                    return "BOSS" + id;
                default:
                    return "Story" + id;
            }
        }
    }
}
