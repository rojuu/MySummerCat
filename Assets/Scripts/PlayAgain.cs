using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayAgain : MonoBehaviour {
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            GameManager.PlayAgain();
        }        
    }
}
