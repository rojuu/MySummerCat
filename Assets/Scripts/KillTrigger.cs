using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        print("Hehee");
        if (collision.tag == "Player") {
            SpawnManager.ResetPlayer();
        }
    }
}
