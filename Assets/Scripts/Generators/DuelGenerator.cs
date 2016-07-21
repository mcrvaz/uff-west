using UnityEngine;
using System.Collections.Generic;

public class DuelGenerator : Generator<Duel> {

    public Duel lastDuel { get; private set; }

    private List<string> backgrounds = new List<string>(new string[] {
        BackgroundConstants.MAP_0,BackgroundConstants.MAP_1,
        BackgroundConstants.MAP_2,BackgroundConstants.MAP_3,
        BackgroundConstants.MAP_5,BackgroundConstants.MAP_6,
        BackgroundConstants.MAP_7, BackgroundConstants.MAP_8,
        BackgroundConstants.MAP_9, BackgroundConstants.MAP_10
    });

    public DuelGenerator() { }

    public Duel Generate() {
        lastDuel = new Duel(
            timeLimit: 30f,
            background: backgrounds[Random.Range(0, backgrounds.Count)],
            targetMinTime: 2f,
            targetMaxTime: 2f,
            evadeMinTime: 2f,
            evadeMaxTime: 2f,
            powerupMinTime: 2f,
            powerupMaxTime: 2f
        );
        return lastDuel;
    }
}
