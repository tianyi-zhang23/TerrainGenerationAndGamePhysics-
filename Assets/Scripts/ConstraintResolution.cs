using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script should be applied to the points of a balloon.
//Each point in the Balloon has its own Verlet Integration component and thus is subject to force independently.
//This script solves the constraints of the points. It assumes that that the point is contained in a Balloon parent object.
//This script should not be applied to strings. 
public class ConstraintResolution : MonoBehaviour
{
    System.Random r = new System.Random();
    private List<Vector3> constraints; //the constraints are initialized in start. It consists of vectors from the current point to the other points.

    void Start()
    {
        constraints = new List<Vector3>();
        Transform parent = transform.parent.gameObject.transform;
        for(int i=0;i<parent.childCount;i++)
        {
            constraints.Add(parent.GetChild(i).transform.position - transform.position);
        }
        //the orfer of constraint vectors are the same order of the children of the parent.
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform parent = transform.parent.gameObject.transform;
        for (int i=0;i<constraints.Count;i++)
        {
            //if the distance between the actual position of the point concerned and its supposed position specified
            //by the constraint vector is too big (more than 0.02), then rectify it.

            if(Vector3.Distance(parent.GetChild(i).transform.position,transform.position+constraints[i])>0.01)
            {
                //either bring this point to the correct position with respect to the other point, or the other way around.
                //on average, this results in a compromise between the two points. The points will be, on average, in their average positions
                if (r.Next(0, 2) == 0)
                    parent.GetChild(i).transform.position = transform.position + constraints[i];
                else
                    transform.position = parent.GetChild(i).transform.position - constraints[i];
            }
        }
    }
}
