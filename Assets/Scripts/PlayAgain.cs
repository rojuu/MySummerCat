using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour {
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            if((Input.mousePosition.y / Screen.height) > 0.7f) {
                GameManager.PlayAgain();
            }
        }        
    }
}
