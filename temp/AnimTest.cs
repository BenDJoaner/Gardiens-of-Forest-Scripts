using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour {
//动画名称  
  public const string ANIM_NAME = "New Animation";  
  //模型对象  
  public GameObject obj;  
  //进度条  
  public float hSliderValue = 0.0f;  
  
  public float animLegth = 0.0f;  
  
  
  void Start()  
  {  
      //得到模型动画  
      //obj = GameObject.Find("man");  
      //得到动画播放长度  
      animLegth = obj.GetComponent<Animation>().GetComponent<Animation>()[ANIM_NAME].length;  
  }  
  
  void OnGUI()  
  {  
      //显示信息  
      string show = "CurrentAnimationLength" + hSliderValue.ToString() + "(s)" + "/" + animLegth.ToString() + "(s)";  
      GUILayout.Label(show);  
      //计算拖动条拖动数值  
      hSliderValue = GUILayout.HorizontalSlider(hSliderValue, 0.0f, 5.0f, GUILayout.Width(200));  
      //绘制动画帧  
      PlaysilderAnimation(obj, hSliderValue);  
  }  
  
  public void PlaysilderAnimation(GameObject manObject, float times)  
  {  
  
      //播放动画  
       
      if(!manObject.GetComponent<Animation>().isPlaying)  
      {  
          //    manObject.animation.Play(ANIM_NAME);  
          manObject.GetComponent<Animation>().Play(ANIM_NAME);  
      }  
      //设置动画时间  
      manObject.GetComponent<Animation>().GetComponent<Animation>()[ANIM_NAME].time = times;  
  }  


}
