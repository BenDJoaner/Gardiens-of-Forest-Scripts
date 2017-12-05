using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSC : MonoBehaviour
{
    SkillManager SKM;
    GameSceneManager GSM;

    // Use this for initialization
    void Start()
    {
        SKM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SkillManager>();
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (GSM.CharState != "MONKEY" || SKM.BananaNum >= GameDataConfig.MaxBananaNum) return;
        if (other.tag == "Player")
        {
            SKM.BananaNum += 1;
            GameObject.Destroy(this.gameObject);
        }
    }
}
