using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour {
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            GameManager.PlayAgain();
        }        
    }
}
