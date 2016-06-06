using UnityEngine;
using System;
using System.Collections.Generic;

public class StatisticsController : MonoBehaviour {

    private int _playerTargetsHit = 0;
    public int playerTargetsHit {
        get { return _playerTargetsHit; }
        set {
            playerShotTimes.Add(DateTime.Now);
            _playerTargetsHit = value;
        }
    }

    private int _enemyTargetsHit = 0;
    public int enemyTargetsHit {
        get { return _enemyTargetsHit; }
        set {
            enemyShotTimes.Add(DateTime.Now);
            _enemyTargetsHit = value;
        }
    }

    private int _playerDoubleDamageHit = 0;
    public int playerDoubleDamageHit {
        get { return _playerDoubleDamageHit; }
        set {
            playerShotTimes.Add(DateTime.Now);
            _playerDoubleDamageHit = value;
        }
    }

    private int _enemyDoubleDamageHit = 0;
    public int enemyDoubleDamageHit {
        get { return _enemyDoubleDamageHit; }
        set {
            enemyShotTimes.Add(DateTime.Now);
            _enemyDoubleDamageHit = value;
        }
    }

    private int _playerBulletTimeHit = 0;
    public int playerBulletTimeHit {
        get { return _playerBulletTimeHit; }
        set {
            playerShotTimes.Add(DateTime.Now);
            _playerBulletTimeHit = value;
        }
    }

    private int _enemyBulletTimeHit = 0;
    public int enemyBulletTimeHit {
        get { return _enemyBulletTimeHit; }
        set {
            enemyShotTimes.Add(DateTime.Now);
            _enemyBulletTimeHit = value;
        }
    }

    public int shotsMissed { get; set; } //not captured yet
    public int targetsExpired { get; set; }
    public float timeElapsed { get; set; }
    public float timeRemaining { get; set; }
    public float playerTimeBetweenShots { get; private set; }
    public float enemyTimeBetweenShots { get; private set; }

    public float playerShotsPerSecond { get; private set; }
    private List<DateTime> playerShotTimes;
    public float enemyShotsPerSecond { get; private set; }
    private List<DateTime> enemyShotTimes;
    public int playerShots { get; private set; }
    public int enemyShots { get; private set; }
    private int playerShootingTime;
    private int enemyShootingTime;

    void Awake() {
        playerShotTimes = new List<DateTime>();
        enemyShotTimes = new List<DateTime>();
    }

    private int CalculatePlayerShots() {
        playerShots = playerTargetsHit + playerDoubleDamageHit + playerBulletTimeHit + shotsMissed;
        return playerShots;
    }

    private int CalculateEnemyShots() {
        enemyShots = enemyTargetsHit + enemyDoubleDamageHit + enemyBulletTimeHit;
        return enemyShots;
    }

    private int CalculateShootingTime(List<DateTime> shotTimes) {
        var endIndex = shotTimes.Count - 1;
        var firstShot = shotTimes[0].Millisecond + shotTimes[0].Second * 1000 + shotTimes[0].Minute * 60000;
        var lastShot = shotTimes[endIndex].Millisecond + shotTimes[endIndex].Second * 1000 + shotTimes[endIndex].Minute * 60000;
        return lastShot - firstShot;
    }

    private int CalculatePlayerShootingTime() {
        playerShootingTime = CalculateShootingTime(playerShotTimes);
        return playerShootingTime;
    }

    private int CalculateEnemyShootingTime() {
        enemyShootingTime = CalculateShootingTime(enemyShotTimes);
        return enemyShootingTime;
    }

    private float CalculatePlayerTimeBetweenShots() {
        playerTimeBetweenShots = 1000 / playerShotsPerSecond;
        return playerTimeBetweenShots;
    }

    private float CalculateEnemyTimeBetweenShots() {
        enemyTimeBetweenShots = 1000 / enemyShotsPerSecond;
        return enemyTimeBetweenShots;
    }

    private float CalculatePlayerShotsPerSecond() {
        playerShotsPerSecond = CalculatePlayerShots() / (CalculatePlayerShootingTime() / 1000f);
        return playerShotsPerSecond;
    }

    private float CalculateEnemyShotsPerSecond() {
        enemyShotsPerSecond = CalculateEnemyShots() / (CalculateEnemyShootingTime() / 1000f);
        return enemyShotsPerSecond;
    }

}
