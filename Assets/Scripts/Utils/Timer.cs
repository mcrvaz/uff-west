using UnityEngine;

public class Timer {

    public float timeLimit { get; set; }
    public float currentTime { get; set; }
    public bool paused { get; set; }
    public bool loop { get; set; }

    public Timer(float timeLimit, bool loop) {
        this.timeLimit = timeLimit;
        this.currentTime = this.timeLimit;
        this.loop = loop;
    }

    public float Run() {
        if (!paused) {
            if (currentTime > 0) {
                currentTime -= Time.deltaTime;
            } else if (loop) {
                ResetTimer();
            }
        }
        return currentTime;
    }

    public bool Finished() {
        return currentTime <= 0;
    }

    public void PauseTimer(bool paused) {
        this.paused = paused;
    }

    public void ResetTimer() {
        currentTime = timeLimit;
    }
}
