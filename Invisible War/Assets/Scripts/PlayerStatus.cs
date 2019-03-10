using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private ParticleSystem ps;
    private float Visible_Time = 5.0f;
    public float freeze_time = 5.0f;
    public GameObject freezeEffect;
    public AudioClip shiveringOne;
    private float remaining_freeze_time = 5.0f;
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
    public bool isHunter;
    private bool firstTime = true;
    private void Start()
    {
        cameraEffect = GetComponentInChildren<CameraShootEffect>() as CameraShootEffect;
        playerRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        playerRenderer.enabled = false;
        sources = gameObject.GetComponents<AudioSource>();

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
                    freezeEffect.SetActive(true);
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
                    freezeEffect.SetActive(false);
                }
                remaining_freeze_time = freeze_time;
                playerRenderer.material = transparent_material;
                freezed = false;
                firstTime = true;
            }
            if (Hit && Visible_Time > 0)
            {
                Visible_Time -= Time.deltaTime;
                StartCoroutine(cameraEffect.Shake(0.25f, .1f));
                playerRenderer.material = ghost_material;

            } else
            {
                Hit = false;
                if (cameraEffect != null)
                {
                    cameraEffect.start = false;
                }
                Visible_Time = 5.0f;
            }
            if (caught)
            {
                playerRenderer.material = ghost_material;
            }
        }
        else
        {
            playerRenderer.enabled = false;
            playerRenderer.material = transparent_material;
        }

    }

    public void GetHit()
    {
        //ps.Play();
        Hit = true;
        PlayerControl pc = GetComponent<PlayerControl>();
        if (pc != null)
        {
            pc.enabled = true;
        }
    }

    public void GetFreezed()
    {
        freezed = true;
    }

    public void setTransparent(bool flag)
    {
        showTransparent = flag;
    }
    public void Caught()
    {
        caught = true;
    }
}
