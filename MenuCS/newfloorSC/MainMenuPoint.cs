using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MainMenuPoint : MonoBehaviour
{
    public int typ;
    public int id;
    public string LevelName;
    public bool pass;
    [Space]
    public GameObject Cage;
    public GameObject Canvas;
    public Text LevelNameText;

    MenuManager manager;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        if (typ == 1) Destroy(gameObject);
        LevelName = SceneConfig.Instance.GetLoadSceneByID(typ, id);
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MenuManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.GetString(LevelName) == "pass")
        {
            pass = true;
            Cage.SetActive(false);
        }
        LevelNameText.text = SceneConfig.Instance.NameText(typ, id);
        Canvas.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        Canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Canvas.SetActive(false);
    }

    public void OnPressEnterButton()
    {
        manager.OnEnterLevel(LevelName);
        PlayerPrefs.SetInt("LPX", (int)player.transform.position.x);
        PlayerPrefs.SetInt("LPY", (int)player.transform.position.y);
        PlayerPrefs.SetInt("LPZ", (int)player.transform.position.z);
    }
}
