using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    Text scoreText;
    void Awake() {
        scoreText = GetComponent<Text>();
        if(scoreText == null) {
            Destroy(gameObject);
        }

        EventManager.OnScoreChanged += OnScoreChanged;
    }

    public void OnScoreChanged(int oldValue, int newValue) {
        scoreText.text = "Score: " + newValue;
    }
}
