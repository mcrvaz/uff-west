﻿using UnityEngine;
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

    void Start() {
        gameController = GameController.Instance;
        victoryText.gameObject.SetActive(gameController.victory);
        defeatText.gameObject.SetActive(!gameController.victory);
        SetStats();
        gameController.SaveGame();
    }

    public void Continue() {
        if (gameController.victory) {
            if (gameController.lastDuel) {
                EndGame(); //player won and it's the last duel, should finish the game.
            } else {
                NextContract(); //player won and there is still another duel ahead.
            }
        } else {
            if (gameController.isDeathDuel) {
                GameOver(); //player lost, and lost a death duel, should be game over.
            } else {
                DeathDuel(); //player lost a common duel, should start a death duel.
            }
        }
    }

    private void EndGame() {
        gameController.NewGame();
        menu.EndGame();
		gameController.lastDuel = false;
    }

    private void NextContract() {
        menu.Contract();
    }

    private void DeathDuel() {
        gameController.isDeathDuel = true;
        menu.PreDeathDuel(); //skips the contract
    }

    private void GameOver() {
        gameController.isDeathDuel = false;
        menu.GameOver();
    }

    private void SetStats() {
        stats = gameController.stats;

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
