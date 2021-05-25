using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Android;
using UnityEngine;

[Serializable]
public class Reaction
{
    public string name; // Name to be able to find reactions easier
    
    public List<GameObject> reactants;
    public List<GameObject> products;

    public float activationEnergy; // The amount of kinetic energy required for a successful collision

}
