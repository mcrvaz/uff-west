using UnityEngine;
using System.Collections;

public abstract class TimedTargetController : TargetController {

    protected override void Awake() {
        base.Awake();
    }

    void Start() {
        base.ShowSelf();
        base.TimedHide(base.timeToLive);
    }

    protected abstract IEnumerator TimedAction();
}
