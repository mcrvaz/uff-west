using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class DialogController : MonoBehaviour {

    public UnityEvent startGame;
    public List<SpeechText> beginningDialogs;
    public List<SpeechText> endingDialogs;
    [HideInInspector]
    public bool phaseFinished;

    private SpeechBubble playerSpeech;
    private SpeechBubble enemySpeech;
    private SpeechText currentDialog;
    private SpeechText.Phase currentPhase;
    private IEnumerator<SpeechText> enumerator;

    void Awake() {
        playerSpeech = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpeechBubble>();
        enemySpeech = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<SpeechBubble>();
    }

    void Start() {
        PopulateDialogs();
        NextDialog();
    }

    void PopulateDialogs() {
        this.beginningDialogs = new List<SpeechText>();
        this.endingDialogs = new List<SpeechText>();
        this.beginningDialogs.Add(new SpeechText("Hola!", SpeechText.Source.Player, SpeechText.Phase.Beginning));
        this.beginningDialogs.Add(new SpeechText("Hola cabrón!", SpeechText.Source.Enemy, SpeechText.Phase.Beginning));
        this.endingDialogs.Add(new SpeechText("Y U MAD BRAH/?1!:?", SpeechText.Source.Player, SpeechText.Phase.Ending));
        enumerator = beginningDialogs.GetEnumerator();
    }

    void Update() {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            NextDialog();
        }
#endif
#if UNITY_ANDROID
        //if (Input.GetTouch(0).phase == TouchPhase.Ended) {
        //    print("touched!");
        //    if (!isFinished) {
        //        NextDialog();
        //    }
        //}
#endif
    }

    private void ShowDialog() {
        if (currentDialog.source == SpeechText.Source.Player) {
            enemySpeech.HideSelf();
            playerSpeech.ShowSelf();
            playerSpeech.SetText(currentDialog.text);
        } else {
            playerSpeech.HideSelf();
            enemySpeech.ShowSelf();
            enemySpeech.SetText(currentDialog.text);
        }
    }

    private void EndDialogPhase() {
        playerSpeech.HideSelf();
        enemySpeech.HideSelf();
        startGame.Invoke();
    }

    private void NextDialog() {
        var talking = enumerator.MoveNext();
        currentDialog = enumerator.Current;
        ShowDialog();
        if (!talking) {
            EndDialogPhase();
        }
    }
}
