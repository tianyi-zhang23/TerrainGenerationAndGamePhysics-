using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject textLeftMuzzle;
    public GameObject textRightMuzzle;
    public GameObject textWindForce;

    // Start is called before the first frame update

    public void updateText(float speed,string position)
    {
        if(position.Equals("left"))
            textLeftMuzzle.GetComponent<Text>().text = "Muzzle Speed of Left Cannon " + speed;
        else
            textRightMuzzle.GetComponent<Text>().text = "Muzzle Speed of Right Cannon " + speed;
    }

    public void updateWindForceText(float force)
    {
        if(force>=0)
            textWindForce.GetComponent<Text>().text = "Wind Force: " + force +" towards the right";
        else
            textWindForce.GetComponent<Text>().text = "Wind Force: " + -force + " towards the left";
    }
}
