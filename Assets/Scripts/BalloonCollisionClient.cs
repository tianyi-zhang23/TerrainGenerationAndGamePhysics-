using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is for the points in the balloon strings. If the balloon strings collide with the balloon itself, it will be brought back to its
//previous position before collision

public class BalloonCollisionClient : MonoBehaviour
{
    public GameObject balloon;
    private Vector3 previousPositionBeforeCollision;
    void Start()
    {
        previousPositionBeforeCollision = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //if the point has collided with the balloon, put it back to the position before collision
        if(balloon.GetComponent<BalloonCollisionServer>().hasCollided(transform.position))
        {
            transform.position = previousPositionBeforeCollision;
        }
        else //otherwise update the position before collision.
        {
            previousPositionBeforeCollision = transform.position;
        }

    }


}
