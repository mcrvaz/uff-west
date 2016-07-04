using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DuelStatisticsController : MonoBehaviour {

    public Text victoryText, defeatText;
    public MenuSceneController menu;
    private GameController gameController;

    void Awake() {
        gameController = GameController.Instance;
    }

    void Start() {
        victoryText.gameObject.SetActive(gameController.victory);
        defeatText.gameObject.SetActive(!gameController.victory);
    }

    public void Continue() {
        if (gameController.victory) {
            print(gameController.lastDuel);
            if (gameController.lastDuel) {
                EndGame();
            } else {
                NextContract();
            }
        } else {
            if (gameController.isDeathDuel) {
                GameOver();
            } else {
                DeathDuel();
            }
        }
    }

    private void EndGame() {
        menu.EndGame();
    }

    private void NextContract() {
        menu.Contract();
    }

    private void DeathDuel() {
        menu.Duel(); //skips the contract
    }

    private void GameOver() {
        menu.GameOver();
    }

}
