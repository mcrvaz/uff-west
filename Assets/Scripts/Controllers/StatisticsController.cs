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
    public float timeBetweenShots { get; set; } //not captured yet

    public float playerShotsPerSecond { get; private set; } //not captured yet
    private List<DateTime> playerShotTimes;
    public float enemyShotsPerSecond { get; private set; } //not captured yet
    private List<DateTime> enemyShotTimes;
    public int playerShots { get; private set; }
    public int enemyShots { get; private set; }
    private int playerShootingTime;

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

    private int CalculatePlayerShootingTime() {
        var endIndex = playerShotTimes.Count - 1;
        var initialDate = playerShotTimes[0].Millisecond + playerShotTimes[0].Second * 1000 + playerShotTimes[0].Minute * 60000;
        var lastDate = playerShotTimes[endIndex].Millisecond + playerShotTimes[endIndex].Second * 1000 + playerShotTimes[endIndex].Minute * 60000;
        playerShootingTime = lastDate - initialDate;
        return playerShootingTime;
    }

    private float CalculatePlayerTimeBetweenShots() {
        return 1000 / playerShotsPerSecond;
    }

    private float CalculatePlayerShotsPerSecond() {
        playerShotsPerSecond = CalculatePlayerShots() / (CalculatePlayerShootingTime() / 1000f);
        return playerShotsPerSecond;
    }

    private float CalculateEnemyShotsPerSecond() {
        return CalculateEnemyShots() / timeElapsed; //wrong
    }

    public void SaveToXML() {
        //TO DO
    }
}
