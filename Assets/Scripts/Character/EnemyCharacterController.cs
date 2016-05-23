﻿using UnityEngine;
using System.Collections;

public class EnemyCharacterController : DuelCharacterController {

    public float minTimeToClick, maxTimeToClick;
    private TargetController selectedTarget;

    void Start() {
        SelectTarget();
        Invoke("ClickRepeating", GetTimeToClick());
    }

    private void ClickRepeating() {
        Click();
        SelectTarget();
        Invoke("ClickRepeating", GetTimeToClick());
    }

    private TargetController[] GetTargets() {
        return GameObject.FindObjectsOfType<TargetController>();
    }

    private TargetController SelectTarget() {
        var targets = GetTargets();
        if (targets.Length > 0) {
            selectedTarget = targets[Random.Range(0, targets.Length)];
        }
        return selectedTarget;
    }

    private void Click() {
        if (selectedTarget != null) {
            selectedTarget.OnEnemyMouseDown();
            print("CLICKED! " + selectedTarget);
        }
        print("MISSED!");
    }

    private float GetTimeToClick() {
        return Random.Range(minTimeToClick, maxTimeToClick);
    }

}
