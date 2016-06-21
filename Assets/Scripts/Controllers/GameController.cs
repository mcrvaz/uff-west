using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//should have every attribute needed for generating a new duel or contract
public class GameController : Singleton<GameController> {
    // guarantee this will be always a singleton only - can't use the constructor!
    protected GameController() { }

    private GameObject nextEnemy;
    //everytime the player loses a regular duel, start a death duel
    private GameObject deathEnemy;
    //if the player wins death duel, restart previous duel, else, game over.
    private float timeLimit, deathTimeLimit;
    private float targetMaxTime, targetMinTime;
    private bool isDeathDuel;

    public void EndDuel(DuelCharacterController winnerCharacter) {
        if (winnerCharacter is EnemyCharacterController) {
            if (isDeathDuel) {
                print("Player died. Forever.");
                SceneManager.LoadScene(SceneNames.GAME_OVER);
            } else {
                isDeathDuel = true;
                print("Player lost!");
                SceneManager.LoadScene(SceneNames.CONTRACT);
            }
        } else {
            print("Player won!");
            isDeathDuel = false;
            SceneManager.LoadScene(SceneNames.DUEL_STATISTICS);
        }
    }

}
