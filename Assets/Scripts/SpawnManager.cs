using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    static SpawnManager instance;

    GameObject player;
    Vector3 spawnPoint;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        player = GameObject.FindWithTag("Player");
        if (!player) {
            Destroy(gameObject);
        }

        spawnPoint = player.transform.position;
    }

    public static void ResetPlayer() {
        instance.player.transform.position = instance.spawnPoint;
    }

    public static void SetSpawnPoint(Vector3 newSpawnPoint) {
        instance.spawnPoint = newSpawnPoint;
    }
}
