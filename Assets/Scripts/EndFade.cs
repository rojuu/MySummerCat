using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndFade : MonoBehaviour {
    public float fadeTime = 3;
    float timePassed = 0;
    Image img;

    public GameObject playAgainButton;
    
    void Awake() {
        img = GetComponent<Image>();
        if(img == null) {
            Destroy(gameObject);
        }
        EventManager.OnGameEnded += OnGameEnded;
    }

    void OnGameEnded() {
        StartCoroutine(Fade());
    }

    IEnumerator Fade() {
        while (true) {
            timePassed += Time.deltaTime;
            float t = timePassed / fadeTime;
            img.color = new Color(img.color.r, img.color.g, img.color.b, t);
            if (t >= 1) break;
            yield return null;
        }

        if(playAgainButton != null) {
            playAgainButton.SetActive(true);
        }
    }
}
