using UnityEngine;
using System.Collections;

public class PlayerGenerator : Generator<Player> {

    private float newHealth, newDamage;

    public PlayerGenerator(float damage, float health) {
        this.newDamage = damage;
        this.newHealth = health;
    }

    public Player Generate() {
        return new Player(
            characterName: "Dummy",
            damage: newDamage,
            prefab: CharacterConstants.PAPACO,
            health: newHealth
        );
    }
}
