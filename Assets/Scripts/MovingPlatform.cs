using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovingPlatform : MonoBehaviour
{

    public Vector3 targetPosition;
    private Vector3 startPosition;
    
    public float lerpTime = 3f;
    float currentLerpTime = 0;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = transform.position + targetPosition;
    }

    private void Update()
    {
        currentLerpTime += Time.deltaTime;
        float t = currentLerpTime / lerpTime;
        t = t * t * (3f - 2f * t);
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        if (Util.VecAlmostEqual(transform.position, targetPosition, 0.001f))
        {
            Vector3 temp = targetPosition;
            targetPosition = startPosition;
            startPosition = temp;
            currentLerpTime = 0;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;

        Handles.DrawSolidRectangleWithOutline(
            new Rect(
                transform.position.x + (targetPosition.x - transform.localScale.x / 2),
                transform.position.y + (targetPosition.y - transform.localScale.y / 2),
                transform.localScale.x,
                transform.localScale.y
            ),
            Color.yellow, Color.red
        );
        Handles.color = Color.yellow;
        Handles.DrawLine(transform.position, transform.position + targetPosition);
    }
#endif
}
