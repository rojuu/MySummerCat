using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    static SoundPlayer instance;
    private void Awake() {
        instance = this;
    }
    public GameObject soundSourcePrefab;

    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip deathSound;

    public static AudioClip JumpSound { get { return instance.jumpSound; } }
    public static AudioClip CoinSound { get { return instance.coinSound; } }
    public static AudioClip DeathSound { get { return instance.deathSound; } }


    public static void PlaySound(AudioClip sound, Vector2 location, Transform parent = null) {
        instance._PlaySound(sound, location, parent);
    }

    void _PlaySound(AudioClip sound, Vector2 location, Transform parent = null) {
        var source = Instantiate(soundSourcePrefab, location, Quaternion.identity, parent).GetComponent<AudioSource>();
        source.clip = sound;
        source.Play();
    }
}
