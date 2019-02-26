using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleDecalPool decalPool;
    public AudioClip Fire_Sound;
    public Image cdImage;
    public Image crosshair;
    private bool Pressed = false;
    private AudioSource source;
    private string playerID;
    public float Fire_CD = 30.0f;
    private bool On_CoolDown = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControl pc = gameObject.GetComponent<PlayerControl>();
        playerID = pc.playerID;
        source = gameObject.GetComponent<AudioSource>();
        if (cdImage != null)
        {
            cdImage.fillAmount = 0;
        }
        // center crosshair
        RectTransform rectTransform = crosshair.gameObject.GetComponent<RectTransform>();
        Vector2 oldPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(oldPosition.x - Screen.width * 0.25f, oldPosition.y - Screen.height*0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire" + playerID) && !Pressed)
        {
            Pressed = true;
            if (!On_CoolDown)
            {
                Shooting();
            }
        } else if (Input.GetButtonUp("Fire" + playerID))
        {
            Pressed = false;
        }

        if (On_CoolDown)
        {
            cdImage.fillAmount -= 1 / Fire_CD * Time.deltaTime;
            if (cdImage.fillAmount<=0)
            {
                On_CoolDown = false;
            }
            // hide paintgun and crosshair
            crosshair.gameObject.SetActive(false);
        }
        else
        {
            crosshair.gameObject.SetActive(true);
        }
    }

    void Shooting()
    {
        decalPool.ClearParticles();
        ParticleSystem.MainModule psMain = particleLauncher.main;
        particleLauncher.Emit(10);
        cdImage.fillAmount = 1;
        source.PlayOneShot(Fire_Sound, 1f);
        On_CoolDown = true;
    }

}
