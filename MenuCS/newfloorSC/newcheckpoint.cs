using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newcheckpoint : MonoBehaviour
{
    public bool Activ;
    public string checkpointID;
    GameObject canva;
    public GameObject lastPoint;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey(checkpointID))
        {
            if (PlayerPrefs.GetString(checkpointID) == "passed")
            {
                Activ = true;
            }
        }
        canva = transform.Find("Canvas").gameObject;

        if (canva) canva.SetActive(false);
        if (PlayerPrefs.HasKey("lastlevel") && PlayerPrefs.GetString("lastlevel") == checkpointID)
        {
            Vector3 lastPos = new Vector3(transform.position.x, 2, transform.position.z);
            GameObject.FindGameObjectWithTag("Player").transform.position = lastPos;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!Activ)
        {
            if (!lastPoint)
            {
                gameObject.SetActive(false);
            }
            else
            {
                if (lastPoint.GetComponent<newcheckpoint>().Activ)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            gameObject.SetActive(true);
        }
        if (checkpointID == "level_1") gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canva.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canva.SetActive(false);
        }
    }

    public void enterLevel()
    {
        GlobleData.loadName = checkpointID;
        Application.LoadLevel(0);
    }

}
