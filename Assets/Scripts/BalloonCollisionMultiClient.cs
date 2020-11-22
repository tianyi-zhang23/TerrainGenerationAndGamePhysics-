using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is used for cannon ball/projectiles.
//it accesses the register of all balloons held by collision manager and sees if there is a collision. It destroys the balloon if there is one.
public class BalloonCollisionMultiClient : MonoBehaviour
{
    GameObject manager;
    private void Start()
    {
        manager = GameObject.Find("Managers");
    }
    // Update is called once per frame
    void Update()
    {
        List<GameObject> allBalloons = manager.GetComponent<CollisionManager>().balloons;
        for (int j = 0; j < allBalloons.Count; j++)
        {
            bool collides = allBalloons[j].transform.GetChild(0).gameObject.GetComponent<BalloonCollisionServer>().hasCollided(transform.position);
            if (collides)
                GameObject.Destroy(allBalloons[j]);
        }

    }
}
