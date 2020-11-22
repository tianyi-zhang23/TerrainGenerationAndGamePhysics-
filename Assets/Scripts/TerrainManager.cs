using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    //this script manages the start of the terrain elements (ground, water, wind). Water and wind must start after ground (mountain) has been initiated
    public GameObject terrain;
    public GameObject water;
    // Start is called before the first frame update
    void Start()
    {
        terrain.GetComponent<TerrainGeneration>().ManagedStart();
        water.GetComponent<WaterGeneration>().ManagedStart();
        GetComponent<WindGenerator>().ManagedStart();
    }

}
