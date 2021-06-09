using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Android;
using UnityEngine;

public enum ReactionType
{
    Exothermic,
    Endothermic
}

[Serializable]
public class Reaction
{
    public string name; // Name to be able to find reactions easier
    public List<ParticleType> reactants;
    public List<ParticleType> products;
    public float transitionState;
    public ReactionType reactionType;
    public float ActivationEnergy { get; set; } // The amount of kinetic energy required for a successful collision
    public float ChangeInEnergy { get; set; }
}