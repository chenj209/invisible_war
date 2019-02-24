using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    //public ParticleSystem splatterParticles;
    public Gradient particleColorGradient;
    public ParticleDecalPool splatterDecalPool;
    public GameObject playerDecal;

    List<ParticleCollisionEvent> collisionEvents;
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
                decal.transform.SetParent(other.transform);
                Destroy(decal, 5);
                other.GetComponent<PlayerStatus>().GetHit();
            }
            else
            {
                splatterDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
                //EmitAtLocation(collisionEvents[i]);
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
