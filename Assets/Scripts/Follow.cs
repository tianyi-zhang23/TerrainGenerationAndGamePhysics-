using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
//this scrit should be applied to the points in the balloon string. It imposes minimal constraints, namely that
//the distance between this point from the point above should never exceed their initial values.
public class Follow : MonoBehaviour
{
    public GameObject balloon;
    public GameObject pointAbove;
    float distanceToPointAbove;
    // Start is called before the first frame update
    void Start()
    {
        distanceToPointAbove = Vector3.Distance(transform.position, pointAbove.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position,pointAbove.transform.position)>distanceToPointAbove)
        {
            Vector3 unitDirection = Vector3.Normalize(transform.position - pointAbove.transform.position); //represents the direction from the point above to the current position of the current point.
            Vector3 newPosition = pointAbove.transform.position + distanceToPointAbove * unitDirection;
            transform.position = newPosition;

            //the current point will be moved closer to the point above, while maintaining their relative direction.
        }
    }

}
