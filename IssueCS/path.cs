using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class path : MonoBehaviour
{
    PlayerMovement movement;
    GameBooleanManager GBM;
    GameSceneManager GSM;
    // Use this for initialization
    void Start()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GBM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBooleanManager>();
        GSM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //按下鼠标，
    void OnMouseDown()
    {
        if (IsTouchedUI()) return;
        Active();
    }

    void Active()
    {
        if (GBM.GamePause) return;
        if (GSM.CharState == "PANGOLIN" && Vector3.Distance(GSM.playerNowPoint, transform.position) > 7)
        {

            return;
        }

        GameObject effect = Instantiate(AssetConfig.PointEffect,
                                        new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        movement.SetTarget(transform);
    }

    //点击UI检测
    private bool IsTouchedUI()
    {
        if (Input.touchCount > 0)
        {
            //使用方法一：传递触摸手势 ID
            if (ClickIsOverUI.Instance.IsPointerOverUIObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("方法一： 点击在UI上");
                return true;
            }

            //使用方法二：传递触摸手势坐标
            if (ClickIsOverUI.Instance.IsPointerOverUIObject(Input.GetTouch(0).position))
            {
                Debug.Log("方法二： 手指点击在UI 上");
                return true;
            }

            //使用方法三：传递画布组件，传递触摸手势坐标
            //if (ClickIsOverUI.Instance.IsPointerOverUIObject(GetComponent<Canvas>(), Input.GetTouch(0).position))
            //{
            //    Debug.Log("方法三： 点击在UI 上");
            //    return true;
            //}
        }

        //检测鼠标点在Canvas上
        if (ClickIsOverUI.Instance.IsPointerOverUIObject())
        {
            Debug.Log("鼠标点在UI上");
            return true;
        }
        return false;
    }
}
