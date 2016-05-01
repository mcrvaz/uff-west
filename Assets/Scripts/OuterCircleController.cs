using UnityEngine;
using System.Collections;

public class OuterCircleController : MonoBehaviour {

    private float timeToLive = 5f;
    
    void Awake() {
        timeToLive = GetComponentInParent<TargetController>().timeToLive;
    }

    void Start() {
        StartCoroutine("LerpOuterCircle");
    }

    IEnumerator LerpOuterCircle() {
        Vector2 scale = transform.localScale;
        float t = 0f;
        while (t < timeToLive) {
            scale = Vector2.Lerp(scale, new Vector2(1,1), t / timeToLive);
            transform.localScale = scale;
            print(scale);
            t += Time.deltaTime;
            yield return null;
        }
    }

}
