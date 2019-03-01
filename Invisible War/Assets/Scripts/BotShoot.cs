using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleDecalPool decalPool;
    public AudioClip Fire_Sound;
    private AudioSource source;
    public bool shoot = false;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
       

    }

    // Update is called once per frame
    void Update()
    {
        
        if (shoot)
        {
            Shooting();
            shoot = false;
        }
    }

    void Shooting()
    {
        decalPool.ClearParticles();
        ParticleSystem.MainModule psMain = particleLauncher.main;
        particleLauncher.Emit(10);
        source.PlayOneShot(Fire_Sound, 1f);
    }
}
