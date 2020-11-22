using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWind : MonoBehaviour
{
    //attached to a point of a balloon to make it subject to wind
    //because wind applies a constant force, we need to make the value quite small

    // Update is called once per frame
    void Update()
    {
        GameObject manager = GameObject.Find("Managers");
        if(transform.position.y>=manager.GetComponent<WindGenerator>().getWindMinHeight()) //if the object is above the mountain
        {
            //we are directly adding wind force to acceleration, since only balloons are using wind. Force and acceleration only differ by a factor of mass, so I simply assume balloons have unit mass.
            GetComponent<VerletIntegration>().acceleration = GetComponent<VerletIntegration>().acceleration + new Vector3(manager.GetComponent<WindGenerator>().getWindForce(),0,0);

        }
    }
}
