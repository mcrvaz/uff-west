using UnityEngine;
using System.Collections;

public class BulletTimePowerup : TimedTargetController {
    [Tooltip("New time scale value for when the player hits this powerup. For instance, 0.5 slows time by half.")]
    public float playerSlowFactor;
    [Tooltip("New time scale value for when the enemy hits this powerup. For instance, 0.5 slows time by half.")]
    public float enemySlowFactor;
    [Tooltip("Duration in seconds before ending the powerup effect.")]
    public float duration;

    private float currentSlowFactor;

    protected override void Awake() {
        base.Awake();
    }

    void SetTimeScale() {
        Time.timeScale = currentSlowFactor;
    }

    void ResetTimeScale() {
        Time.timeScale = 1;
    }

    protected override IEnumerator TimedAction() {
        SetTimeScale();

        yield return StartCoroutine(base.HideSelf());
        yield return new WaitForSeconds(duration * currentSlowFactor);

        ResetTimeScale();
        Destroy(gameObject);
    }

    public void SetBulletTime(float slowFactor) {
        currentSlowFactor = slowFactor;
        StartCoroutine("TimedAction");
    }

    void OnMouseDown() {
        if (duelController.RegisterPlayerBulletTime(this)) {
            StartCoroutine(base.HideSelf());
        }
    }

    public override void OnEnemyMouseDown() {
        duelController.RegisterEnemyBulletTime(this);
        StartCoroutine(base.HideSelf());
    }

}
