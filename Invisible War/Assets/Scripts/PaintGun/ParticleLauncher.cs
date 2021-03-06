﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    //public ParticleSystem splatterParticles;
    public Gradient particleColorGradient;
    public ParticleDecalPool splatterDecalPool;
    public GameObject playerDecal;
    public bool inTutorial;
    List<ParticleCollisionEvent> collisionEvents;
    public GameObject directionArrow;
    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            if (other.tag == "Player")
            {
                GameObject decal = Instantiate(playerDecal, collisionEvents[i].intersection, Quaternion.LookRotation(-1 * collisionEvents[i].normal));
                decal.transform.up = collisionEvents[i].normal;
                decal.transform.Rotate(Vector3.up, Random.Range(0, 360), Space.Self);

                decal.transform.SetParent(other.transform);
                Destroy(decal, GameConfig.instance.paintgunEffectDuration);
                if (other.GetComponent<PlayerStatus>() != null)
                {
                    other.GetComponent<PlayerStatus>().GetHit();
                }
                if (inTutorial)
                {
                    TutorialStateController.HunterShootDone = true;
                    directionArrow.SetActive(true);
                }
            }
            else
            {
                if (splatterDecalPool)
                    splatterDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            }
        }
    }


    //void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    //{
    //    splatterParticles.transform.position = particleCollisionEvent.intersection;
    //    splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
    //    ParticleSystem.MainModule psMain = splatterParticles.main;
    //    psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));
    //    splatterParticles.Emit(1);
    //}

}
