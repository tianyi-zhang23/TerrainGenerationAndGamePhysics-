using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script provides a public method HasCollided(Vector3), which will return true if the given Vector3 is within the balloon.
//it detects whether the point is on the right side of all of the vectors that enclose the balloon.
//If that is the case, the point must be inside the balloon.
public class BalloonCollisionServer : MonoBehaviour
{
    //the offsets are used to give an offset to the actual locations of the balloon points.
    //to make the detection more sensitive, we want the bounding box to be bigger than the actual balloon.
    //the offset can be used to expand the bounding box and make collision detection more sensitive.
    public Vector3[] offsets;
    // Update is called once per frame
    public bool hasCollided(Vector3 point)
    {
        bool hasCollided = true;
        //as long as the point is on the left side of any one of the line segments, it is not inside the balloo.
        for(int i=0;i<transform.childCount-1;i++)
        {
            Vector3 linePtA = transform.GetChild(i).position + offsets[i];
            Vector3 linePtB = transform.GetChild(i + 1).position + offsets[i];
            if((linePtB.x - linePtA.x)*(point.y - linePtA.y) - (linePtB.y - linePtA.y)*(point.x-linePtA.x)>0) //use cross product to see if the point is on the right side of the line from one point of the balloon to another clockwise
            {
                hasCollided = false;
                break;
            }

        }
        //must also check the final segment that encloses the balloon, that is from the last point to the first point
        Vector3 a = transform.GetChild(transform.childCount-1).position + offsets[transform.childCount-1];
        Vector3 b = transform.GetChild(0).position + offsets[0];
        if ((b.x - a.x) * (point.y - a.y) - (b.y - a.y) * (point.x - a.x) >= 0) //use cross product to see if the point is on the right side of the line from one point of the balloon to another clockwise
        {
            hasCollided = false;
        }

        return hasCollided;
    } 
}
