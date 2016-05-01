using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float timeToLive = 5;
    float currentTimeToLive = 0;
    bool paused;
    
    public float Run(bool loop = true) {
        if (!paused) {
            if (currentTimeToLive > 0) {
                currentTimeToLive -= Time.deltaTime;
            } else if (loop) {
                ResetTimer();
            }
        }
        return currentTimeToLive;
    }

    public void PauseTimer(bool paused){
        this.paused = paused;
    }

    public void ResetTimer() {
        currentTimeToLive = timeToLive;
    }
}
