using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SimulationModel : MonoBehaviour
{

    public ParticleModel particleModel;
    public ContainerModel containerModel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnParticle()
    {
        GameObject currentContainer = containerModel.GetCurrentContainer();
        particleModel.InstantiateParticle(currentContainer);
    }

    public void NextContainer()
    {
        containerModel.NextContainer();
        particleModel.RemoveNonContainerParticles(containerModel.GetCurrentContainer());
        
    }
}
