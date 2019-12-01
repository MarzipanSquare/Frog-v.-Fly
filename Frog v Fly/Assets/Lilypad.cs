using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.UI;

public class Lilypad : MonoBehaviour
{
    [SerializeField]
    private int padID;

    [SerializeField]
    private SimulationManager manager;
    
    private float _timer;
    // how long lilypads stay underwater
    public int lilypadWaitTime = 2;
    
    private IEnumerator coroutine;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Frog"))
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            if (!GetComponent<SpriteRenderer>().enabled)
            {

                GameObject frog = col.gameObject;
                manager.TakeFrogLife();
                
                // code to flash frog and place it back at start
                frog.transform.position = new Vector3(-4f, 2f, 0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Frog"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(WaitAndFloatUp(lilypadWaitTime));
        }
    }

    private IEnumerator WaitAndFloatUp(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}
