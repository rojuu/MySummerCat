using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager {
    public static Action<int, int> OnScoreChanged;

    public static void TriggerOnScoreChanged(int oldScore, int newScore) {
        if(OnScoreChanged != null) {
            OnScoreChanged(oldScore, newScore);
        }
    }
}
