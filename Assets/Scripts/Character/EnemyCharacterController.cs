using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyCharacterController : DuelCharacterController {

    public float minTimeToClick, maxTimeToClick;
    [HideInInspector]
    public TargetController selectedTarget { get; set; }

    void Awake() {
        healthBar = GameObject.FindGameObjectWithTag("EnemyHealthBar").GetComponent<Image>();
        animator = GetComponentInChildren<Animator>();
    }

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

    private void Click() {
        if (selectedTarget != null) {
            selectedTarget.OnEnemyMouseDown();
        }
    }

    private float GetTimeToClick() {
        return Random.Range(minTimeToClick, maxTimeToClick);
    }

    public TargetController SelectTarget() {
        var targets = GetTargets();
        if (targets.Length > 0) {
            selectedTarget = targets[Random.Range(0, targets.Length)];
        }
        return selectedTarget;
    }
}
