using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int lilypadWaitTime = 2;
    public bool flyActive = true;
    public GameObject flyPrefab; 
    
    public bool isSim = false;

    // Update is called once per frame
    void Update()
    {
        if (flyActive == false)
        {
            Object.Instantiate(flyPrefab);
            flyActive = true;
        }
    }
    
    
}
