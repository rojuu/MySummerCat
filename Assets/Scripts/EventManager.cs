using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager {
    public static Action<int, int> OnScoreChanged;
    public static Action OnGameEnded;
    public static Action PlayAgain;

    public static void TriggerOnScoreChanged(int oldScore, int newScore) {
        if (OnScoreChanged != null) {
            OnScoreChanged(oldScore, newScore);
        }
    }

    public static void TriggerOnGameEnd() {
        if(OnGameEnded != null) {
            OnGameEnded();
            OnGameEnded = null;
            OnScoreChanged = null;
        }
    }

    public static void TriggerPlayAgain() {
        if(PlayAgain != null) {
            PlayAgain();
        }
    }
}
