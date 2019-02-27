using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private ParticleSystem ps;
    private float Visible_Time = 5.0f;
    private bool Hit = false;

    private CameraShootEffect cameraEffect;
    public GameObject onShoot;

    private void Start()
    {
        //ps = GetComponentInChildren<ParticleSystem>();
        //ps.Stop();

        cameraEffect = GetComponentInChildren<CameraShootEffect>() as CameraShootEffect;
    }


    private void Update()
    {
        if (Hit)
        {
            Visible_Time -= Time.deltaTime;
            StartCoroutine(cameraEffect.Shake(0.25f, .1f));
           // onShoot.gameObject.SetActive(true);

            if (Visible_Time < 0)
            {
                Hit = false;
                cameraEffect.start = false;
            //    onShoot.gameObject.SetActive(false);
                Visible_Time = 5.0f;
            }
        }

        //if (Hit)
        //{
        //    Visible_Time = 5f;
        //    Hit = false;
        //    StartCoroutine(cameraEffect.Shake(0.25f, .1f));
        //    onShoot.gameObject.SetActive(true);
        //}

        //if (Visible_Time > 0f)
        //{
        //    Visible_Time -= Time.deltaTime;
        //}
        //else if (Visible_Time < 0f)
        //{
        //    ps.Stop();
        //    Visible_Time = 0f;
        //    onShoot.gameObject.SetActive(false);
        //}
    }

    public void GetHit()
    {
        //ps.Play();
        Hit = true;
        PlayerControl pc = GetComponent<PlayerControl>();
        pc.enabled = true;
    }

}
