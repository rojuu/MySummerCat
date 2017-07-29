using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RotatingPlatform : MonoBehaviour
{
    public float radius;
    Vector3 center;
    float angle;
    public float speed;

    public float lerpTime = 3f;
    float currentLerpTime = 0;

    void Start()
    {
        center = transform.position;
    }

    void Update()
    {
        //point on a circle where center is (x0,y0)
        //(x0 + r cos theta, y0 + r sin theta)

        angle += speed * Time.deltaTime;

        transform.position =
            new Vector3(
                center.x + radius * Mathf.Cos(angle),
                center.y + radius * Mathf.Sin(angle)
        );

        angle = angle % 360;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;

        Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    }
#endif
}
