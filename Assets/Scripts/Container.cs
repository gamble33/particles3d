using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

    //Array of transforms of walls that are not within the outer walls of the container.
    public Transform[] outerWalls;
    
    //Array of two edge points of box (TODO: make spawning work with all sides rather than edge points as some containers may not be cubes)
    public Transform[] edgePoints;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
