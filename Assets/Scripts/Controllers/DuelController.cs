using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DuelController : MonoBehaviour {

    public float timeLimit;
    private StatisticsController stats;
    private DuelCharacterController player;
    private EnemyCharacterController enemy;
    private DuelTimeController timer;

    void Awake() {
        ActivateTimer();
        stats = GameObject.FindObjectOfType<StatisticsController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<DuelCharacterController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCharacterController>();
        timer = GameObject.FindObjectOfType<DuelTimeController>();
    }

    void ActivateTimer() {
        if (timeLimit <= 0) {
            timer.gameObject.SetActive(false);
        }
    }

    public void TargetExpired(TargetController target) {
        stats.targetsExpired++; //statistics
        //if the target that the enemy was aiming expired
        //he should pick a new one
        if (target == enemy.selectedTarget) {
            enemy.SelectTarget();
        }
    }

    public void EndDuel() {
        stats.timeElapsed = timer.currentTime; //statistics
        stats.timeRemaining = timeLimit - timer.currentTime; //statistics
        SceneManager.LoadScene(SceneNames.DUEL_STATISTICS);
    }

    private void RegisterShot(DuelCharacterController source, DuelCharacterController destiny) {
        var damage = ApplyDoubleDamage(source);
        if (destiny.TakeDamage(damage) <= 0) {
            EndDuel();
        }
    }

    private float ApplyDoubleDamage(DuelCharacterController source) {
        var damage = source.damage;

        if (source.hasPowerup > 0) {
            damage *= DoubleDamagePowerup.damageFactor;
            source.hasPowerup--;
        }

        return damage;
    }

    private bool CanShoot() {
        return !player.revolver.isReloading;
    }

    public void RegisterEnemyShot() {
        stats.enemyTargetsHit++; //statistics
        RegisterShot(source: enemy, destiny: player);
    }

    public bool RegisterPlayerShot() {
        if (CanShoot()) {
            stats.playerTargetsHit++;
            player.revolver.Fire();
            RegisterShot(source: player, destiny: enemy);
        } else {
            //play empty sound
        }

        return CanShoot();
    }

    public void RegisterEnemyDoubleDamage(DoubleDamagePowerup dd) {
        stats.enemyDoubleDamageHit++; //statistics
        enemy.hasPowerup = dd.numberOfShots;
    }

    public bool RegisterPlayerDoubleDamage(DoubleDamagePowerup dd) {
        if (CanShoot()) {
            stats.playerDoubleDamageHit++; //statistics
            player.revolver.Fire();
            player.hasPowerup = dd.numberOfShots;
        } else {
            //play empty sound
        }
        return CanShoot();
    }

    public void RegisterEnemyBulletTime(BulletTimePowerup bt) {
        stats.enemyBulletTimeHit++; //statistics
        bt.Execute(bt.enemySlowFactor);
    }

    public bool RegisterPlayerBulletTime(BulletTimePowerup bt) {
        if (CanShoot()) {
            stats.playerBulletTimeHit++;
            player.revolver.Fire();
            bt.Execute(bt.playerSlowFactor);
        }
        return CanShoot();
    }

}
