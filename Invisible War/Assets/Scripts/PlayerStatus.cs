using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private ParticleSystem ps;
    private float Visible_Time = 5.0f;
    public float freeze_time = 5.0f;
    private float remaining_freeze_time = 5.0f;
    private bool Hit = false;
    private bool freezed = false;
    private bool showTransparent = false;
    public Material iced_material;
    public Material transparent_material;
    public Material ghost_material;

    private CameraShootEffect cameraEffect;
    public GameObject onShoot;
    private SkinnedMeshRenderer playerRenderer;

    private void Start()
    {
        cameraEffect = GetComponentInChildren<CameraShootEffect>() as CameraShootEffect;
        playerRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
    }


    private void Update()
    {
        if (Hit || freezed || showTransparent)
        {
            playerRenderer.enabled = true;
            if (freezed && remaining_freeze_time > 0)
            {
                playerRenderer.material = iced_material;
                remaining_freeze_time -= Time.deltaTime;
            }
            else
            {
                remaining_freeze_time = freeze_time;
                playerRenderer.material = transparent_material;
                freezed = false;
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
        pc.enabled = true;
    }

    public void GetFreezed()
    {
        freezed = true;
    }

    public void setTransparent(bool flag)
    {
        showTransparent = flag;
    }

}
