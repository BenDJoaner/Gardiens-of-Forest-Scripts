using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManagerSC : MonoBehaviour
{

    public GameObject[] Portals;

    public int index = 0;

    [HideInInspector]
    public float cdTime;
    [HideInInspector]
    public float[] ExistTime = new float[6];

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (cdTime > 0)
        {
            cdTime -= Time.deltaTime;
        }

        //for (int i = 0; i < Portals.Length; i++)
        //{
        //    if (i % 2 == 0 && Portals[i].activeSelf)
        //        if (ExistTime[i] >= 0)
        //            ExistTime[i] -= Time.deltaTime;
        //        else
        //        {
        //            Portals[i].SetActive(false);
        //            Portals[i - 1].SetActive(false);
        //        }
        //}
    }

    public void ResetPortal()
    {
        index = 0;
        for (int i = 0; i < Portals.Length; i++)
        {
            Portals[i].SetActive(false);
        }
    }

    public void PutaPortal(Vector3 vect)
    {
        if (index == GameDataConfig.MaxPortalNum) return;
        Vector3 pos = vect;
        if (index % 2 == 1) pos.y = vect.y + 0.01f;
        Portals[index].SetActive(true);
        Portals[index].transform.position = pos;
        GameObject temp = Instantiate(AssetConfig.DeegEffect);
        temp.transform.position = vect;
        index += 1;
    }
}
