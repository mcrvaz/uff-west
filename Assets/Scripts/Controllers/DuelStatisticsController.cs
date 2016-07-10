using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DuelStatisticsController : MonoBehaviour {

    public bool showPlayer, showEnemy, showDuel;
    public Text victoryText, defeatText;
    public Text d_timeElapsed, d_timeRemaining, d_targetsExpired;
    public Text p_targetsHit, p_doubleDamagesHit, p_bulletTimesHit, p_totalShots, p_timeBetweenShots, p_shotsPerSecond;
    public Text e_targetsHit, e_doubleDamagesHit, e_bulletTimesHit, e_totalShots, e_timeBetweenShots, e_shotsPerSecond;
    public MenuSceneController menu;
    private GameController gameController;
    private StatisticsController stats;

    void Awake() {
        gameController = GameController.Instance;
        stats = gameController.stats;
    }

    void Start() {
        victoryText.gameObject.SetActive(gameController.victory);
        defeatText.gameObject.SetActive(!gameController.victory);

        SetStats();
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

    private void SetStats() {
        stats.CalculateStats();
        stats.SaveXML();

        SetPlayerStats();
        SetEnemyStats();
        SetDuelStats();
    }

    private void SetPlayerStats() {
        if (!showPlayer) {
            return;
        }
        p_targetsHit.text = stats.playerTargetsHit.ToString();
        p_doubleDamagesHit.text = stats.playerDoubleDamageHit.ToString();
        p_bulletTimesHit.text = stats.playerBulletTimeHit.ToString();
        p_totalShots.text = stats.playerShots.ToString();
        p_timeBetweenShots.text = stats.playerTimeBetweenShots.ToString();
        p_shotsPerSecond.text = stats.playerShotsPerSecond.ToString();
    }

    private void SetEnemyStats() {
        if (!showEnemy) {
            return;
        }
        e_targetsHit.text = stats.enemyTargetsHit.ToString();
        e_doubleDamagesHit.text = stats.enemyDoubleDamageHit.ToString();
        e_bulletTimesHit.text = stats.enemyBulletTimeHit.ToString();
        e_totalShots.text = stats.enemyShots.ToString();
        e_timeBetweenShots.text = stats.enemyTimeBetweenShots.ToString();
        e_shotsPerSecond.text = stats.enemyShotsPerSecond.ToString();
    }

    private void SetDuelStats() {
        if (!showDuel) {
            return;
        }
        d_timeElapsed.text = stats.timeElapsed.ToString();
        d_timeRemaining.text = stats.timeRemaining.ToString();
        d_targetsExpired.text = stats.targetsExpired.ToString();
    }

}
