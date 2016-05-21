using UnityEngine;
using System.Collections;

public class DuelController : MonoBehaviour {

    public float timeLimit;

    void Awake() {
        if (timeLimit == 0) {
            FindObjectOfType<DuelTimeController>().gameObject.SetActive(false);
        }
    }

}
