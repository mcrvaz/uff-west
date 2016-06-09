using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundButtonController : MonoBehaviour {

    public Sprite enabledButton;
    public Sprite disabledButton;

    private Image currentImage;
    private bool soundEnabled;

    void Awake() {
        currentImage = GetComponent<Image>();
    }

    public void EnableSound() {
        soundEnabled = !soundEnabled;
        currentImage.sprite = soundEnabled ? disabledButton : enabledButton;
    }

}
