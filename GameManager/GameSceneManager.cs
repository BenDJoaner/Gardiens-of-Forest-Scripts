using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public int CageSum;                        //笼子总个数
    public int CageOpenNum;                //已经打开笼子的个数
    //public int AlramEnemy;
    public string CharState;                //现在的角色
    public GameObject Destination;
    public GameObject[] Enemies;

    [Header("坐标")]
    public Vector3 PlayerNextPoint;
    public Vector3 playerNowPoint;
    public Vector3 playerLastPosition;
    [HideInInspector]
    public GameObject[] babies;
    GameBooleanManager GBM;
    GameObject player;
    AudioSource AS;



    float timeCount;
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 60;
        GBM = GetComponent<GameBooleanManager>();
        Destination.SetActive(false);
        AS = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerPrefs.SetString("lastlevel", SceneManager.GetActiveScene().name);
        Enemies = GameObject.FindGameObjectsWithTag("Hunter");
        CageSum = GameObject.FindGameObjectsWithTag("Cage").Length;
        playerNowPoint = new Vector3(1, 2, 1);
        PlayerNextPoint = playerNowPoint;
        babies = GameObject.FindGameObjectsWithTag("baby");
    }

    // Update is called once per frame
    void Update()
    {
        if (GBM.OpenAllCage && !GBM.GameLost)
        {
            Destination.SetActive(true);
        }

        if (!GBM.OpenAllCage)
        {
            GBM.OpenAllCage |= CageOpenNum == CageSum;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        playerNowPoint = new Vector3(
            (int)(player.transform.position.x),
            player.transform.position.y,
            (int)(player.transform.position.z));

        if (GBM.GameWin)
        {
            if (!AS.isPlaying)
            {
                AS.clip = AssetConfig.WinAudio;
                AS.Play();
                AS.loop = false;
            }
            timeCount += Time.deltaTime;
            if (timeCount > 1.7f)
            {
                GlobleData.loadName = "MainScen";
                SceneManager.LoadScene("Async");
                tempData.unlockedLevel = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name, "pass");
            }
        }

        if (GBM.GameLost)
        {
            if (!AS.isPlaying)
            {
                AS.clip = AssetConfig.LostAudio;
                AS.Play();
                AS.loop = false;
            }
            timeCount += Time.deltaTime;
            if (timeCount > 1.5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void BabiesVisible(bool visible)
    {
        for (int i = 0; i < babies.Length; i++)
        {
            if (babies[i].GetComponent<BabyScript>().saved)
                babies[i].SetActive(visible);
        }
    }

    public void MoveBabies(Vector3 pos)
    {
        for (int i = 0; i < babies.Length; i++)
        {
            if (babies[i].GetComponent<BabyScript>().saved)
            {
                babies[i].GetComponent<NavMeshAgent>().enabled = false;
                babies[i].transform.position = pos;
                babies[i].GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }

    public void OPENEDCAGE()
    {
        CageOpenNum += 1;
    }
}
