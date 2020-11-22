using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is for the balloons with string to destroy itself after it 
public class BaloonSelfDestroy : MonoBehaviour
{
    public float leftLimit;
    public float rightLimit;
    public float upLimit;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 location = transform.GetChild(0).GetChild(0).position; //this looks at the location of one of the points of the balloon.
        //the reason to do this is that the BalloonWithString game object does not change position. Only the children of its children (the points) do.
        if (location.x < leftLimit || location.x > rightLimit || location.y > upLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
