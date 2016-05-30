using UnityEngine;
using System.Collections;

public class BulletTimePowerup : PowerupController {
    [Tooltip("New time scale value for when the player hits this powerup. For instance, 0.5 slows time by half.")]
    public float playerSlowFactor; //switch to enum?
    [Tooltip("New time scale value for when the enemy hits this powerup. For instance, 0.5 slows time by half.")]
    public float enemySlowFactor; //switch to enum?
    [Tooltip("Duration in seconds before ending the powerup effect.")]
    public float duration;

    private float currentSlowFactor;

    protected override void Awake() {
        base.Awake();
    }

    void Start() {
        base.ShowSelf();
        base.TimedHide(base.timeToLive);
    }

    void SetTimeScale() {
        Time.timeScale = currentSlowFactor;
    }

    void ResetTimeScale() {
        Time.timeScale = 1;
    }

    //should rename?
    private IEnumerator BulletTimeAction() {
        SetTimeScale();
        base.HideSelf();
        yield return new WaitForSeconds(duration * currentSlowFactor);
        ResetTimeScale();
        Destroy(gameObject);
    }

    public void Execute(float slowFactor) {
        currentSlowFactor = slowFactor;
        StartCoroutine("BulletTimeAction");
    }

    void OnMouseDown() {
        if (duelController.RegisterPlayerBulletTime(this)) {
            base.HideSelf();
        }
    }

    public override void OnEnemyMouseDown() {
        duelController.RegisterEnemyBulletTime(this);
        base.HideSelf();
    }

}
