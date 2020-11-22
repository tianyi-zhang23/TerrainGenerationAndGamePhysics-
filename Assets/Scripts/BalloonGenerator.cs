using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script generates balloons once per second.
public class BalloonGenerator : MonoBehaviour
{
    public float xLeft; //specifies the area to spawn balloons
    public float xRight;
    public float y; //specifies the y coordinate to spawn balloons.
    public GameObject balloon;
    // Start is called before the first frame update
    float countdown;
    System.Random r;
    private void Start()
    {
        countdown = 1f;
        r = new System.Random();
    }
    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown<=0)
        {
            GameObject b = Instantiate(balloon, new Vector3((float)(r.NextDouble()*(xRight-xLeft)+xLeft), y, 0), Quaternion.identity);
            GetComponent<CollisionManager>().registerBalloon(b); //let the collision manager know of the presence of this balloon
            countdown = 1;
        }
    }
}
