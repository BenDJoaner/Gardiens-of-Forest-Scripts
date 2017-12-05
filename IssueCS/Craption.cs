using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Craption : MonoBehaviour
{
    NavMeshObstacle block;
    Animator anim;
    // Use this for initialization
    void Start()
    {
        block = GetComponent<NavMeshObstacle>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(AssetConfig.ChargeEffect_3, transform.position, Quaternion.identity);
    }

    private void OnTriggerExit(Collider other)
    {
        block.enabled = true;
        anim.SetTrigger("active");
    }
}
