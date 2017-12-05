using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider SliderProcess;
    public Text TextProcess;

    //异步对象
    AsyncOperation async;
    //读取场景的进度，它的取值范围在0 - 1 之间。
    int progress = 0;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(loadScene());
    }
    IEnumerator loadScene()
    {
        async = Application.LoadLevelAsync(GlobleData.loadName);
        //读取完毕后返回， 系统会自动进入C场景
        yield return async;
    }
    // Update is called once per frame
    void Update()
    {
        progress = (int)(async.progress * 100);
        SliderProcess.value = progress;
        TextProcess.text = progress + "%";
    }
}
