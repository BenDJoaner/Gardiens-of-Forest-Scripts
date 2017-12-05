using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class XiniuSkill : MonoBehaviour
{

    public LayerMask targetingLayerMask = -1;
    public GameObject SkillEffect;
    public GameObject HitEffect;
    public Slider CDslider;
    //public GameObject ArrowObject;

    GameObject SelectedHunter;
    GameObject player;
    Camera cam;
    AudioSource AS;
    HUDManager HUD;
    GameBooleanManager GBM;
    //GameObject[] Hunters = { };
    //GameObject[] arrowArr = { };

    bool activeCharge;
    bool activeSkill;
    bool isColding;
    float CDtime = 6;
    float targetingRayLength = 100;

    private string launchAudio = "Audio/launchSkill";

    // Use this for initialization
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        AS = GetComponent<AudioSource>();
        GBM = GetComponent<GameBooleanManager>();
        HUD = GetComponent<HUDManager>();
        SkillEffect.SetActive(false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GBM.GamePause || GBM.PlayerUnlocking)
        {
            return;
        }

        if (activeCharge)
        {

            if (!SelectedHunter.GetComponent<EnemyAI>().Dead)
            {
                ChargeFunction();
            }
            else
            {
                StopChargeFunction();
            }
        }

        if (activeSkill)
        {
            if (!isColding)
            {
                TargetingRaycast();
            }
            if (CDtime > 0)
            {
                SkillEffect.SetActive(true);
                CDtime -= 2 * Time.deltaTime;
            }
            else
            {
                StopChargeFunction();
            }
        }
    }

    private void Update()
    {
        if (GBM.GamePause) return;
        CDslider.value = CDtime;
        SkillCoodTime();

        if (GBM.GameLost)
        {
            activeSkill = false;
            activeCharge = false;
            SkillEffect.SetActive(false);
        }
    }

    void SkillCoodTime()
    {
        //如果冷却中，每秒恢复1能量
        if (isColding)
        {
            if (CDtime < 6)
            {
                CDtime += 0.5f * Time.deltaTime;
            }
            else
            {
                CDtime = 6;
                //HUD.activeskill(true);
                isColding = false;//当且仅当CDtime为6时，isColding冷却结束
            }
        }
    }

    //CHARGING
    void ChargeFunction()
    {
        //GBM.PlayerMoveable = false;
        //GBM.PlayerUncatchable = true;
        player.transform.LookAt(SelectedHunter.transform.position);
        player.GetComponent<Animator>().SetBool("charge", true);
        //HUD.activeskill(false);
        player.GetComponent<PlayerMovement>().SetTarget(SelectedHunter.transform);
        player.transform.Translate(Vector3.forward * Time.deltaTime * 7);
        //GBM.PlayerHiding = false;
    }

    void StopChargeFunction()
    {
        //HUD.activeskill(false);
        SkillEffect.SetActive(false);
        GameObject hit = Instantiate(HitEffect);
        hit.transform.LookAt(player.transform.position);
        hit.transform.position = player.transform.position;

        player.GetComponent<Animator>().SetBool("charge", false);
        SelectedHunter = null;
        //GBM.PlayerMoveable = true;
        //GBM.PlayerUncatchable = false;
        GBM.PlayerOnPosition = false;
        CDtime = 0;
        isColding = true;
        activeSkill = false;
        activeCharge = false;
    }

    void TargetingRaycast()
    {
        Transform targetTransform = null;
        if (cam != null)
        {
            RaycastHit hitInfo;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, targetingRayLength, targetingLayerMask.value))
            {
                targetTransform = hitInfo.collider.transform;
                if (SelectedHunter != hitInfo.collider && hitInfo.collider.tag == "Hunter")
                {
                    if (Input.GetButtonDown("Fire1"))
                        SelectedHunter = hitInfo.collider.gameObject;
                }
                if (SelectedHunter == hitInfo.collider.gameObject && Input.GetButtonDown("Fire1"))
                {
                    activeCharge = true;
                }
            }
            if (SelectedHunter)
            {
                Vector3 arrowposition = SelectedHunter.transform.position;
                arrowposition.y = arrowposition.y + 2;
            }
        }
    }

    public void SetSkillActive()
    {
        if (!activeSkill && !activeCharge && !isColding)
        {
            AS.clip = (AudioClip)Resources.Load(launchAudio);
            AS.Play();
            AS.loop = false;
            activeSkill = true;
        }
    }
}
