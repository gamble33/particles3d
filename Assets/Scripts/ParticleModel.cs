using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

public class ParticleModel : MonoBehaviour
{

    public Particle[] particles;
    public Transform particleHolder;
    public ParticleIDModel particleIDModel;

    /// <summary>
    /// This function instantiates a particle by using 3 Random functions to determine
    /// The x, y and z position of the location.
    /// The random range is between edgePoint[0] and edgePoint[1] of the box which are two edge points
    /// that any value in between is inside the contained
    /// TODO: make location determination work without the use of edgepoints.
    /// </summary>
    /// <param name="currentContainer"></param>
    public void InstantiateParticle(GameObject currentContainer)
    {
        Container container = currentContainer.GetComponent<Container>();
        Transform[] edgePoints = container.edgePoints;
        
        Vector3 randomSpawnLocation = new Vector3(
            Random.Range(edgePoints[0].position.x, edgePoints[1].position.x),
            Random.Range(edgePoints[0].position.y, edgePoints[1].position.y),
            Random.Range(edgePoints[0].position.z, edgePoints[1].position.z));
        
        GameObject newParticle = Instantiate(particles[Random.Range(0,particles.Length)].gameObject, randomSpawnLocation, Quaternion.identity, particleHolder);
        newParticle.GetComponent<Particle>().SetupParticle(particleIDModel.GetNewParticleID());
    }
    
    
    /// <summary>
    /// This function will destroy every particle that is outside an area of container marked by 2 edgePoints.
    /// </summary>
    /// <param name="currentContainer"></param>
    public void RemoveNonContainerParticles(GameObject currentContainer)
    {
        Container container = currentContainer.GetComponent<Container>();
        Transform[] edgePoints = container.edgePoints;
        
        foreach(Transform particle in particleHolder)
        {
            if (!particle.CompareTag("particle")) continue;

            if (CheckObjectOustideEdgePoints(particle, edgePoints))
            {
                Destroy(particle.gameObject);
                Debug.Log("Particle Destroyed! reason: Outside of container");
                continue;
            }

        }
    }
    
    /// <summary>
    /// Thsi function uses two conditionals to check if an object is outside of an area marked by 2 edge points.
    /// </summary>
    /// <param name="_object"></param>
    /// <param name="edgePoints"></param>
    /// <returns></returns>
    private bool CheckObjectOustideEdgePoints(Transform _object, Transform[] edgePoints)
    {

        Vector3 minPoint = edgePoints[0].position;
        Vector3 maxPoint = edgePoints[1].position;

        if (_object.position.x < minPoint.x || _object.position.y < minPoint.y ||
            _object.position.z < minPoint.z) return true;
        
        if (_object.position.x > maxPoint.x || _object.position.y > maxPoint.y ||
            _object.position.z > maxPoint.z) return true;
        
        return false;
    }

    private void SetupSceneExistingParticles()
    {
        foreach (Transform particle in particleHolder)
        {
            if (!particle.CompareTag("particle")) continue;
            particle.GetComponent<Particle>().SetupParticle(particleIDModel.GetNewParticleID());
        }
        
    }

    private void Awake()
    {
        SetupSceneExistingParticles();
    }
}
