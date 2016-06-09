using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DuelTimeController : MonoBehaviour {

    public UnityEvent finish;
    [HideInInspector]
    public float currentTime { get; set; }
    private float timeLimit;
    private Timer timer;
    private Text visualTimer;
    private bool isRunning;

    void Awake() {
        timeLimit = GameObject.FindObjectOfType<DuelController>().timeLimit;
        visualTimer = GetComponent<Text>();
        timer = new Timer(timeLimit, loop: false);
    }

    void Update() {
        if (isRunning) {
            UpdateUIText();
        }

        if (timer.Finished()) {
            finish.Invoke();
        }
    }

    public void StartTimer() {
        isRunning = true;
    }

    void UpdateUIText() {
        currentTime = timer.Run();
        visualTimer.text = ((int)currentTime).ToString();
    }

}
