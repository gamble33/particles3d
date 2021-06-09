using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ReactionHandler : MonoBehaviour
{
    public List<Reaction> reactions; // All possible reactions that can occur

    /// <summary>
    /// When a collision occurs between particles, CheckForReaction() is called which checks if there is enough total
    /// energy (at least enough for activation energy) 
    /// </summary>
    /// <param name="potentialReactants"></param>
    public void CheckForReaction(List<Particle> potentialReactants)
    {
        List<ParticleType> potentialParticleTypes =
            potentialReactants.Select(particle => particle.ParticleType).ToList();

        foreach (Reaction reaction in reactions)
        {
            if (CheckEqualParticles(reaction.reactants, potentialParticleTypes))
            {
                // Collided particles are reactants of a certain reaction

                float totalEnergy = CalculateTotalKineticEnergy(potentialReactants);
                // Checks that the particles have enough total kinetic energy [JOULES]
                // +
                // to react
                if (totalEnergy >= reaction.ActivationEnergy)
                {
                    // Reaction can occur
                    React(potentialReactants, reaction);
                }
            }
        }
    }

    /// <summary>
    /// Checks that two sets of particles are equal to each other
    /// </summary>
    /// <param name="set1">First set of particles</param>
    /// <param name="set2">Second set of particles</param>
    /// <returns></returns>
    private bool CheckEqualParticles(List<ParticleType> set1, List<ParticleType> set2)
    {
        if (set1.Count != set2.Count) return false; // TODO: Allow different length particle arrays
        foreach (ParticleType particleSet1 in set1)
        {
            bool particleFound = false; // Flag (IK -__-)
            foreach (ParticleType particleSet2 in set2)
            {
                if (particleSet2.name == particleSet1.name) particleFound = true;
            }

            if (!particleFound) return false;
        }

        return true;
    }

    private void React(List<Particle> reactants, Reaction reaction)
    {
        Debug.Log($"Reaction occured {reaction.name}");
        // TODO: Implement object pooling
        
        // There are two particles so each particle gains half of the kinetic energy released by reaction
        reactants[0].ParticleType = reaction.products[0];
        reactants[0].AddKineticEnergy(reaction.ChangeInEnergy/2);
        
        reactants[1].ParticleType = reaction.products[1];
        reactants[1].AddKineticEnergy(reaction.ChangeInEnergy/2);
    }

    /// <summary>
    /// Returns total kinetic energy [JOULES] of a list of particles 
    /// </summary>
    /// <param name="particles"></param>
    /// <returns></returns>
    private float CalculateTotalKineticEnergy(List<Particle> particles)
    {
        float totalEnergy = 0f;
        for (int i = 0; i < particles.Count; i++)
        {
            totalEnergy += particles[i].KineticEnergy;
        }

        return totalEnergy;
    }

    private void Awake()
    {
        InitializeReactions();
    }

    private void InitializeReactions()
    {
        foreach (Reaction reaction in reactions)
        {
            float reactantEnergy = 0; // Total potential energy of reactants.

            float productEnergy = 0; // Total potential energy of products.

            foreach (ParticleType reactant in reaction.reactants)
            {
                reactantEnergy += reactant.potentialEnergy;
            }

            foreach (ParticleType product in reaction.products)
            {
                productEnergy += product.potentialEnergy;
            }

            if (reactantEnergy > reaction.transitionState)
                Debug.LogError($"Reaction ({reaction.name}) " +
                               $"has reactants with an energy of {reactantEnergy} which is higher than the transition state " +
                               $"energy {reaction.transitionState}");

            reaction.ActivationEnergy = reaction.transitionState - reactantEnergy;
            
            // The amount of kinetic energy gained
            reaction.ChangeInEnergy = reactantEnergy - productEnergy;
            
        }
    }
}