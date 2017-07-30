using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    Vector2 cameraPosition;
    Vector2 targetPosition;
    public float lerpSpeed = 8;
    
    private void FixedUpdate() {
        cameraPosition = transform.position;
        targetPosition = target.position;
        SetPosition(Vector2.Lerp(cameraPosition, targetPosition, lerpSpeed * Time.deltaTime));
    }

    void SetPosition(Vector2 pos) {
        transform.position = new Vector3(pos.x, pos.y, -10);
    }
}
