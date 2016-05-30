using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DuelController : MonoBehaviour {

    public float timeLimit;
    private DuelCharacterController player;
    private EnemyCharacterController enemy;

    void Awake() {
        ActivateTimer();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<DuelCharacterController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCharacterController>();
    }

    void ActivateTimer() {
        if (timeLimit <= 0) {
            FindObjectOfType<DuelTimeController>().gameObject.SetActive(false);
        }
    }

    public void TargetExpired(TargetController target) {
        //if the target that the enemy was aiming expired
        //he should pick a new one
        if (target == enemy.selectedTarget) {
            enemy.SelectTarget();
        }
    }

    public void EndDuel() {
        SceneManager.LoadScene(SceneNames.DUEL_STATISTICS);
    }

    private void RegisterShot(DuelCharacterController source, DuelCharacterController destiny) {
        var damage = ApplyDoubleDamage(source);
        if (destiny.TakeDamage(damage) <= 0) {
            EndDuel();
        }
    }

    private void ApplyBulletTime(float slowFactor) {

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
        RegisterShot(source: enemy, destiny: player);
    }

    public bool RegisterPlayerShot() {
        if (CanShoot()) {
            player.revolver.Fire();
            RegisterShot(source: player, destiny: enemy);
        } else {
            //play empty sound
        }

        return CanShoot();
    }

    public void RegisterEnemyDoubleDamage(DoubleDamagePowerup dd) {
        enemy.hasPowerup = dd.numberOfShots;
    }

    public bool RegisterPlayerDoubleDamage(DoubleDamagePowerup dd) {
        if (CanShoot()) {
            player.revolver.Fire();
            player.hasPowerup = dd.numberOfShots;
        } else {
            //play empty sound
        }
        return CanShoot();
    }

    public void RegisterEnemyBulletTime(BulletTimePowerup bt) {
        bt.Execute(bt.enemySlowFactor);
    }

    public bool RegisterPlayerBulletTime(BulletTimePowerup bt) {
        //TO DO
        if (CanShoot()) {
            player.revolver.Fire();
            bt.Execute(bt.playerSlowFactor);
        }
        return CanShoot();
    }
}
