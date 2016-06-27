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
    private DuelController duelController;

    void Awake() {
        visualTimer = GetComponent<Text>();
        duelController = GameObject.FindObjectOfType<DuelController>();
    }

    void Start() {
        timeLimit = duelController.timeLimit;
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

    public void PauseTimer() {
        isRunning = false;
    }

    void UpdateUIText() {
        currentTime = timer.Run();
        visualTimer.text = ((int)currentTime).ToString();
    }

}
