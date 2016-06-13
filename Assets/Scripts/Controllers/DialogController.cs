using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class DialogController : MonoBehaviour {

    public UnityEvent endDialogEvent; //what happens after dialog phase ends
    public List<SpeechText> dialogs;

    public SpeechText.Phase phase;

    private SpeechBubble playerSpeech;
    private SpeechBubble enemySpeech;
    private SpeechText currentDialog;
    private IEnumerator<SpeechText> enumerator;

    void Awake() {
        playerSpeech = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpeechBubble>();
        enemySpeech = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<SpeechBubble>();
        dialogs = new List<SpeechText>();
    }

    void Start() {
        PopulateDialogs();
        StartDialogPhase(SpeechText.Phase.Beginning);
    }

    void PopulateDialogs() {
        var d = new List<SpeechText>();
        //suppose this is the XML QUE O MOHAMMED AINDA NÃO FEZ
        d.Add(new SpeechText("Hola!", SpeechText.Source.Player, SpeechText.Phase.Beginning));
        d.Add(new SpeechText("Hola cabrón!", SpeechText.Source.Enemy, SpeechText.Phase.Beginning));
        d.Add(new SpeechText("Czy mówisz po polsku?", SpeechText.Source.Enemy, SpeechText.Phase.Beginning));
        d.Add(new SpeechText("What did u say bish?!?", SpeechText.Source.Player, SpeechText.Phase.Beginning));

        d.Add(new SpeechText("empty dialog", SpeechText.Source.Player, SpeechText.Phase.Victory)); //workaround
        d.Add(new SpeechText("i won!", SpeechText.Source.Player, SpeechText.Phase.Victory));
        d.Add(new SpeechText("rip", SpeechText.Source.Enemy, SpeechText.Phase.Victory));

        d.Add(new SpeechText("you lost!", SpeechText.Source.Enemy, SpeechText.Phase.Defeat));
        d.Add(new SpeechText("rip", SpeechText.Source.Player, SpeechText.Phase.Defeat));

        foreach (var dialog in d) {
            if (dialog.phase == this.phase) {
                dialogs.Add(dialog);
            }
        }
    }

    void Update() {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            NextDialog();
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
        print(currentDialog.text);
        ShowDialog();

        if (!hasNext) {
            EndDialogPhase();
        }
    }

}
