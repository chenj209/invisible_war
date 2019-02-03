using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private ParticleSystem ps;
 
    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Stop();
    }

    public void GetHit()
    {
        ps.Play();
    }

}
