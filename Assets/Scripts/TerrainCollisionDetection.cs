using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollisionDetection : MonoBehaviour
{
    public float restitutionCoef = 0.9f;
    public float leftBoundary; //left and rightmost x-coordinates for which we should detect collision
    public float rightBoundary;
    Vector3[] groundLine;
    Boolean isBelowTerrain = false;
    
    //the script assumes that the terrain and water generation generate a line with a point every 1/1000 in x-coordinate.
    //it is assume that the terrains have already been generated when this script runs. Indeed, this script should only run 
    //when the user launches a projectile by pressing the space bar.
    void Start()
    {
        GameObject ground = GameObject.Find("MountainLine"); //find the two terrain objects
        GameObject water = GameObject.Find("WaterLine");
        //save the Vector3 array that represent the curve of ground and water.
        //terrainLines[0] will be the array for ground, terrainLines[1] for water.
        groundLine = new Vector3[ground.GetComponent<LineRenderer>().positionCount];
        ground.GetComponent<LineRenderer>().GetPositions(groundLine);


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float radius = transform.localScale.x / 2f; //radius of the cannon ball projectile.

        int offset = (int)(-groundLine[0].x * 1000);
        int index = offset + (int)(transform.position.x * 1000);
        if (index > -1 && index < groundLine.Length)
        {
            Vector3 terrainPointBelow = groundLine[index]; //v[index] has the point in the terrain(water,ground) that is right below or above the projectile.
                                                           //detects if the collision happens; i.e. if the projectile intersects the terrain lines.
            if (Vector3.Distance(terrainPointBelow, transform.position) <= radius)
            {
                Debug.Log("collision detected");
                bounce(groundLine[index + 1] - groundLine[index - 1]);
            }

            //if the projectile was previously above a terrain but is now below, then there is a collision.
            else if (transform.position.y < terrainPointBelow.y && isBelowTerrain == false)
            {
                transform.position = new Vector3(transform.position.x, terrainPointBelow.y, transform.position.z); //bring the object up to the y value that coincides with the ground line. It is not exactly where the collision happened but it looks good enough.
                bounce(groundLine[index + 1] - groundLine[index - 1]);
            }
            isBelowTerrain = transform.position.y < terrainPointBelow.y;
        }
    }

    //makes the projectile bounce off the surface it hit.
    //takes argument: a vector3 representing the line tangent to the 
    void bounce(Vector3 tangent)
    {
        //to calculate the new velocity:
        Vector3 unitTangent = tangent.normalized;
        Vector3 currentVelocity = GetComponent<VerletIntegration>().getVelocity();
        Vector3 preservedComponent = Vector3.Dot(currentVelocity, unitTangent) * unitTangent;         //the component of current velocity along the tangent is preserved.

        Vector3 normalComponent = currentVelocity - preservedComponent;         //the component of the original velocity normal to the tangent is just the original velocity minus its preserved component

        Vector3 newVelocity = preservedComponent - normalComponent; //the new velocity is the sum of the tangential component of the original velocity and the negated normal velocity.
        GetComponent<VerletIntegration>().setVelocity(newVelocity);
    }
}

