using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHUDManager : MonoBehaviour
{
    public Text SettingText;
    public Text MusicText;
    public Text AudioText;
    public Text TeamText;
    public Text TeamNameText;

    public GameObject SettingPanel;
    public Dropdown languageDrop;

    MenuManager manager;

    // Use this for initialization
    void Start()
    {
        OnUILanguageChange();
        languageDrop.value = PlayerPrefs.GetInt("language");
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnUILanguageChange()
    {
        string[] arg = { };
        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                arg = Language_CN.UI_arg;
                break;
            case 1:
                arg = Languge_EN.UI_arg;
                break;
        }
        SettingText.text = arg[6];
        MusicText.text = arg[7];
        AudioText.text = arg[8];
        TeamText.text = arg[9];
        TeamNameText.text = arg[10];
    }


    public void OnLanguageChange()
    {
        if (PlayerPrefs.GetInt("language") == languageDrop.value) return;
        PlayerPrefs.SetInt("language", languageDrop.value);
        manager.OnEnterLevel("MainScen");
    }


    public void OnChangeSettingVisible()
    {
        if (SettingPanel.activeSelf) SettingPanel.SetActive(false);
        else SettingPanel.SetActive(true);
    }

}
