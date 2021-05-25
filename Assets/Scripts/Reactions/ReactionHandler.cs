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
            if(reaction.reactants.Equals(potentialReactants))
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

    private void React(List<Particle> reactants, Reaction reaction)
    {
        // TODO: React
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
