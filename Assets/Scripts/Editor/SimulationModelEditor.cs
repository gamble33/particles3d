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

        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Spawn Particle"))
        {
            simulationModel.SpawnParticle();
        }

        if (GUILayout.Button("Spawn 5 Particles"))
        {
            for(int i=0;i<5;i++) simulationModel.SpawnParticle();
        }

        if (GUILayout.Button("Spawn 10 Particles"))
        {
            for(int i=0;i<10;i++) simulationModel.SpawnParticle();
        }
        
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Next Container"))
        {
            simulationModel.NextContainer();
        }

        DrawDefaultInspector();
    }
}
