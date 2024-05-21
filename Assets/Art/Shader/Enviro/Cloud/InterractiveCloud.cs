using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractiveCloud : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float onExitRemainingLifeTime = 0;

    [Header("References")]
    [SerializeField] private new ParticleSystem particleSystem;

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();  
        if (particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, particles) <= 0 ) { return; }
        ParticleSystem.Particle[] particleArray = particles.ToArray();

        for (int i = 0; i < particleArray.Length; i++) 
        {
            if (particleArray[i].remainingLifetime <= onExitRemainingLifeTime ) 
            {
                continue; 
            }
            particleArray[i].remainingLifetime = onExitRemainingLifeTime;   
        }
        particles = new List<ParticleSystem.Particle>(particleArray);
        ParticlePhysicsExtensions.SetTriggerParticles(particleSystem, ParticleSystemTriggerEventType.Exit, particles);
    }

}
