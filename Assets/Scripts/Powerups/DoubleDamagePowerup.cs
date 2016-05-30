using UnityEngine;

public class DoubleDamagePowerup : TargetController {
    [Tooltip("Number of shots before this powerup expires.")]
    public int numberOfShots;
    [Tooltip("Damage multiplier.")]
    public int setDamageFactor;
    public static int damageFactor; //switch to enum?

    protected override void Awake() {
        base.Awake();
        damageFactor = setDamageFactor;
    }

    void OnMouseDown() {
        if (duelController.RegisterPlayerDoubleDamage(this)) {
            DestroySelf();
        }
    }

    public override void OnEnemyMouseDown() {
        duelController.RegisterEnemyDoubleDamage(this);
        DestroySelf();
    }

}
