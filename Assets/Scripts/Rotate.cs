using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rotate
{
    public static float angle = Mathf.PI / (float)2;
    public static float[] Matrix = new float[] { Mathf.Cos(angle), -Mathf.Sin(angle), Mathf.Sin(angle), Mathf.Cos(angle)};
}
