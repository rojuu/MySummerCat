using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public ParticleSystem coinParticle;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            GameObject coin = Instantiate(coinParticle, transform.position, transform.rotation).gameObject;
            SoundPlayer.PlaySound(SoundPlayer.CoinSound, coin.transform.position, coin.transform.parent);

            GameManager.Score++;
            Destroy(gameObject);
        }
    }
}
