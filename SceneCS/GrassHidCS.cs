using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassHidCS : MonoBehaviour
{

    GameObject player;
    GameBooleanManager GBM;
    HUDManager HUD;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        HUD = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HUDManager>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            GBM.PlayerHide = true;
            HUD.setPCMessage("隐匿", GameDataConfig.ColorGreen);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player"))
        {
            HUD.setPCMessage("暴露", GameDataConfig.ColorRed);
            GBM.PlayerHide = false;
        }
    }
}
