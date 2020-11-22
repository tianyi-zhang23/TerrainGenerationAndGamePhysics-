using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lets the line attached trace the outline of a balloon.
public class MakeOutline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> points = new List<Vector3>();
        Transform balloonBody = transform.parent.parent.GetChild(0); //the first child of the Balloon parent is the body
        Transform balloonString = transform.parent.parent.GetChild(1); //the second is the string

        for(int i=0;i<balloonBody.childCount;i++) //traces the balloon body
        {
            points.Add(balloonBody.GetChild(i).position);
        }
        points.Add(balloonBody.GetChild(0).position); //needs to wrap around the balloon, so go back to the first point again

        for(int i=0;i<balloonString.childCount;i++)
        {
            points.Add(balloonString.GetChild(i).position);
        }
        GetComponent<LineRenderer>().positionCount = points.Count;
        GetComponent<LineRenderer>().SetPositions(points.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        Start();
    }
}
