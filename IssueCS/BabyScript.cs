using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyScript : MonoBehaviour
{
    public bool saved;
    NavMeshAgent nav;
    GameObject player;
    Cage cage;
    GameBooleanManager GBM;
    // Use this for initialization
    void Start()
    {
        cage = GetComponentInParent<Cage>();
        nav = GetComponent<NavMeshAgent>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (cage.unlocked)
        {
            nav.enabled = true;
            saved = true;
            nav.SetDestination(player.transform.position);
            if (GBM.GameWin || GBM.GameLost)
            {
                Destroy(gameObject);
                Instantiate(AssetConfig.CharChangeEffect, transform.position, Quaternion.identity);
            }
        }
    }
}
