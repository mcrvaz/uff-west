using UnityEngine;
using System.Collections;

public class DoubleDamagePowerup : TargetController {
    [Tooltip("Number of shots before this powerup expires.")]
    public int numberOfShots;
    [Tooltip("Damage multiplier.")]
    public int setDamageFactor;
    public static int damageFactor;

    protected override void Awake() {
        base.Awake();
        damageFactor = setDamageFactor;
    }

    void OnMouseDown() {
        if (duelController.RegisterPlayerDoubleDamage(this)) {
            base.DestroySelf();
        }
    }

    public override void OnEnemyMouseDown() {
        duelController.RegisterEnemyDoubleDamage(this);
        base.DestroySelf();
    }

}
