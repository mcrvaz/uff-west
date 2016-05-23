using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

    [Tooltip("Minimum time in seconds before destroying this object.")]
    public float minTimeToLive;
    [Tooltip("Minimum time in seconds before destroying this object.")]
    public float maxTimeToLive;

    [HideInInspector]
    public float timeToLive;

    void Awake() {
        timeToLive = Random.Range(minTimeToLive, maxTimeToLive);
    }

    void Start() {
        TimedDestroy(timeToLive);
    }

    protected void HideSelf() {
        var collider = gameObject.GetComponent<Collider2D>();
        var sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        collider.enabled = false;
        foreach (var sprite in sprites) {
            sprite.enabled = false;
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

    void OnMouseDown() {
        DestroySelf();
    }

}
