using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    public void ManagedStart()
    {
        InterpolateFunction baseTerrainGeneration = (x) =>
        {
            if (x < -7.5f)
                return -2f;
            else if (-7.5f <= x && x < -5.5)
                return 1.5f * (x+1.5f) + 7f;
            else if (-5.5f <= x && x < 0f)
                return -1.5f * (x+1.5f) - 5;
            else if (0f <= x && x < 5.5f)
                return 1.5f * (x-1.5f) - 5f;
            else if (5.5f <= x && x < 7.5f)
                return -1.5f * (x-1.5f) + 7f;
            else return -2f;

        };
        Vector3[] baseTerrain = PerlinNoise.fillYValues(PerlinNoise.makeEmpty(-15, 15), baseTerrainGeneration);


        InterpolateFunction fifthOrder = (x) => (float)(6 * Math.Pow(x, 5) - 15 * Math.Pow(x, 4) + 10 * Math.Pow(x, 3));
        Vector3[] iter2 = PerlinNoise.perlinNoise(-15, 0.5f, 0.25f, 60, fifthOrder);
        Vector3[] iter3 = PerlinNoise.perlinNoise(-15, 0.25f, 0.125f, 120, fifthOrder);

        Vector3[] extraPerlinMountain = PerlinNoise.vec3ArrayConcat(PerlinNoise.makeEmpty(-15, -7.5f), PerlinNoise.perlinNoiseSmoothStartEnd(-7.5f, 0.5f,1, 30, fifthOrder),PerlinNoise.makeEmpty(7.5f,15f));

        Vector3[] terrainLine = PerlinNoise.vec3ArrayYSum(baseTerrain, iter2, iter3, extraPerlinMountain);
        translateDown(terrainLine, 1f);
        graph(terrainLine);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void graph(Vector3[] positions)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);

    }
    void translateDown(Vector3[] vectors, float f)
    {
        for (int i = 0; i < vectors.Length; i++)
        {
            vectors[i].y -= f;
        }
    }


}
