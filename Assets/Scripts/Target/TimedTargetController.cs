using UnityEngine;
using System.Collections;

public abstract class TimedTargetController : TargetController {

    protected override void Awake() {
        base.Awake();
    }

    void Start() {
        base.ShowSelf();

        StartCoroutine(base.TimedHide(base.timeToLive));
    }

    protected new void DestroySelf() {
        base.DestroySelf();
    }

    protected abstract IEnumerator TimedAction();
}
