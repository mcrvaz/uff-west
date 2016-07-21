using UnityEngine;
using System.Collections.Generic;

public class EnemyGenerator : Generator<Enemy> {

    public Enemy lastEnemy { get; private set; }
    private float healthAdd, damageAdd, minTimeToClickAdd, maxTimeToClickAdd;
    private List<string> prefabs = new List<string>(new string[] {
        CharacterConstants.HELLBOY, CharacterConstants.ENEMY, CharacterConstants.BADDEAD
    });

    public EnemyGenerator(
        Enemy lastEnemy, float healthAdd, float damageAdd,
        float minTimeToClickAdd, float maxTimeToClickAdd
    ) {
        this.lastEnemy = lastEnemy;
        this.healthAdd = healthAdd;
        this.damageAdd = damageAdd;
        this.minTimeToClickAdd = minTimeToClickAdd;
        this.maxTimeToClickAdd = maxTimeToClickAdd;
    }

    public Enemy Generate() {
        float newDamage = lastEnemy.damage + damageAdd;
        float newMinTimeToClick = lastEnemy.minTimeToClick + minTimeToClickAdd;
        float newMaxTimeToClick = lastEnemy.maxTimeToClick + maxTimeToClickAdd;
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
