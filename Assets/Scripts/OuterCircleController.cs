using UnityEngine;
using System.Collections;

public class OuterCircleController : MonoBehaviour {

    private float timeToLive;
    private Vector2 originalScale;

    void Awake() {
        originalScale = Vector2.one;
        timeToLive = GetComponentInParent<TargetController>().timeToLive;
    }

    void Start() {
        StartCoroutine("LerpOuterCircle");
    }

    IEnumerator LerpOuterCircle() {
        Vector2 scale = transform.localScale;
        float t = 0f;

        while (t < timeToLive) {
            t += Time.deltaTime;
            transform.localScale = Vector2.Lerp(scale, originalScale, t / timeToLive);
            yield return null;
        }
    }

}
