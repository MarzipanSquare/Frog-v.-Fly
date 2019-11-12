using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

enum Direction {Up, Down, Left, Right}

public class FrogMovement : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    private Vector3 currentPad; // the position of the lily pad the frog is currently on

    private Animator anim; // the frogs animator
    private Rigidbody2D rb; // the frogs rigidbody (for physics and collisions)

    // Start is called before the first frame update
    void Start()
    {
        // get the frog's animator and rigidbody
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // every frame check for a key down and move the frog in that direction
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || 
            Input.GetKeyDown(KeyCode.W))
        {
            Move(Direction.Up);
            
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow) || 
                   Input.GetKeyDown(KeyCode.S))
        {
            Move(Direction.Down);
            
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || 
                   Input.GetKeyDown(KeyCode.A))
        {
            Move(Direction.Left);
            
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) || 
                   Input.GetKeyDown(KeyCode.D))
        {
            Move(Direction.Right);
        }
        Vector2 forward = transform.TransformDirection(Vector2.up) * 3;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, forward);
        Debug.DrawRay(transform.position, forward, Color.white); //uncomment this to see the raycast in the scenes
    }

    void Move(Direction dir)
    {
        
        // move the frog in the direction given by the method argument
        switch (dir)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -270));
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;
        }
        
        // generate a raycast of length 3 that looks down the frog's nose
        Vector3 forward = transform.TransformDirection(Vector2.up) * 3;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, forward);
        Debug.DrawRay(transform.position, forward, Color.white); //uncomment this to see the raycast in the scene

        // if the raycast hits a lilypad collider, move the frog to that lilypad
        if (hit.collider.gameObject.CompareTag("LilyPad"))
        {
            Debug.Log(hit.collider.tag);
            transform.position = hit.collider.gameObject.transform.position;
            currentPad = transform.position;
        }
        
        // play the frog's hop animation
        anim.Play("frog_hop");
    }
    
    // if the fly enters the frog's collider, catch the fly
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Fly"))
        {
            // destroy the fly object
            Destroy(col.gameObject);
            gameManager.flyActive = false;
            
            // increment the frog's score
            
        }
    }
}
