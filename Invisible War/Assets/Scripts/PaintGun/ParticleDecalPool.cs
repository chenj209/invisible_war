using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalPool : MonoBehaviour
{
    public int maxDecals = 100;
    [SerializeField]
    private ParticleDecalData[] particleData;
    public int index;
    public float decalSizeMin = 20f;
    public float decalSizeMax = 60f;
    private ParticleSystem.Particle[] particles;
    public ParticleSystem decalParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];
        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData();
        }
    }
    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        SetParticleData(particleCollisionEvent, colorGradient);
        DisplayParticles();
    }
    // Update is called once per frame
    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        if (index >= maxDecals)
        {
            index = 0;
        }
        // record collision position, rotation, size and color
        particleData[index].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360);
        particleData[index].rotation = particleRotationEuler;
        particleData[index].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[index].color = colorGradient.Evaluate(Random.Range(0f, 1f));

        index++;
    }
    void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;
        }
        decalParticleSystem.SetParticles(particles, particles.Length);
    }

    public void ClearParticles()
    {
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];
        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData();
        }
        decalParticleSystem.SetParticles(particles, particles.Length);
    }
}
