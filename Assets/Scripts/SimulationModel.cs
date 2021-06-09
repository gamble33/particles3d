using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SimulationModel : MonoBehaviour
{
    [SerializeField] private double a;
    [SerializeField] private int energies;

    private double _aLast;
    private double _energiesLast;

    [Header("References")] public ParticleModel particleModel;
    public ContainerModel containerModel;


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

    private void Start()
    {
        CalculateDistribution();
    }

    private void CalculateDistribution()
    {
        BoltzmannDistribution distribution = new BoltzmannDistribution(energies, a);
        DistributionGraph.Instance.SetGraph(distribution.GetProbabilities());
    }

    private void Update()
    {
        if (_aLast != a || _energiesLast != energies)
        {
            CalculateDistribution();
        }
    }

    private void LateUpdate()
    {
        _aLast = a;
        _energiesLast = energies;
    }
}