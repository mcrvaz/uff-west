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
        if (timeLimit == 0) {
            FindObjectOfType<DuelTimeController>().gameObject.SetActive(false);
        }
    }

    public void TargetExpired(TargetController target) {
        //if the target that the enemy was aiming expired
        //he should pick a new one
        if(target == enemy.selectedTarget) {
            enemy.SelectTarget();
        }
    }

    public void EndDuel() {
        SceneManager.LoadScene(SceneNames.DUEL_STATISTICS);
    }

    private void RegisterShot(DuelCharacterController source, DuelCharacterController destiny) {
        if(destiny.TakeDamage(source.damage) <= 0) {
            EndDuel();
        }
    }

    public void RegisterEnemyShot() {
        RegisterShot(source: enemy, destiny: player);
    }

    public void RegisterPlayerShot() {
        if (!player.revolver.isReloading) {
            player.revolver.Fire();
        }
        RegisterShot(source: player, destiny: enemy);
    }

    public void RegisterEnemyDoubleDamage() {
        //TO DO
    }

    public void RegisterPlayerDoubleDamage() {
        //TO DO
    }

    public void RegisterEnemyBulletTime() {
        //TO DO
    }

    public void RegisterPlayerBulletTime() {
        //TO DO
    }
}
