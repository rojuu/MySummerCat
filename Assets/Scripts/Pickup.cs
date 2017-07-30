using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public ParticleSystem coinParticle;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            Instantiate(coinParticle, transform.position, transform.rotation);
            GameManager.Score++;
            Destroy(gameObject);
        }
    }
}
