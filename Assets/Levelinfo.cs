using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level")]
public class LevelInfo : ScriptableObject
{
    public int cluster_count = 4;
    public float small_rate;
    public float large_rate;
    public float fast_rate;

    public float[] wave_timings;
}
