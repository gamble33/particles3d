using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

public class Particle : MonoBehaviour
{
    /**
     * TODO: Make particle a prefab.
     * TODO: add a unique ID to each particle (for collisionHandler naming system)
     */
    [Header("Variables")] public Vector3 startVelocity;

    public string natureName;

    [Header("References")] public Rigidbody rb;
    public MeshRenderer meshRenderer;

    /// <summary>
    /// Kinetic Energy [JOULES] (0.5 * mass * (velocity * velocity)
    /// </summary>
    public float KineticEnergy { get; set; }

    public float PotentialEnergy { get; set; }

    public ParticleType ParticleType
    {
        get => _particleType;

        set
        {
            _particleType = value;
            InitParticleSettings();
        }
    }

    private ParticleType _particleType;
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
    /// Adds kinetic energy to the particle and changes its velocity
    /// </summary>
    /// <param name="ke">The kinetic energy being added</param>
    public void AddKineticEnergy(float ke)
    {
        KineticEnergy += ke;
        
        // Ratio between the new and old velocity
        float ratio = (float) Math.Sqrt((float) Math.Pow(rb.velocity.magnitude, 2) + 2 * ke / rb.mass) /
                      rb.velocity.magnitude;
        
        rb.velocity *= ratio;
    }

    /// <summary>
    /// This method is called when a new instance of a particle is Instantiated();
    /// an ID made by the ParticleIDModel is passed in, to keep a unique id for each particle.
    /// The collisionHandler is passed in so the particles can talk to it while in the simulation.
    /// </summary>
    /// <param name="id"></param>
    public void SetupParticle(String id)
    {
        this.SetParticleID(id);
    }

    /// <summary>
    /// Calculates and sets kinetic energy of the particle based on velocity
    /// </summary>
    public void CalculateKineticEnergy()
    {
        // KE = 1/2 * m * (v * v)
        this.KineticEnergy = (float) (0.5 * rb.mass * Math.Pow(rb.velocity.magnitude, 2));
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        rb.velocity = direction;
        rb.mass = 2f;

        CalculateKineticEnergy();
    }


    private void LateUpdate()
    {
        _lastVelocity = rb.velocity;
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
        CollisionHandler.Instance.SetParticlesResultantVelocities(this, collided, _lastVelocity, rb.mass);
    }

    private void SetParticleID(string id)
    {
        particleID = id;
    }

    private void InitParticleSettings()
    {
        meshRenderer.material = _particleType.material;
        PotentialEnergy = _particleType.potentialEnergy;
    }
}