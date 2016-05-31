using UnityEngine;
using System.Collections;

public class EvasionTargetController : TimedTargetController {

    [Tooltip("Time in seconds that the one who hits this target will be invulnerable.")]
    public float evasionTime;

    private DuelCharacterController character;

    protected override void Awake() {
        base.Awake();
    }

    protected override IEnumerator TimedAction() {
        print("before" + character);
        character.invulnerable = true;
        base.HideSelf();
        yield return new WaitForSeconds(evasionTime);
        print("after" + character);
        character.invulnerable = false;
        Destroy(gameObject);
    }

    public void SetInvulnerable(DuelCharacterController character) {
        this.character = character;
        StartCoroutine("TimedAction");
    }

    void OnMouseDown() {
        if (duelController.RegisterPlayerEvasion(this)) {
            base.HideSelf();
        }
    }

    public override void OnEnemyMouseDown() {
        duelController.RegisterEnemyEvasion(this);
        base.HideSelf();
    }

}
