using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerModel : MonoBehaviour
{

    public GameObject[] containers;
    
    private int currentContainerIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetCurrentContainer()
    {
        return containers[currentContainerIndex];
    }

    
    public void NextContainer()
    {
        if (currentContainerIndex + 1 >= containers.Length)
        {
            containers[currentContainerIndex].SetActive(false);
            currentContainerIndex = 0;
            containers[currentContainerIndex].SetActive(true);
        }
        else
        {
            containers[currentContainerIndex].SetActive(false);
            currentContainerIndex++;
            containers[currentContainerIndex].SetActive(true); 
        }
        
    }
    
}
    