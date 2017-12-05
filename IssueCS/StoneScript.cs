using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour {

    GameBooleanManager GBM;
    GameSceneManager GSM;
    SkillManager SKM;
    // Use this for initialization
    void Start () {
        SKM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SkillManager>();
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (GSM.CharState)
            {
                case "CRANE":
                    break;
                case "MONKEY":
                    SKM.JSH_Enegy = 0;
                    break;
                case "PANGOLIN":
                    SKM.CSJ_Enegy = 0;
                    break;
                default:
                    GBM.PlayerFall = true;
                    Destroy(this);
                    break;
            }
        }
        else if (other.gameObject.tag == "Hunter")
        {
            other.gameObject.GetComponent<HunterAI>().EnemyDeadFunc();
        }
    }
}
