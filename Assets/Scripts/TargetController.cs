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

}
