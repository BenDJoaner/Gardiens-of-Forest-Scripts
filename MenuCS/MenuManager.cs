using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class MenuManager : MonoBehaviour
{
    bool onPosition;
    GameObject player;
    Vector3 lastPositon;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.HasKey("LPX"))
            lastPositon = new Vector3(PlayerPrefs.GetInt("LPX"), PlayerPrefs.GetInt("LPY"), PlayerPrefs.GetInt("LPZ"));
        else
        {
            player.GetComponent<NavMeshAgent>().enabled = true;
            onPosition = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!onPosition)
        {
            player.transform.position = lastPositon;
            player.GetComponent<NavMeshAgent>().enabled = true;
            onPosition = true;
        }
    }

    /// <summary>
    /// 进入关卡方法
    /// </summary>
    /// <param name="name">Name.</param>
    public void OnEnterLevel(string name)
    {
        GlobleData.loadName = name;
        SceneManager.LoadScene("Async");
    }

}
