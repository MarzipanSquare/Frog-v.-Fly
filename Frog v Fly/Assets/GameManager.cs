using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    // how long lilypads stay underwater
    public int lilypadWaitTime = 2;
    
    // is there a fly active on the scene?
    public bool flyActive = true;
    public GameObject flyPrefab; 
    
    // flag to switch to sim or game
    public bool isSim = false;

    public GameObject fly;
    private FlyMovement _flyMovement;

    private void Start()
    {
        Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-5, 5), 0);
        fly = (GameObject) Instantiate(flyPrefab, position, Quaternion.identity);
        _flyMovement = fly.GetComponent<FlyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // if there's no fly on the scene, make a new one
        if (flyActive == false)
        {
            Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-5, 5), 0);
            fly = (GameObject) Instantiate(flyPrefab, position, Quaternion.identity);
            _flyMovement = fly.GetComponent<FlyMovement>();
            
            flyActive = true;
        }
    }
    
    
}
