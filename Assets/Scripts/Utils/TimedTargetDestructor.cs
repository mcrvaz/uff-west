using UnityEngine;
using System.Collections;

public class TimedTargetDestructor : MonoBehaviour {

    private float m_TimeOut = 5f;
    [SerializeField]
    private bool m_DetachChildren = false;

    private void Awake() {
        m_TimeOut = GetComponentInParent<TargetController>().timeToLive;
    }

    private void Start() {
        Invoke("DestroyNow", m_TimeOut);
    }

    private void DestroyNow() {
        if (m_DetachChildren) {
            transform.DetachChildren();
        }
        DestroyObject(gameObject);
    }
}
