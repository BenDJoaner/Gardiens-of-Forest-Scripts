using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPath : MonoBehaviour
{
    MenuPlayerMovement movement;
    // Use this for initialization
    void Start()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MenuPlayerMovement>();
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
        GameObject effect = Instantiate(AssetConfig.PointEffect,
                                        new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        movement.SetTarget(transform);
    }

    //点击UI检测
    private bool IsTouchedUI()
    {
        if (Input.touchCount > 0)
        {
            if (ClickIsOverUI.Instance.IsPointerOverUIObject(Input.GetTouch(0).fingerId))
            {
                return true;
            }

            if (ClickIsOverUI.Instance.IsPointerOverUIObject(Input.GetTouch(0).position))
            {
                return true;
            }
        }
        if (ClickIsOverUI.Instance.IsPointerOverUIObject())
        {
            return true;
        }
        return false;
    }
}
