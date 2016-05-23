using UnityEngine;
using System.Collections;

public class BulletTimePowerup : TargetController {
    [Tooltip("New time scale value. For instance, 0.5 slows time by half.")]
    public float slowFactor;
    [Tooltip("Duration in seconds before ending the powerup effect.")]
    public float duration;

    void Start() {
        base.TimedHide(base.timeToLive);
    }

    void SetTimeScale() {
        Time.timeScale = slowFactor;
    }

    void ResetTimeScale() {
        Time.timeScale = 1;
    }

    //should rename
    IEnumerator BulletTimeAction() {
        SetTimeScale();
        base.HideSelf();
        yield return new WaitForSeconds(duration * slowFactor);
        ResetTimeScale();
        Destroy(gameObject);
    }

    void OnMouseDown() {
        //TO DO
        StartCoroutine("BulletTimeAction");
        duelController.RegisterPlayerBulletTime();
    }

    public new void OnEnemyMouseDown() {
        //TO DO
        duelController.RegisterEnemyBulletTime();
        DestroySelf();
    }

}
