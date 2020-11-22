using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using UnityEngine;
//This script should be attached to a line. The line will become water.
public class WaterGeneration : MonoBehaviour
{
    public GameObject terrain;
    // Start is called before the first frame update
    public void ManagedStart()
    {
        Vector3[] terrainLine = new Vector3[terrain.GetComponent<LineRenderer>().positionCount];
        terrain.GetComponent<LineRenderer>().GetPositions(terrainLine);

        Vector3[] waterLine = PerlinNoise.perlinNoiseSmoothStartEnd(-5, 0.5f, 0.5f, 20, PerlinNoise.fifthOrder);
        Vector3[] waterLineIter2 = PerlinNoise.perlinNoiseSmoothStartEnd(-5, 0.25f, 0.25f, 40, PerlinNoise.fifthOrder);
        Vector3[] waterLineIter3 = PerlinNoise.perlinNoiseSmoothStartEnd(-5, 0.125f, 0.125f, 80, PerlinNoise.fifthOrder);
        waterLine = PerlinNoise.vec3ArrayYSum(waterLine, waterLineIter2,waterLineIter3);
        translateDown(waterLine, 4f);
        int offset = 10000; //the 10000th element of the line array of the terrain is x=-4, which is the 0th element of the array for water line.

        int intersectionIndexLeft=0; //the index at which the water line and terrain line intersect.
        int intersectionIndexRight=0;
        for(int i=0;i<waterLine.Length;i++)
        {
            if(Math.Abs(waterLine[i].y-terrainLine[i+offset].y)<0.1)
            {
                intersectionIndexLeft = i;
                break;
            }
        }

        for(int i=7000;i<waterLine.Length;i++)
        {
            if (Math.Abs(waterLine[i].y - terrainLine[i + offset].y) < 0.1)
            {
                intersectionIndexRight = i;
                break;
            }
        }

        graph(waterLine.ToList().GetRange(intersectionIndexLeft, intersectionIndexRight - intersectionIndexLeft).ToArray());
    }

    void graph(Vector3[] positions)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);

    }

    void translateDown(Vector3[] vectors, float f)
    {
        for(int i=0;i<vectors.Length;i++)
        {
            vectors[i].y -= f;
        }
    }


}
