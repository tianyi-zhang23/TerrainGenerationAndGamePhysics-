using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public GameObject manager;
    public float projectileSpeed = 3;
    public string position;
    // Start is called before the first frame update
    private void Start()
    {
        changeProjSpeed(0);
    }
    // Update is called once per frame

    public void changeProjSpeed(float increment)
    {
        projectileSpeed += increment;
        manager.GetComponent<TextManager>().updateText(projectileSpeed, position);
    }

}
