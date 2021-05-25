using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimulationModel))]
public class SimulationModelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        SimulationModel simulationModel = (SimulationModel) target;
        
        if (GUILayout.Button("Spawn Particle"))
        {
            simulationModel.SpawnParticle();
        }

        if (GUILayout.Button("Next Container"))
        {
            simulationModel.NextContainer();
        }

        DrawDefaultInspector();
    }
}
