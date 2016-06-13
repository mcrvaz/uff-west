using UnityEngine;
using System.Collections;

public class DuelController : MonoBehaviour {

    public float timeLimit;
    private StatisticsController stats;
    private DuelCharacterController player;
    private EnemyCharacterController enemy;
    private DuelTimeController timer;
    private DialogController dialog;
    private ObjectSpawner[] spawners;

    void Awake() {
        EvaluateTimer();
        stats = GameObject.FindObjectOfType<StatisticsController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<DuelCharacterController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCharacterController>();
        timer = GameObject.FindObjectOfType<DuelTimeController>();
        dialog = GameObject.FindObjectOfType<DialogController>();
        spawners = GameObject.FindObjectsOfType<ObjectSpawner>();
    }

    void EvaluateTimer() {
        if (timeLimit <= 0) {
            //hide timer
            timer.gameObject.SetActive(false);
        }
    }

    public void StartDuelPhase() {
        timer.StartTimer();
        foreach (var s in spawners) {
            s.enabled = true;
        }
    }

    public void EndDuelPhase() {
        timer.StartTimer();
        foreach (var s in spawners) {
            s.enabled = false;
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
        var winner = player.health > 0 ? player : enemy;
        dialog.NextPhase();
        //GameController.Instance.EndDuel(winner);
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
        enemy.Fire();
        RegisterShot(source: enemy, destiny: player);
    }

    public bool RegisterPlayerShot() {
        var canShoot = CanShoot();
        if (canShoot) {
            stats.playerTargetsHit++;
            player.Fire();
            RegisterShot(source: player, destiny: enemy);
        } else {
            //play empty sound
        }

        return canShoot;
    }

    public void RegisterEnemyDoubleDamage(DoubleDamagePowerup dd) {
        stats.enemyDoubleDamageHit++; //statistics
        enemy.Fire();
        enemy.hasPowerup = dd.numberOfShots;
    }

    public bool RegisterPlayerDoubleDamage(DoubleDamagePowerup dd) {
        var canShoot = CanShoot();
        if (canShoot) {
            stats.playerDoubleDamageHit++; //statistics
            player.Fire();
            player.hasPowerup = dd.numberOfShots;
        } else {
            //play empty sound
        }
        return canShoot;
    }

    public void RegisterEnemyBulletTime(BulletTimePowerup bt) {
        stats.enemyBulletTimeHit++; //statistics
        enemy.Fire();
        bt.SetBulletTime(bt.enemySlowFactor);
    }

    public bool RegisterPlayerBulletTime(BulletTimePowerup bt) {
        var canShoot = CanShoot();
        if (canShoot) {
            stats.playerBulletTimeHit++;
            player.Fire();
            bt.SetBulletTime(bt.playerSlowFactor);
        }
        return canShoot;
    }

    public void RegisterEnemyEvasion(EvasionTargetController ev) {
        ev.SetInvulnerable(enemy);
    }

    public bool RegisterPlayerEvasion(EvasionTargetController ev) {
        ev.SetInvulnerable(player);
        return true; //can always evade, doesnt consume ammo
    }

}
