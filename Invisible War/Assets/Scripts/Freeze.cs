using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Freeze : MonoBehaviour
{
    public Image cdImage;
    public float Freeze_CD;
    private bool On_CoolDown = false;
    public GameObject freezeEffect;
    public AudioClip freezeSound;
    private AudioSource[] sources;
    public GameObject catcher;
    public bool inTutorial;
    public bool bot;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (!bot)
        {
            cdImage.fillAmount = 0;
        }
        timer = 0;
        sources = gameObject.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bot)
        {
            if (Input.GetButton("Fire02"))
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
                cdImage.fillAmount -= 1 / Freeze_CD * Time.deltaTime;
                if (cdImage.fillAmount < 0.8 && !inTutorial)
                {
                    PlayerControl hunter = catcher.GetComponent<PlayerControl>();
                    hunter.enabled = true;
                }
                if (cdImage.fillAmount <= 0)
                {
                    On_CoolDown = false;
                }
            }
            else
            {
                timer -= 1 / Freeze_CD * Time.deltaTime;
                if (timer < 0.8)
                {
                    PlayerControl hunter = catcher.GetComponent<PlayerControl>();
                    hunter.enabled = true;
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
        timer = 1;
        On_CoolDown = true;
        sources[0].PlayOneShot(freezeSound, 1f);
        freezeEffect.SetActive(false);
        freezeEffect.SetActive(true);
        if (inTutorial && !bot)
        {
            if (Vector3.Distance(catcher.transform.position, transform.position) < 60)
            {
                Catcher hunter = catcher.GetComponent<Catcher>();
                hunter.stopRunning();
                 catcher.GetComponent<PlayerStatus>().GetFreezed();
            }
        }else if (inTutorial && bot)
        {
            if (Vector3.Distance(catcher.transform.position, transform.position) < 100)
            {
                PlayerControl hunter = catcher.GetComponent<PlayerControl>();
                hunter.enabled = false;
                catcher.GetComponent<PlayerStatus>().GetFreezed();
            }
        }else if (!inTutorial)
        {
            if (Vector3.Distance(catcher.transform.position, transform.position) < 30)
            {
                PlayerControl hunter = catcher.GetComponent<PlayerControl>();
                hunter.enabled = false;
                catcher.GetComponent<PlayerStatus>().GetFreezed();
            }
        }
    }
}
