using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DuelController : MonoBehaviour {

    public float timeLimit;

    void Awake() {
        ActivateTimer();
    }

    void ActivateTimer() {
        if (timeLimit == 0) {
            FindObjectOfType<DuelTimeController>().gameObject.SetActive(false);
        }
    }

    public void EndDuel() {
        SceneManager.LoadScene(SceneNames.DUEL_STATISTICS);
    }

}
