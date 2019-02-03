using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private ParticleSystem ps;
    private float Visible_Time = 5.0f;
    private bool Hit = false;

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Stop();
    }


    private void Update()
    {
        if (Hit)
        {
            Visible_Time -= Time.deltaTime;
            if (Visible_Time < 0)
            {
                Hit = false;
                ps.Stop();
                Visible_Time = 5.0f;
            }
        }
    }

    public void GetHit()
    {
        ps.Play();
        Hit = true;
    }

}
