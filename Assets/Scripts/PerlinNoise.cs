using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public delegate float InterpolateFunction(float x);
public class PerlinNoise
{
    public static InterpolateFunction fifthOrder = (x) => (float)(6 * Math.Pow(x, 5) - 15 * Math.Pow(x, 4) + 10 * Math.Pow(x, 3));
    static System.Random random = new System.Random();

    //make an array of Vector3 from x=start to x=end at an increment of 1/1000. The y and z values of all the Vector3s are 0.
    public static Vector3[] makeEmpty(float start, float end)
    {
        Vector3[] empty = new Vector3[(int)((end - start) * 1000)];
        for (int i = 0; i < empty.Length; i++)
        {
            empty[i] = new Vector3((i / 1000f) + start, 0, 0);
        }
        return empty;
    }

    //given a function f: float -> float, this method computes fills in all the y values of each Vector3 in the vectors array by doing y=f(x)
    public static Vector3[] fillYValues(Vector3[] vectors, InterpolateFunction func)
    {
        for (int i = 0; i < vectors.Length; i++)
        {
            vectors[i].y = func(vectors[i].x);
        }
        return vectors;
    }
    //Given a start point, an end point, and a function for interpolation, returns a curve that starts from the start point and ends at the end point
    public static Vector3[] interpolate(Vector3 start, Vector3 end, InterpolateFunction func)
    {
        InterpolateFunction adjusted = (x) => (end.y - start.y) * func((x - start.x) / (end.x - start.x)) + start.y;
        return fillYValues(makeEmpty(start.x, end.x), adjusted);
    }
    /*this function generates an array of Vector3 representing a line of 1d perlin noise.
     * It takes:
     * start: leftmost x coordinate for the perlin noise 
     * freq: distance in x value between two random points
     * max: maximum y value. The min value is always 0.
     * numOfSegs: number of segments. The total length of line is freq*numOfSegs
     * func: a function func: float -> float used for interpolation
     */
    public static Vector3[] perlinNoise(float start, float freq, float max, int numOfSegs, InterpolateFunction func)
    {
        List<Vector3> pointsOfLine = new List<Vector3>();
        Vector3 segStart = new Vector3(start, (float)random.NextDouble() * max, 0);
        for (int i = 0; i < numOfSegs; i++)
        {
            Vector3 segEnd = new Vector3(start + (i + 1) * freq, (float)random.NextDouble() * max, 0);
            Vector3[] segmentPoints = interpolate(segStart, segEnd, func);
            pointsOfLine.AddRange(segmentPoints);
            segStart = segEnd;
        }
        return pointsOfLine.ToArray();
    }
    //same as the perlin noise function, but with initial point and final point (0,0).
    public static Vector3[] perlinNoiseSmoothStartEnd(float start, float freq, float max, int numOfSegs, InterpolateFunction func)
    {
        List<Vector3> pointsOfLine = new List<Vector3>();
        Vector3 segStart = new Vector3(start, 0f, 0f);
        Vector3 segEnd;
        Vector3[] segmentPoints;
        for (int i = 0; i < numOfSegs - 1; i++)
        {
            segEnd = new Vector3(start + (i + 1) * freq, (float)random.NextDouble() * max, 0);
            segmentPoints = interpolate(segStart, segEnd, func);
            pointsOfLine.AddRange(segmentPoints);
            segStart = segEnd;
        }
        segEnd = new Vector3(start + numOfSegs * freq, 0, 0);
        segmentPoints = interpolate(segStart, segEnd, func);
        pointsOfLine.AddRange(segmentPoints);
        return pointsOfLine.ToArray();
    }

    //add the Y values of vectors in two Vector3 arrays element-wise and returns the array of the sums. The x and z values will be identical to the first argument array.
    //the sum array will take the size of the first argument array.
    //if the second array is smaller than the first one, then 
    public static Vector3[] vec3ArrayYSum(Vector3[] a, params Vector3[][] bs)
    {
        Vector3[] sum = new Vector3[a.Length];
        try
        {
            for (int i = 0; i < sum.Length; i++)
            {
                sum[i] = new Vector3(a[i].x, a[i].y, a[i].z);
                for (int j = 0; j < bs.Length; j++)
                {
                    sum[i].y += bs[j][i].y;
                }
            }
        }
        catch (System.Exception e) { }

        return sum;
    }

    public static Vector3[] vec3ArrayConcat(params Vector3[][] vs)
    {
        List<Vector3> pointsOfLine = new List<Vector3>();
        for (int i = 0; i < vs.Length; i++)
        {
            pointsOfLine.AddRange(vs[i]);
        }
        return pointsOfLine.ToArray();
    }
}
