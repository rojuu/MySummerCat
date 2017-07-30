using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMarioStyle : MonoBehaviour {

    public Transform target;

    float switchPoint = 0.5f;
    float targetPoint = 0.11f;

    float currentTarget;

    Vector3 cameraPosition;

    Vector3 offset;

    int dir = 1;

    private void Start() {
        offset = Vector3.zero;
        currentTarget = -targetPoint;
        cameraPosition = target.position - offset * dir;
    }

    private void Update() {
        Vector3 viewPortDistFromCenter = Camera.main.ScreenToViewportPoint(Camera.main.WorldToScreenPoint(target.position));
        viewPortDistFromCenter = viewPortDistFromCenter * 2 - Vector3.one;

        if (Mathf.Abs(viewPortDistFromCenter.x) < Mathf.Abs(targetPoint)) {
            offset.x = (Camera.main.ViewportToWorldPoint(new Vector3((currentTarget + 1) / 2, 0, 10)) - transform.position).x;
            cameraPosition = target.position - offset * dir;
        }

        if (Mathf.Abs(viewPortDistFromCenter.x) > Mathf.Abs(switchPoint)) {
            dir *= -1;
            offset.x = (Camera.main.ViewportToWorldPoint(new Vector3((currentTarget + 1) / 2, 0, 10)) - transform.position).x;
            cameraPosition = target.position - offset * dir;
        }

        cameraPosition.y = target.transform.position.y;
        SetPosition(cameraPosition);
        print(Mathf.Abs(viewPortDistFromCenter.x));
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
