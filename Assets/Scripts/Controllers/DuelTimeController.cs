using UnityEngine;
using UnityEngine.UI;

public class DuelTimeController : MonoBehaviour {

    private float timeLimit;
    private Timer timer;
    private Text visualTimer;

    void Awake() {
        timeLimit = GameObject.FindObjectOfType<DuelController>().timeLimit;
        visualTimer = GetComponent<Text>();
        timer = new Timer(timeLimit, loop: false);
    }

    void Update() {
        visualTimer.text = ((int)timer.Run()).ToString();
    }

}
