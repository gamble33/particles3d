using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public static CollisionHandler Instance;

    [SerializeField] private ReactionHandler reactionHandler;

    private Dictionary<string, ParticleStats> collidedParticles = new Dictionary<string, ParticleStats>();

    /// <summary>
    /// This function is called when a particle collides with another particle
    /// The particle's (script) that calls this function is added to a dictionary containing all particles that are currently colliding.
    /// This function gets the resultant velocities of both particles ONLY AFTER both particle's (scripts) call this function
    /// </summary>
    /// <param name="particle"></param>
    /// <param name="collided"></param>
    /// <param name="pVelocity"></param>
    /// <param name="pMass"></param>
    public void SetParticlesResultantVelocities(Particle particle, Particle collided, Vector3 pVelocity,
        float pMass = 1f)
    {
        collidedParticles.Add(particle.GetParticleID(), new ParticleStats {velocity = pVelocity, mass = pMass});
        if (collidedParticles.ContainsKey(collided.GetParticleID()))
        {
            ParticleStats collStats = collidedParticles[collided.GetParticleID()];

            Vector2 xVelocities = CalculateVelocities(pMass, collStats.mass, pVelocity.x, collStats.velocity.x);

            Vector2 yVelocities = CalculateVelocities(pMass, collStats.mass, pVelocity.y, collStats.velocity.y);

            Vector2 zVelocities = CalculateVelocities(pMass, collStats.mass, pVelocity.z, collStats.velocity.z);


            particle.SetMyVelocity(new Vector3(xVelocities.x, yVelocities.x, zVelocities.x));
            collided.SetMyVelocity(new Vector3(xVelocities.y, yVelocities.y, zVelocities.y));

            collidedParticles.Remove(collided.GetParticleID());
            collidedParticles.Remove(particle.GetParticleID());
        }

        particle.CalculateKineticEnergy();
        collided.CalculateKineticEnergy();
        
        List<Particle> potentialReactants = new List<Particle>
        {
            particle, collided
        };
        reactionHandler.CheckForReaction(potentialReactants);
    }

    /// <summary>
    /// This function calculates the resultant velocities of two particles that collided
    /// using two equations derived from the conservation of momentum and kinetic energy.
    /// </summary>
    /// <param name="particle1Mass"></param>
    /// <param name="particle2Mass"></param>
    /// <param name="particle1Velocity"></param>
    /// <param name="particle2Velocity"></param>
    /// <returns></returns>
    private Vector2 CalculateVelocities(float particle1Mass, float particle2Mass, float particle1Velocity,
        float particle2Velocity)
    {
        float velocityParticle1 =
            ((particle1Mass - particle2Mass) / (particle1Mass + particle2Mass)) * particle1Velocity +
            ((2 * particle2Mass) / (particle1Mass + particle2Mass)) * particle2Velocity;
        float velocityParticle2 = ((2 * particle1Mass) / (particle1Mass + particle2Mass)) * particle1Velocity +
                                  ((particle2Mass - particle1Mass) / (particle1Mass + particle2Mass)) *
                                  particle2Velocity;

        return new Vector2(velocityParticle1, velocityParticle2);
    }

    private void Awake()
    {
        // Singleton
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }
}