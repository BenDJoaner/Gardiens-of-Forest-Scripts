using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class HUDManager : MonoBehaviour
{

    public GameObject PlayerCanva;
    public GameObject MainCanva;
    public Text CharMessage;
    public Text AlramMessage;

    GameBooleanManager GBM;
    GameObject MainCamera;
    Animator PlayerCanvaAnim;
    //Animator MainCanvaAnim;
    Blur blur;

    // Use this for initialization
    void Start()
    {
        PlayerCanvaAnim = PlayerCanva.GetComponent<Animator>();
        GBM = GetComponent<GameBooleanManager>();
        //MainCanvaAnim = MainCanva.GetComponent<Animator>();
        MainCamera = Camera.main.gameObject;
        blur = MainCamera.GetComponent<Blur>();
        blur.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPCMessage(string msg, Color color)
    {
        CharMessage.text = msg;
        CharMessage.color = color;
        PlayerCanvaAnim.SetTrigger("active");
    }

    public void ShowRangAnim(bool val)
    {
        PlayerCanvaAnim.SetBool("showrang", val);
    }
}
