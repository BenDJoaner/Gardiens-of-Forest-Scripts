using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PortalSC : MonoBehaviour
{
    public int index;

    int AnotherIndex;
    float deny;
    bool enter;
    Transform AnotherTran;
    PortalManagerSC manager;
    GameSceneManager GSM;
    GameBooleanManager GBM;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PortalManagerSC>();
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        if (index % 2 == 0)
        {
            AnotherIndex = index + 1;
        }
        else
        {
            AnotherIndex = index - 1;
        }
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Hunter"){
        //    gameObject.SetActive(false);
        //}
        if (manager.cdTime > 0) return;
        if (other.tag == "Player")
        {
            if (!manager.Portals[AnotherIndex].activeSelf) return;
            AnotherTran = manager.Portals[AnotherIndex].transform;
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.gameObject.transform.position = AnotherTran.position;
            other.GetComponent<PlayerMovement>().SetTarget(AnotherTran);
            GBM.PlayerMoveing = true;
            Instantiate(AssetConfig.CharChangeEffect, AnotherTran.position, Quaternion.identity);
            Instantiate(AssetConfig.CharChangeEffect, transform.position, Quaternion.identity);
            manager.cdTime = 1;
            GSM.MoveBabies(AnotherTran.position);
            other.GetComponent<NavMeshAgent>().enabled = true;
        }
        else if (other.tag == "Hunter")
        {
            Instantiate(AssetConfig.DeegEffect, transform.position, Quaternion.identity);
            Instantiate(AssetConfig.DeegEffect, AnotherTran.position, Quaternion.identity);
            manager.Portals[AnotherIndex].SetActive(false);
            gameObject.SetActive(false);
            manager.index -= 2;
        }
    }
}
