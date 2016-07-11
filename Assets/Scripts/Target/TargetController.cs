using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

    [Tooltip("Minimum time in seconds before destroying this object.")]
    public float minTimeToLive;
    [Tooltip("Minimum time in seconds before destroying this object.")]
    public float maxTimeToLive;
    [HideInInspector]
    public float timeToLive;

    protected DuelController duelController;

    private bool hit;
    private CircleCollider2D targetCollider;
    private SpriteRenderer[] sprites;
    private Animator animator;
    private bool destroyAnimPlaying;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
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

    protected AnimatorStateInfo PlayDestroyAnimation() {
        if (destroyAnimPlaying) {
            return animator.GetCurrentAnimatorStateInfo(0);
        }
        destroyAnimPlaying = true;
        animator.SetTrigger("destroy");
        return animator.GetCurrentAnimatorStateInfo(0);
    }

    protected void DestroySelf() {
        var anim = PlayDestroyAnimation();
        Destroy(gameObject, anim.length);
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
        if (hit) {
            return; //target was already hit
        }
        hit = true;

        //should be separated
        if (duelController.RegisterPlayerShot()) {
            DestroySelf();
        }
    }

    public virtual void OnEnemyMouseDown() {
        if (hit) {
            return; //target was already hit
        }
        hit = true;

        duelController.RegisterEnemyShot();
        DestroySelf();
    }

    void OnDestroy() {
        if (!hit) {
            //tells the controller that it expired
            duelController.TargetExpired(this);
        }
    }

}
