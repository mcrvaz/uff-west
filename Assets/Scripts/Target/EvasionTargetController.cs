using UnityEngine;
using System.Collections;

public class EvasionTargetController : TargetController {

    [Tooltip("Time in seconds that the one who hits this target will be invulnerable.")]
    public float evasionTime;

    protected override void Awake() {
        base.Awake();
    }

    void OnMouseDown() {
        if (duelController.RegisterPlayerEvasion(this)) {
            DestroySelf();
        }
    }

    public override void OnEnemyMouseDown() {
        duelController.RegisterEnemyEvasion(this);
        DestroySelf();
    }

}
