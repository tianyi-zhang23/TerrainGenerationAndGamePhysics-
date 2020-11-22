using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class KeyboardInputManager : MonoBehaviour
{
    public Vector3 gravity;
    public GameObject projectile;
    public GameObject cannonLeft;
    public GameObject cannonRight;
    // Start is called before the first frame update
    private GameObject currentCannon;

    void Start()
    {
        //set the initial cannon to be controled to be the left one. THe color of the current cannon becomes red.
        currentCannon = cannonLeft;
        setCannonColor();

    }
    void Update()
    {
        //if user pressed Tab, switch current cannon
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            currentCannon = currentCannon == cannonLeft ? cannonRight : cannonLeft;
        }
        setCannonColor();

        //if user presses space, fire cannon
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fireCannon();
        }

        //the following keys can be held down to adjust rotation/velocity continuously
        if(Input.GetKey(KeyCode.UpArrow))
        {
            elevateBarrel();
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            descendArrow();
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            descreaseVelocity();
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            increaseVelocity();
        }
    }
    //increase the muzzle speed of the current cannon.
    void increaseVelocity()
    {
        currentCannon.GetComponent<FireProjectile>().changeProjSpeed(0.2f);
    }
    void descreaseVelocity()
    {
        currentCannon.GetComponent<FireProjectile>().changeProjSpeed(-0.2f);
    }
    void descendArrow()
    {
        currentCannon.transform.Rotate(0f, 0f, -1f);
        float currentAngle = currentCannon.transform.rotation.eulerAngles.z;
        if (currentAngle<=270f)
        {
            Vector3 prev = currentCannon.transform.eulerAngles;
            currentCannon.transform.eulerAngles = new Vector3(prev.x, prev.y, 270f);
        }
    }

    void elevateBarrel()
    {
        currentCannon.transform.Rotate(0f, 0f, 1f);
        float currentAngle = currentCannon.transform.rotation.eulerAngles.z;
        if (currentAngle > 0f && currentAngle <270f)
        {
            Vector3 prev = currentCannon.transform.eulerAngles;
            currentCannon.transform.eulerAngles = new Vector3(prev.x, prev.y, 0f);
        }
    }

    float toRad(float d)
    {
        return (float)Math.PI * d / 180f;
    }
    void fireCannon()
    {
        GameObject activeProjectile = Instantiate(projectile, currentCannon.transform.position,currentCannon.transform.rotation);//spawn the projectile at the position of the cannon.
        float angle = currentCannon.transform.rotation.eulerAngles.z;
        Vector3 unitVelocity = new Vector3((float)Math.Abs(Math.Cos(toRad(angle-90f))), (float)Math.Abs(Math.Sin(toRad(angle-90f))), 0); //calculatt the direction at which the projectile should travel
        if(currentCannon.GetComponent<FireProjectile>().position.Equals("right"))
        {
            unitVelocity.x = -unitVelocity.x;  //the cannon on the right side should fire projectiles in the opposite x direction.
        }
        activeProjectile.GetComponent<VerletIntegration>().initVelocity = unitVelocity * currentCannon.GetComponent<FireProjectile>().projectileSpeed;
        activeProjectile.GetComponent<VerletIntegration>().acceleration = gravity;
    }
    void setCannonColor()
    {
        currentCannon.GetComponent<SpriteShapeRenderer>().color = Color.red;
        if(object.ReferenceEquals(currentCannon,cannonLeft))
        {
            cannonRight.GetComponent<SpriteShapeRenderer>().color = Color.white;
        }
        else
        {
            cannonLeft.GetComponent<SpriteShapeRenderer>().color = Color.white;
        }
    }
}
