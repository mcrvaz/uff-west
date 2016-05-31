using UnityEngine;
using System.Collections;

public class EvasionTargetController : TargetController {

    [Tooltip("Time in seconds that the one who hits this target will be invulnerable.")]
    public float evasionTime;

    private DuelCharacterController character;

    protected override void Awake() {
        base.Awake();
    }

    void Start() {
        base.ShowSelf();
        base.TimedHide(base.timeToLive);
    }

    private IEnumerator _SetInvulnerable() {
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
        StartCoroutine("_SetInvulnerable");
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
