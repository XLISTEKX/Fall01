using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldParticleControler : MonoBehaviour
{
    public int moneyCount;

    ParticleSystem ps;

    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
       // ps.emission.SetBurst(0, new ParticleSystem.Burst(0,moneyCount,1,0));
        ps.Emit(moneyCount);
        Invoke("updateTriger", 0.01f);

    }
    void updateTriger()
    {
        ps.trigger.AddCollider(GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Collider2D>());
    }
    private void OnParticleTrigger()
    {
        int trigeredParticels = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        GameObject.FindGameObjectWithTag("EventSystem").GetComponent<PlayerStatsInv>().money += (uint) trigeredParticels;

        for(int i = 0; i < trigeredParticels; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            particles[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        if(ps.particleCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
