using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DistributionGraph : MonoBehaviour
{

    public static DistributionGraph Instance;
    
    private void Awake()
    {
        // Singleton
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
        
    }

    private void Start()
    {
        DebugGUI.SetGraphProperties("distribution", "probability", 0, 200, 2, new Color(1, 0.5f, 1), false);

    }

    public void SetGraph(double[] probabilities)
    {
        DebugGUI.ClearGraph("distribution");
        DebugGUI.SetGraphProperties("distribution", "probability", 0f, (float) probabilities.Max() + 0.01f, 2,
                    new Color(1, 0.5f, 1), false);
        
        for (int i = 0; i < probabilities.Length; i++)
        {
            DebugGUI.Graph("distribution", (float) probabilities[i]);
        }
        
    }
}
