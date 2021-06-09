using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltzmannDistribution
{
    private readonly int _energies; // The amount of different energies that are possible outcomes
    private readonly double _a; // The 'temperature' value
    private Dictionary<int, double> _energyCumulativeProbability;
    private double[] _probabilities; // The calculated probabilities of 0..1..2...'_energies'


    /// <summary>
    ///  Creates a Boltzman distribution, who's values can be accessed later.
    /// </summary>
    /// <param name="energies">The amount of different possible derived energies from the distribution curve</param>
    /// <param name="a">a is the value for temperature</param>
    public BoltzmannDistribution(int energies, double a)
    {
        this._energies = energies;
        this._a = a;
        this._energyCumulativeProbability = new Dictionary<int, double>();
        this._probabilities = new double[energies + 1];
            
        // Order matters! Probabilities have to be calculated before cumulative probabilities
        CalculateProbabilities();
        CalculateCumulativeProbabilities();
    }

    public double[] GetProbabilities()
    {
        return _probabilities;
    }
    
    private void CalculateProbabilities()
    {
        for (int i = 0; i < _energies + 1; i++)
        {
            _probabilities[i] = CalculateProbabilityDensity(i);
        }
    }

    private void CalculateCumulativeProbabilities()
    {
        _energyCumulativeProbability.Add(0,0d); // Setting the '0 energy' probability to 0
        
        for (int energy = 1; energy < _energies + 1; energy++)
        {
            _energyCumulativeProbability.Add(energy, _energyCumulativeProbability[energy - 1] + _probabilities[energy]);
        }
    }
    
    /// <summary>
    /// Calculates a probability using the Maxwell-Boltzmann Probability Density Function (PDF)
    /// </summary>
    /// <param name="x">X value (on the graph)</param>
    /// <returns></returns>
    private double CalculateProbabilityDensity(int x)
    {
        // Maxwell-Boltzmann Probability Density Function
        double probability = Math.Sqrt(2 / Math.PI) * ((x * x) * Math.Exp((-x * x) / (2 * (_a * _a)))) /
                             Math.Pow(_a, 3);
        return probability;
    }
}