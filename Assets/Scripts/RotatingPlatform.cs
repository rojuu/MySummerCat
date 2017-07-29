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
    public float angle;
    float _angle;
    public float speed;

    void Start()
    {
        center = transform.position;
        _angle = angle * Mathf.Deg2Rad;
    }

    void Update()
    {
        //point on a circle where center is (x0,y0)
        //(x0 + r cos theta, y0 + r sin theta)

        _angle += speed * Time.deltaTime;

        transform.position =
            new Vector3(
                center.x + radius * Mathf.Cos(_angle),
                center.y + radius * Mathf.Sin(_angle)
        );

        _angle = _angle % 360;
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
