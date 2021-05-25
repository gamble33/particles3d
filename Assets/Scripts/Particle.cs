using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Particle : MonoBehaviour
{
    /**
     * TODO: Make particle a prefab.
     * TODO: add a unique ID to each particle (for collisionHandler naming system)
     */
    [Header("Variables")]
    public GameObject nature; // The nature of the particle is the type of particle that it is (e.g. A / B)

    public Vector3 startVelocity;

    [Header("References")] public Rigidbody rb;
    
    /// <summary>
    /// Kinetic Energy [JOULES] (0.5 * mass * (velocity * velocity)
    /// </summary>
    public float KineticEnergy { get; set; }
    public float PotentialEnergy { get; set; }

    private CollisionHandler _collisionHandler;
    private Vector3 _lastVelocity;
    [SerializeField] private string particleID;

    public void SetMyVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public string GetParticleID()
    {
        return particleID;
    }

    /// <summary>
    /// This method is called when a new instance of a particle is Instantiated();
    /// an ID made by the ParticleIDModel is passed in, to keep a unique id for each particle.
    /// The collisionHandler is passed in so the particles can talk to it while in the simulation.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="_collisionHandler"></param>
    public void SetupParticle(String id, CollisionHandler _collisionHandler)
    {
        this.SetParticleID(id);
        this._collisionHandler = _collisionHandler;
        Debug.Log(particleID);
        Debug.Log(this._collisionHandler);
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        rb.velocity = direction;
        Debug.Log(particleID);
        rb.mass = 2f;
    }


    private void LateUpdate()
    {
        _lastVelocity = rb.velocity;
    }

    private void Update()
    {
        if(!rb.velocity.Equals(_lastVelocity)) CalculateKineticEnergy();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("particle"))
        {
            SetParticleCollisionVelocity(other);
        }
        else if (other.gameObject.CompareTag("wall"))
        {
            rb.velocity = GetWallCollisionVelocity(other, _lastVelocity);
        }
    }
    
    /// <summary>
    /// Calculates and sets kinetic energy of the particle based on velocity
    /// </summary>
    private void CalculateKineticEnergy()
    {
        // KE = 1/2 * m * (v * v)
        this.KineticEnergy = (float) (0.5 * rb.mass * Math.Pow(rb.velocity.magnitude, 2));
    }


    /// <summary>
    /// This function calculates the resulting velocity of a particle's collision with a wall.
    /// Using the formula −(2(n · v) n − v)
    /// n being the normalized vector3 of the wall. v being the last velocity that this particle had before a collision.
    /// </summary>
    /// <param name="wall"></param>
    /// <param name="velocity"></param>
    /// <returns></returns>
    private Vector3 GetWallCollisionVelocity(Collision wall, Vector3 velocity)
    {
        Vector3 wallNormalizedVector = wall.contacts[0].normal;

        Vector3 finalVelocity = velocity - 2 * (Vector3.Dot(velocity, wallNormalizedVector)) * wallNormalizedVector;

        return finalVelocity;
    }

    /// <summary>
    /// This function takes the collidedParticle, and gets it's 'Particle' script component.
    /// It calls the collisionHandler:
    ///     with this particle (script), the collidedParticle's (script) component, this particle's lastVelocity, and it's mass which is constant. 
    /// </summary>
    /// <param name="collidedParticle"></param>
    private void SetParticleCollisionVelocity(Collision collidedParticle)
    {
        Particle collided = collidedParticle.transform.GetComponent<Particle>();
        _collisionHandler.SetParticlesResultantVelocities(this, collided, _lastVelocity, rb.mass);
        return;
    }

    private void SetParticleID(string _id)
    {
        particleID = _id;
    }
}