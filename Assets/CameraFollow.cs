using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    float switchPoint = 0.33f;
    float targetPoint = 0.11f;
    Vector3 offset;

    private void Start() {
        offset = Vector3.zero;
        offset.x -= targetPoint;
    }

    private void Update() {
        Vector3 viewPortDistFromCenter = Camera.main.ScreenToViewportPoint(Camera.main.WorldToScreenPoint(target.position));
        viewPortDistFromCenter = viewPortDistFromCenter * 2 - Vector3.one;



        SetPosition(target.position + offset);
    }

    void SetPosition(Vector3 pos) {
        pos.z = -10;
        transform.position = pos;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3((switchPoint + 1) / 2, 0, 10));
        Vector3 pos2 = Camera.main.ViewportToWorldPoint(new Vector3((switchPoint + 1) / 2, 1, 10));

        Gizmos.DrawLine(pos, pos2);


        Gizmos.color = Color.red;
        pos = Camera.main.ViewportToWorldPoint(new Vector3((targetPoint + 1) / 2, 0, 10));
        pos2 = Camera.main.ViewportToWorldPoint(new Vector3((targetPoint + 1) / 2, 1, 10));
        
        Gizmos.DrawLine(pos, pos2);


        Gizmos.color = Color.green;
        pos = Camera.main.ViewportToWorldPoint(new Vector3((-switchPoint + 1) / 2, 0, 10));
        pos2 = Camera.main.ViewportToWorldPoint(new Vector3((-switchPoint + 1) / 2, 1, 10));

        Gizmos.DrawLine(pos, pos2);


        Gizmos.color = Color.red;
        pos = Camera.main.ViewportToWorldPoint(new Vector3((-targetPoint + 1) / 2, 0, 10));
        pos2 = Camera.main.ViewportToWorldPoint(new Vector3((-targetPoint + 1) / 2, 1, 10));

        Gizmos.DrawLine(pos, pos2);
    }
}
