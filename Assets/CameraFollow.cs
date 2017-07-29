using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    private void Update() {
        SetPosition(target.position);
    }

    void SetPosition(Vector3 pos) {
        pos.z = -10;
        transform.position = pos;
    }
}
