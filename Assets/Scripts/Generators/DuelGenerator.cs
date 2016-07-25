using UnityEngine;
using System.Collections.Generic;

public class DuelGenerator : Generator<Duel> {

    private float newTimeLimit, newTargetMinTime, newTargetMaxTime, newEvadeMinTime, newEvadeMaxTime, newPowerupMinTime, newPowerupMaxTime;
    private List<string> backgrounds = new List<string>(new string[] {
        BackgroundConstants.MAP_0,BackgroundConstants.MAP_1,
        BackgroundConstants.MAP_2,BackgroundConstants.MAP_3,
        BackgroundConstants.MAP_4, BackgroundConstants.MAP_5,
        BackgroundConstants.MAP_6, BackgroundConstants.MAP_7,
        BackgroundConstants.MAP_8, BackgroundConstants.MAP_9,
        BackgroundConstants.MAP_10
    });

    public DuelGenerator(
        float newTimeLimit,
        float newTargetMinTime, float newTargetMaxTime,
        float newEvadeMinTime, float newEvadeMaxTime,
        float newPowerupMinTime, float newPowerupMaxTime) {

        this.newTimeLimit = newTimeLimit;
        this.newTargetMinTime = newTargetMinTime;
        this.newTargetMaxTime = newTargetMaxTime;
        this.newEvadeMinTime = newEvadeMinTime;
        this.newEvadeMaxTime = newEvadeMaxTime;
        this.newPowerupMinTime = newPowerupMinTime;
        this.newPowerupMaxTime = newPowerupMaxTime;
    }

    public Duel Reset() {
        return Generate();
    }

    public Duel Generate() {
        return new Duel(
            timeLimit: this.newTimeLimit,
            background: backgrounds[Random.Range(0, backgrounds.Count)],
            targetMinTime: this.newTargetMinTime,
            targetMaxTime: this.newTargetMaxTime,
            evadeMinTime: this.newEvadeMinTime,
            evadeMaxTime: this.newEvadeMaxTime,
            powerupMinTime: this.newPowerupMinTime,
            powerupMaxTime: this.newPowerupMaxTime
        );
    }
}
