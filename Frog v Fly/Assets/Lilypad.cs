using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilypad : MonoBehaviour
{
    [SerializeField]
    private int padID;

    [SerializeField]
    private GameManager manager;

    IEnumerator waitThreeSeconds()
    {
        yield return new WaitForSeconds(manager.lilypadWaitTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Frog"))
        {
            Debug.Log("Frog has entered trigger of " + gameObject.name);
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Frog"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            this.gameObject.SetActive(false);
            waitThreeSeconds();
            this.gameObject.SetActive(true);
        }
    }
}
