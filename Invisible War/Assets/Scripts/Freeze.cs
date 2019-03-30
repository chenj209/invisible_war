using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Freeze : MonoBehaviour
{
    public Image cdImage;
    private bool On_CoolDown = false;
    public GameObject freezeEffect;
    public AudioClip freezeSound;
    private AudioSource[] sources;
    public GameObject catcher;
    public GameObject freezeInstruction;
    public bool inTutorial;
    public bool bot;
    private float timerBot;
    private float timerNonBot;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!bot)
        {
            cdImage.fillAmount = 0;
        }
        timerBot = GameConfig.instance.freezeEffectDuration;
        timerNonBot = GameConfig.instance.freezeEffectDuration;
        sources = gameObject.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inTutorial)
        {
            if (Vector3.Distance(catcher.transform.position, transform.position) < GameConfig.instance.freezeDistance)
            {
                freezeInstruction.SetActive(true);
            }
            else
            {
                freezeInstruction.SetActive(false);
            }
        }
        if (!bot)
        {
            if (Input.GetButton("Fire02") || Input.GetKey(KeyCode.P))
            {
                if (!On_CoolDown)
                {
                    Skill();
                }
            
            }
        }
        if (On_CoolDown)
        {
            if (!bot)
            {
                cdImage.fillAmount -= 1 / GameConfig.instance.freezeCoolDown * Time.deltaTime;
                PlayerControl hunter = catcher.GetComponent<PlayerControl>();
              
                if (!hunter.enabled)
                {
                    timerNonBot -= Time.deltaTime;
                    if (timerNonBot < 0)
                    {
                        hunter.enabled = true;
                        timerNonBot = 5;
                        shooting huntershoot = catcher.GetComponent<shooting>();
                        if (huntershoot)
                            huntershoot.enabled = true;
                        CatchGhost catchability = catcher.GetComponentInChildren<CatchGhost>();
                        if (catchability)
                            catchability.enabled = true;
                    }
                }
                if (cdImage.fillAmount <= 0)
                {
                    On_CoolDown = false;
                }
            }
            else
            {
                PlayerControl hunter = catcher.GetComponent<PlayerControl>();
                if (!hunter.enabled)
                {
                    timerBot -= Time.deltaTime;
                    if (timerBot < 0)
                    {
                        hunter.enabled = true;
                        shooting huntershoot = catcher.GetComponent<shooting>();
                        if (huntershoot)
                            huntershoot.enabled = true;
                        CatchGhost catchability = catcher.GetComponentInChildren<CatchGhost>();
                        if (catchability)
                            catchability.enabled = true;
                        timerBot = 5;
                    }
                }
                
            }
        }
    }

    public void Skill()
    {
        if (!bot)
        {
            cdImage.fillAmount = 1;
        }
        On_CoolDown = true;
        if (sources[0])
        sources[0].PlayOneShot(freezeSound, 1f);
        Vector3 freezePosition = gameObject.transform.position;
        freezePosition.y = -14.6f;
        GameObject freezeEffectObj = Instantiate(freezeEffect, freezePosition, Quaternion.identity);
        Destroy(freezeEffectObj, 6);
        //if (inTutorial && !bot)
        //{
        //    if (Vector3.Distance(catcher.transform.position, transform.position) < 60)
        //    {
        //        Catcher hunter = catcher.GetComponent<Catcher>();
        //        shooting huntershoot = catcher.GetComponent<shooting>();
        //        if (huntershoot)
        //            huntershoot.enabled = false;
        //        CatchGhost catchability = catcher.GetComponentInChildren<CatchGhost>();
        //        if (catchability)
        //            catchability.enabled = false;
        //        hunter.stopRunning();
        //        catcher.GetComponent<PlayerStatus>().GetFreezed();
        //    }
        //}else if (inTutorial && bot)
        //{
        //    if (Vector3.Distance(catcher.transform.position, transform.position) < 100)
        //    {
        //        PlayerControl hunter = catcher.GetComponent<PlayerControl>();
        //        hunter.enabled = false;
        //        shooting huntershoot = catcher.GetComponent<shooting>();
        //        if (huntershoot)
        //            huntershoot.enabled = false;
        //        CatchGhost catchability = catcher.GetComponentInChildren<CatchGhost>();
        //        if (catchability)
        //            catchability.enabled = false;
        //        catcher.GetComponent<PlayerStatus>().GetFreezed();
        //        TutorialStateController.HunterFreezeDone = true;
        //    }
        //}else if (!inTutorial)
        //{
        if (Vector3.Distance(catcher.transform.position, transform.position) < GameConfig.instance.freezeDistance)
        {
            PlayerControl hunter = catcher.GetComponent<PlayerControl>();
            hunter.enabled = false;
            shooting huntershoot = catcher.GetComponent<shooting>();
            if (huntershoot)
                huntershoot.enabled = false;
            CatchGhost catchability = catcher.GetComponentInChildren<CatchGhost>();
            if (catchability)
                catchability.enabled = false;
            catcher.GetComponent<PlayerStatus>().GetFreezed();
        }
        
    }
}
