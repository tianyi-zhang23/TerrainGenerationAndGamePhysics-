using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is for the cannonball projectile to destroy itself if it goes off screen left or right.
public class SelfDestroy : MonoBehaviour
{
    public float leftLimit;
    public float rightLimit;
    public float waterLeft;
    public float waterRight;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftLimit || transform.position.x > rightLimit || (transform.position.x > waterLeft && transform.position.x < waterRight && transform.position.y < -4))
        {
            Destroy(this.gameObject);
        }
    }
}
