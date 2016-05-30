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

    void Awake() {
        timeLimit = GameObject.FindObjectOfType<DuelController>().timeLimit;
        visualTimer = GetComponent<Text>();
        timer = new Timer(timeLimit, loop: false);
    }

    void Update() {
        UpdateUIText();

        if (timer.Finished()) {
            finish.Invoke();
        }
    }

    void UpdateUIText() {
        currentTime = timer.Run();
        visualTimer.text = ((int)currentTime).ToString();
    }

}
