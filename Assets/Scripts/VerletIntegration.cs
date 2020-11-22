using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this scripts should be attached to a moving object such as
//a projectile. It models its motion in presence of an acceleration
//using Verlet Integration.
public class VerletIntegration : MonoBehaviour
{
    public Vector3 initVelocity;
    public Vector3 acceleration;
    Vector3 prev;
    // Start is called before the first frame update
    private void Start()
    {
        prev = transform.position - 0.02f * initVelocity; //calculate what would have been the position of this object at the instant right before the launch, given the initial velocity.
    }
    void FixedUpdate()
    {
        //updates position based on new position = 2* current position - previous position + acceleration * delta time
        Vector3 curPos = transform.position;
        //Debug.Log(curPos);
        transform.position = 2 * curPos - prev + acceleration * 0.02f; //Unity's fixed update is called every 0.02 seconds.
        prev = curPos;

    }

    public void setVelocity(Vector3 newVelocity)
    {
        prev = transform.position - 0.02f * newVelocity;
    }

    public Vector3 getVelocity()
    {
        //velocity = displacement over time
        return (1f/0.02f)*(transform.position - prev);
    }

}
