using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogController : MonoBehaviour {

    public List<SpeechText> dialogs;
    [HideInInspector]
    public bool isPhaseFinished;

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
        this.dialogs = new List<SpeechText>();
        this.dialogs.Add(new SpeechText("Hola!", SpeechText.Source.Player, SpeechText.Phase.Beginning));
        this.dialogs.Add(new SpeechText("Hola cabrón!", SpeechText.Source.Enemy, SpeechText.Phase.Beginning));
        enumerator = dialogs.GetEnumerator();
        enumerator.MoveNext();
    }

    void Update() {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            print("clicked!");
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

    private void NextDialog() {
        currentDialog = enumerator.Current;
        isPhaseFinished = enumerator.MoveNext();
        print(currentDialog.text);
        ShowDialog();
    }
}
