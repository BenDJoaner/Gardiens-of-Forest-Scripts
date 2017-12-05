using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour
{
    public int unlockSKill;
    GameSceneManager GSM;                //场景管理器
    MainUIManagerSC MUM;
    SkillManager SKM;

    // Use this for initialization
    void Start()
    {
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
        MUM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainUIManagerSC>();
        SKM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SkillManager>();

        if (PlayerPrefs.GetInt("skill" + unlockSKill) == 1)
        {
            Destroy(this.gameObject);
            GSM.OPENEDCAGE();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GSM.OPENEDCAGE();
            Destroy(gameObject);
            PlayerPrefs.SetInt("skill" + unlockSKill, 1);
            SKM.useSkill[unlockSKill] = 1;
            MUM.LoadSKillUnlock();
            MUM.Btn_CharBtn();
        }
    }
}
