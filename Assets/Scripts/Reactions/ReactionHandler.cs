using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using UnityEngine;

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
        foreach(Reaction reaction in reactions)
        {
            if(CheckEqualParticles(reaction.reactants, potentialReactants))
            {
                // Collided particles are reactants of a certain reaction
                
                float totalEnergy = CalculateTotalKineticEnergy(potentialReactants);
                // Checks tht the particle have enough total kinetic energy [JOULES] to react
                if (totalEnergy >= reaction.activationEnergy)
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
    private bool CheckEqualParticles(List<Particle> set1, List<Particle> set2)
    {
        if (set1.Count != set2.Count) return false; // TODO: Allow different length particle arrays
        foreach(Particle particleSet1 in set1)
        {
            bool particleFound = false; // Flag (IK -__-)
            foreach (Particle particleSet2 in set2)
            {
                if (particleSet2.natureName == particleSet1.natureName) particleFound = true;
            }

            if (!particleFound) return false;
        }

        return true;
    }

    private void React(List<Particle> reactants, Reaction reaction)
    {
        // TODO: Implement object pooling
        
        reactants[0].SetNature(reaction.products[0].GetNature());
        reactants[0].natureName = reaction.products[0].natureName;
        reactants[1].SetNature(reaction.products[1].GetNature());
        reactants[1].natureName = reaction.products[1].natureName;
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
    
}
