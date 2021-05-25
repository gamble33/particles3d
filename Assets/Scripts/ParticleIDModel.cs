using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleIDModel : MonoBehaviour
{
    private int particleCount = -1;

    public string GetNewParticleID()
    {
        particleCount++;
        return $"PARTICLE{particleCount.ToString()}";
        
    }

}
