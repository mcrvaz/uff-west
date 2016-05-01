using UnityEngine;
using System.Collections;

public class Timer {

    public float timeLimit { get; set; }
    float currentTime = 0;
    bool paused;

    public Timer() {

    }

    public Timer(float timeLimit) {
        this.timeLimit = timeLimit;
    }
    
    public float Run(bool loop = true) {
        if (!paused) {
            if (currentTime > 0) {
                currentTime -= Time.deltaTime;
            } else if (loop) {
                ResetTimer();
            }
        }
        return currentTime;
    }

    public void PauseTimer(bool paused){
        this.paused = paused;
    }

    public void ResetTimer() {
        currentTime = timeLimit;
    }
}
