using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechBubble : MonoBehaviour {

    private Image speechBubble;
    private Text speechText;

    void Awake() {
        speechBubble = GetComponentInChildren<Image>();
        speechText = GetComponentInChildren<Text>();
    }

    public void SetText(string newText) {
        speechText.text = newText;
    }

    public void HideSelf() {
        speechBubble.enabled = false;
        speechText.enabled = false;
    }

    public void ShowSelf() {
        speechBubble.enabled = true;
        speechText.enabled = true;
    }

}
