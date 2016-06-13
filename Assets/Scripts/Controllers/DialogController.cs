using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class DialogController : MonoBehaviour {

    public UnityEvent startGame; //what happens after first dialog phase ends
    public UnityEvent endGame; //what happens after second dialog phase ends
    public List<SpeechText> beginningDialogs;
    public List<SpeechText> endingDialogs;

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
        beginningDialogs.Add(new SpeechText("Czy mówisz po polsku?", SpeechText.Source.Enemy, SpeechText.Phase.Beginning));
        beginningDialogs.Add(new SpeechText("What did u say bish?!?", SpeechText.Source.Player, SpeechText.Phase.Beginning));

        endingDialogs.Add(new SpeechText("u mad?", SpeechText.Source.Player, SpeechText.Phase.Ending));
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
        var dialogList = (phase == SpeechText.Phase.Beginning) ? beginningDialogs : endingDialogs;
        enumerator = dialogList.GetEnumerator();
        NextDialog(phase);
    }

    private void NextDialog(SpeechText.Phase phase) {
        var talking = enumerator.MoveNext();
        currentDialog = enumerator.Current;
        ShowDialog();
        if (!talking) {
            EndDialogPhase();
        }
    }

    public void NextPhase() {
        currentPhase = SpeechText.Phase.Ending;
        StartDialogPhase(currentPhase);
    }
}
