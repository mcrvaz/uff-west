using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TargetController : MonoBehaviour {

    [Tooltip("Minimum time in seconds before destroying this object.")]
    public float minTimeToLive;
    [Tooltip("Minimum time in seconds before destroying this object.")]
    public float maxTimeToLive;
    [HideInInspector]
    public float timeToLive;

    protected DuelController duelController;

    private Collider2D container;
    private CircleCollider2D targetCollider;
    private SpriteRenderer[] sprites;

    void Awake() {
        targetCollider = GetComponent<CircleCollider2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        timeToLive = Random.Range(minTimeToLive, maxTimeToLive);
        HideSelf();
        duelController = FindObjectOfType<DuelController>();
    }

    void Start() {
        if (CheckCollision()) {
            //fuck this shit
            DestroySelf();
        } else {
            ShowSelf();
            TimedDestroy(timeToLive);
        }
    }

    protected void HideSelf() {
        targetCollider.enabled = false;
        foreach (var sprite in sprites) {
            sprite.enabled = false;
        }
    }

    protected void ShowSelf() {
        targetCollider.enabled = true;
        foreach (var sprite in sprites) {
            sprite.enabled = true;
        }
    }

    protected void DestroySelf() {
        Destroy(gameObject);
    }

    protected void TimedHide(float time) {
        Invoke("HideSelf", time);
    }

    protected void TimedDestroy(float time) {
        Invoke("DestroySelf", time);
    }

    private bool CheckCollision() {
        var hitCollider = Physics2D.OverlapCircle(
            transform.position,
            targetCollider.radius,
            1 << LayerMask.NameToLayer("Targets")
        );
        return hitCollider != null;
    }

    void OnMouseDown() {
        duelController.RegisterPlayerShot();
        DestroySelf();
    }

    public void OnEnemyMouseDown() {
        duelController.RegisterEnemyShot();
        DestroySelf();
    }

}
