using UnityEngine;
using System.Collections.Generic;

public class EnemyGenerator : Generator<Enemy> {

    private Enemy lastEnemy;
    private Enemy defaultEnemy;
    private float healthAdd, damageAdd, minTimeToClickAdd, maxTimeToClickAdd;
    private List<string> prefabs = new List<string>(new string[] {
        CharacterConstants.ENEMY, CharacterConstants.BADDEAD, CharacterConstants.OLD_GUY
    });

    public EnemyGenerator(
        float healthAdd, float damageAdd,
        float minTimeToClickAdd, float maxTimeToClickAdd
    ) {
        this.healthAdd = healthAdd;
        this.damageAdd = damageAdd;
        this.minTimeToClickAdd = minTimeToClickAdd;
        this.maxTimeToClickAdd = maxTimeToClickAdd;
        defaultEnemy = new Enemy(
            characterName: "Default Enemy",
            damage: 5,
            minTimeToClick: 1f,
            maxTimeToClick: 1.1f,
            prefab: prefabs[Random.Range(0, prefabs.Count)],
            health: 100f
        );
        lastEnemy = defaultEnemy;
    }

    public void Reset() {
        lastEnemy = defaultEnemy;
    }

    public Enemy Generate() {
        float newDamage = lastEnemy.damage + damageAdd;
        float newMinTimeToClick = lastEnemy.minTimeToClick - minTimeToClickAdd;
        float newMaxTimeToClick = lastEnemy.maxTimeToClick - maxTimeToClickAdd;
        float newHealth = lastEnemy.health + healthAdd;
        string newPrefab = prefabs[Random.Range(0, prefabs.Count)];

        lastEnemy = new Enemy(
            characterName: "Dummy",
            damage: newDamage,
            minTimeToClick: newMinTimeToClick,
            maxTimeToClick: newMaxTimeToClick,
            prefab: newPrefab,
            health: newHealth
        );
        return lastEnemy;
    }
}
