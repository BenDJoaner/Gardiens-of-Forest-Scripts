using UnityEngine;
using System.Collections;

// 脚本使用在摄像机身上
public class CameraFollow : MonoBehaviour
{
    public Transform stander; // 主角位置
    public float speed = 5f; // 相机速度  

    Vector3 distance;
    void Start()
    {
        distance = transform.position - stander.position;
    }


    void FixedUpdate()
    {
        Vector3 targetCamPos = stander.position + distance;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, speed * Time.deltaTime);
    }
}
