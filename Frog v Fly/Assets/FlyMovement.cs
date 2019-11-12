using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Code found at http://findnerd.com/list/view/Random-movement-of-a-sprite-in-2D/22107/
// Additional code and comments added

public class FlyMovement : MonoBehaviour
{
    public float moveSpeed ;
    public Vector3 currentDirection;
    public float turnSpeed;
    
    private float targetAngle;
    private Vector3 currentPos;
    private bool play=true;
    private Vector3 direction;
    
    void Start()
    {
        currentDirection = Vector3.up;
        InvokeRepeating ("Start1", 0f, 5f);
    }
    void Start1(){
        play = true;
        direction = new Vector3(Random.Range(-3.0f,3.0f),Random.Range(-4.0f,4.0f),0); //random position in x and y
    }
    void Update()
    {
        currentPos = transform.position;//current position of gameObject
        if(play)
        { //calculating direction
            currentDirection = direction - currentPos;  
    
            currentDirection.z = 0;
            currentDirection.Normalize ();
            play = false;
        }    
        Vector3 target = currentDirection * moveSpeed + currentPos;  //calculating target position
        transform.position = Vector3.Lerp (currentPos, target, Time.deltaTime);//movement from current position to target position
        targetAngle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90; //angle of rotation of gameobject
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime); //rotation from current direction to target direction
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("Border"))
        {
            CancelInvoke ();//stop call to start1 method
            direction = new Vector3 (Random.Range (-3.0f, 3.0f), Random.Range (-4.0f, 4.0f), 0); //again provide random position in x and y
            play = true;            
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        InvokeRepeating ("Start1", 2f, 5f);
    }
}

