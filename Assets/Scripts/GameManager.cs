using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    private static int score;
    public static int Score {
        get {
            return score;
        }
        set {
            EventManager.TriggerOnScoreChanged(score, value);
            score = value;
        }
    }

    public static void EndGame() {
        EventManager.TriggerOnGameEnd();
    }

    public static void PlayAgain() {
        EventManager.TriggerPlayAgain();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
