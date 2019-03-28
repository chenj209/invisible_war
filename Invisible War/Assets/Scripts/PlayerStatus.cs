using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    private ParticleSystem ps;
    public bool invincible = false;
    //public GameObject freezeEffect;
    public Image freezeEffect;
    public Image hitEffect;
    public AudioClip shiveringOne;
    private float Visible_Time;
    private float remaining_freeze_time;
    private bool Hit = false;
    private bool freezed = false;
    private bool showTransparent = false;
    private bool caught = false;
    public Material iced_material;
    public Material transparent_material;
    public Material ghost_material;
    private AudioSource[] sources;
    private CameraShootEffect cameraEffect;
    public GameObject onShoot;
    private SkinnedMeshRenderer playerRenderer;
    public GameObject catchEffect;
    public bool isHunter;
    private bool firstTime = true;
    public bool isTutorial;

    public bool gethit = false;
    public bool getfreezed = false;


    private void Start()
    {
        cameraEffect = GetComponentInChildren<CameraShootEffect>() as CameraShootEffect;
        playerRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        resetPlayerTransparency();
        sources = gameObject.GetComponents<AudioSource>();
        Visible_Time = GameConfig.instance.paintgunEffectDuration;
        remaining_freeze_time = GameConfig.instance.freezeEffectDuration;
    }


    private void Update()
    {
        if (Hit || freezed || showTransparent || caught)
        {
            playerRenderer.enabled = true;
            if (freezed && remaining_freeze_time > 0)
            {
                if (freezeEffect && isHunter)
                {
                    //freezeEffect.SetActive(true);
                    freezeEffect.enabled = true;
                }
                if (firstTime && isHunter)
                {
                    sources[1].PlayOneShot(shiveringOne, 1.5f);
                    firstTime = false;
                }
                playerRenderer.material = iced_material;
                remaining_freeze_time -= Time.deltaTime;
            }
            else
            {
                if (freezeEffect)
                {
                    //freezeEffect.SetActive(false);
                    freezeEffect.enabled = false;
                }
                remaining_freeze_time = GameConfig.instance.freezeEffectDuration;
                playerRenderer.material = transparent_material;
                freezed = false;
                firstTime = true;
            }
            if (Hit && Visible_Time > 0)
            {
                Visible_Time -= Time.deltaTime;
                StartCoroutine(cameraEffect.Shake(0.25f, .1f));
                playerRenderer.material = ghost_material;
                if (hitEffect && !isHunter)
                hitEffect.enabled = true;
                if (isTutorial)
                {
                    TutorialStateController.HunterShootDone = true;
                }
            } else
            {
                Hit = false;
                if (hitEffect)
                hitEffect.enabled = false;
                if (cameraEffect != null)
                {
                    cameraEffect.start = false;
                }
                Visible_Time = GameConfig.instance.paintgunEffectDuration;
            }
            if (caught)
            {
                playerRenderer.material = ghost_material;
            }
        }
        else
        {
            resetPlayerTransparency();
        }

    }

    public void GetHit()
    {
        //ps.Play();
        Hit = true;
        gethit = true;
        PlayerControl pc = GetComponent<PlayerControl>();
        if (pc != null)
        {
            pc.enabled = true;
        }
    }

    public void GetFreezed()
    {
        freezed = true;
        getfreezed = true;
    }

    public void setTransparent(bool flag)
    {
        showTransparent = flag;
    }
    public void Caught()
    {
        if (!invincible)
        {
            Destroy(gameObject, 1);
            GameObject effect = Instantiate(catchEffect, this.gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(effect, 2);
            caught = true;
        }
    }
    private void resetPlayerTransparency()
    {
        if (isHunter)
        {
            playerRenderer.enabled = !GameConfig.instance.hunterTransparent;
        } else
        {
            playerRenderer.enabled = !GameConfig.instance.ghostTransparent;
        }
        playerRenderer.material = transparent_material;
    }
}
