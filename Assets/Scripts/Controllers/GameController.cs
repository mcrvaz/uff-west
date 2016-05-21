using UnityEngine;
using System.Collections;

public class GameController : Singleton<GameController> {
    protected GameController() { } // guarantee this will be always a singleton only - can't use the constructor!

    //Should have every attribute needed for generating a new duel
    public GameObject nextEnemy;
    public float timeLimit;
    public float targetMaxTime, targetMinTime;

}
