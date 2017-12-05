using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newfloor : MonoBehaviour
{
    public GameObject checkpoint;
    public int CheckPiontNumber;
    private bool active;
    private Vector3 currentPosition;
    private Vector3 playerpos;
    float risetime;

    // Use this for initialization
    void Start()
    {
        checkpoint = GameObject.Find("checkpoint (" + CheckPiontNumber + ")");
        if (!checkpoint)
        {
            active = true;
        }
        else if (checkpoint.GetComponent<newcheckpoint>().Activ)
        {
            active = true;
        }
        currentPosition = transform.position;                                           //初始位置
        transform.position = new Vector3(currentPosition.z, -10, currentPosition.z);
    }

    void riseFunction()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (risetime < 100)
        {
            risetime += 10 * Time.deltaTime;
        }
        if (Mathf.Abs(playerpos.x - currentPosition.x) < risetime && Mathf.Abs(playerpos.z - currentPosition.z) < risetime)
        {
            iTween.MoveTo(gameObject, currentPosition, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkpoint)
        {
            active = true;
        }
        else if (checkpoint.GetComponent<newcheckpoint>().Activ)
        {
            active = true;
        }
        else
            GameObject.Destroy(this.gameObject);
        if (active && (transform.position.y != currentPosition.y))
        {
            riseFunction();
        }
    }
}
