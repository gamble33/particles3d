using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleIDModel : MonoBehaviour
{
    private int _particleCount = -1;

    public string GetNewParticleID()
    {
        _particleCount++;
        return $"PARTICLE{_particleCount.ToString()}";
        
    }

}
