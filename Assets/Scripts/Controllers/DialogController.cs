using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class DialogController : MonoBehaviour {

    public UnityEvent endDialogEvent; //what happens after dialog phase ends
    public List<SpeechText> dialogs = new List<SpeechText>();

    public SpeechText.Phase phase;

    private SpeechBubble playerSpeech;
    private SpeechBubble enemySpeech;
    private SpeechText currentDialog;
    private IEnumerator<SpeechText> enumerator;

    void Start() {
        playerSpeech = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpeechBubble>();
        enemySpeech = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<SpeechBubble>();
        StartDialogPhase(SpeechText.Phase.Beginning);
    }

    void Update() {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            NextDialog();
        }
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        if(Input.touchCount > 0){
            if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                NextDialog();
            }
        }
#endif
    }

    private void ShowDialog() {
        if (currentDialog == null) {
            return; //no dialog
        }

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
        endDialogEvent.Invoke();
        Destroy(gameObject);
    }

    private void StartDialogPhase(SpeechText.Phase phase) {
        enumerator = dialogs.GetEnumerator();
        NextDialog();
    }

    private void NextDialog() {
        var hasNext = enumerator.MoveNext();
        currentDialog = enumerator.Current;
        ShowDialog();

        if (!hasNext) {
            EndDialogPhase();
        }
    }

}
