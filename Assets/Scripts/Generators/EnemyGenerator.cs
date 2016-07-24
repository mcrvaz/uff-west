﻿using UnityEngine;
using System.Collections.Generic;

public class EnemyGenerator : Generator<Enemy> {

    private Enemy lastEnemy;
    private Enemy defaultEnemy;
    private float healthAdd, damageAdd, minTimeToClickAdd, maxTimeToClickAdd;
    private List<string> prefabs = new List<string>(new string[] {
        CharacterConstants.ENEMY, CharacterConstants.BADDEAD,
        CharacterConstants.OLD_GUY, CharacterConstants.PADRE,
        CharacterConstants.MASKED_BANDIT, CharacterConstants.BOSS
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

    public Enemy Reset() {
        return lastEnemy = defaultEnemy;
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

    public Enemy Generate(string facePrefab) {
        float newDamage = lastEnemy.damage + damageAdd;
        float newMinTimeToClick = lastEnemy.minTimeToClick - minTimeToClickAdd;
        float newMaxTimeToClick = lastEnemy.maxTimeToClick - maxTimeToClickAdd;
        float newHealth = lastEnemy.health + healthAdd;

        lastEnemy = new Enemy(
            characterName: "Dummy",
            damage: newDamage,
            minTimeToClick: newMinTimeToClick,
            maxTimeToClick: newMaxTimeToClick,
            prefab: GetEnemyPrefab(facePrefab),
            health: newHealth
        );
        return lastEnemy;
    }

    private string GetEnemyPrefab(string facePrefab) {
        switch (facePrefab) {
            case ContractConstants.BOSS_FACE:
                return CharacterConstants.BOSS;
            case ContractConstants.BAD_DEAD_FACE:
                return CharacterConstants.BADDEAD;
            case ContractConstants.ENEMY_FACE:
                return CharacterConstants.ENEMY;
            case ContractConstants.MASKED_BANDIT_FACE:
                return CharacterConstants.MASKED_BANDIT;
            case ContractConstants.OLD_GUY_FACE:
                return CharacterConstants.OLD_GUY;
            case ContractConstants.PADRE_FACE:
                return CharacterConstants.PADRE;
            default:
                return null;
        }
    }
}
