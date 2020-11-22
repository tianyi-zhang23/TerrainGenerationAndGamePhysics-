using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    //keeps a register of all the balloons
    public List<GameObject> balloons;

    private void Start()
    {
        balloons = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {

        for (int j = 0; j < balloons.Count; j++)
        {
            if (balloons[j] == null)
            {
                balloons.RemoveAt(j);
                j--;
            }
        }
    }

    public void registerBalloon(GameObject g)
    {
        balloons.Add(g);
    }
}
