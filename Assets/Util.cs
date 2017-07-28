using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static bool VecAlmostEqual(Vector3 a, Vector3 b, float accuracy)
    {
        return Vector3.SqrMagnitude(a - b) < accuracy;
    }

    public static bool VecAlmostEqual(Vector2 a, Vector2 b, float accuracy)
    {
        return Vector2.SqrMagnitude(a - b) < accuracy;
    }
}
