using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsShowScript : MonoBehaviour
{
    public bool showTips;
    public bool starShow;

    public Text msg;

    public int TipsNumber;
    public GameObject MainCanva;

    GameBooleanManager GBM;
    HUDManager HUD;
    Animator MainCanvaAnim;
    // Use this for initialization
    void Start()
    {
        MainCanvaAnim = MainCanva.GetComponent<Animator>();
        GBM = GetComponent<GameBooleanManager>();
        if (starShow) MainCanvaAnim.SetTrigger("tipsactive");
    }

    // Update is called once per frame
    void Update()
    {
        switch (TipsNumber)
        {
            case 1:

                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    public void NextButtonFunction()
    {
        if (starShow) MainCanvaAnim.SetTrigger("tipsdeactive");
    }
}
