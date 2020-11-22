using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script should be attached the the Manager game object
public class WindGenerator : MonoBehaviour
{
    private float windMinHeight; //minimum height for a balloon to be subject to wind.
    public GameObject mountain;
    public float maxWindForce; //the maximum velocity of the wind
    private float windForce;
    private System.Random r = new System.Random();
    private float timeRemainingTillNextWindChange;

    //can only be called manually after the mountain line has been initiated.
    public void ManagedStart()
    {
        //find the max y point in the mountain line
        Vector3[] points = new Vector3[mountain.GetComponent<LineRenderer>().positionCount];
        mountain.GetComponent<LineRenderer>().GetPositions(points);

        float maxY = points[0].y;
        foreach(Vector3 v in points)
        {
            if (v.y > maxY)
                maxY = v.y;
        }
        windMinHeight = maxY;
        windForce =(float) (r.NextDouble() * maxWindForce * 2 - maxWindForce); //gives a number in the range between -maxWindVelocity to +maxWindVelocity
        timeRemainingTillNextWindChange = 2f;

        GetComponent<TextManager>().updateWindForceText(windForce);
    }
    // Update is called once per frame
    void Update()
    {
        timeRemainingTillNextWindChange -= Time.deltaTime; //reduce the time elapsed from the last run of this Update()

        if(timeRemainingTillNextWindChange<=0) //if time is up (2 seconds) for the next wind change
        {
            windForce = (float)(r.NextDouble() * maxWindForce * 2 - maxWindForce); //gives a number in the range between -maxWindForce to +maxWindForce
            timeRemainingTillNextWindChange = 2; //reset count down
            GetComponent<TextManager>().updateWindForceText(windForce);
        }
    }

    public float getWindMinHeight()
    {
        return windMinHeight;
    }

    public float getWindForce()
    {
        return windForce;
    }
}
