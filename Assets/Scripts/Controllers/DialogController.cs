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
    private SpeechText.Phase currentPhase = SpeechText.Phase.Beginning;
    private IEnumerator<SpeechText> enumerator;

    void Awake() {
        playerSpeech = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpeechBubble>();
        enemySpeech = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<SpeechBubble>();
        beginningDialogs = new List<SpeechText>();
        endingDialogs = new List<SpeechText>();
    }

    void Start() {
        PopulateDialogs();
        StartDialogPhase(SpeechText.Phase.Beginning);
    }

    void PopulateDialogs() {
        beginningDialogs.Add(new SpeechText("Hola!", SpeechText.Source.Player, SpeechText.Phase.Beginning));
        beginningDialogs.Add(new SpeechText("Hola cabrón!", SpeechText.Source.Enemy, SpeechText.Phase.Beginning));

        endingDialogs.Add(new SpeechText("Y U MAD BRAH/?1!:?", SpeechText.Source.Player, SpeechText.Phase.Ending));

        enumerator = beginningDialogs.GetEnumerator();
    }

    void Update() {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            NextDialog(currentPhase);
        }
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.GetTouch(0).phase == TouchPhase.Ended) {
            NextDialog();
        }
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

    private void StartDialogPhase(SpeechText.Phase phase) {
        NextDialog(phase);
    }

    private void ChangePhase(SpeechText.Phase newPhase) {
        currentPhase = newPhase;
    }

    private void NextDialog(SpeechText.Phase phase) {
        var talking = enumerator.MoveNext();
        currentDialog = enumerator.Current;
        ShowDialog();
        if (!talking) {
            EndDialogPhase();
        }
    }
}
