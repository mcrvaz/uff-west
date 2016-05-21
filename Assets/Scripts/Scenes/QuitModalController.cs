using UnityEngine;

public class QuitModalController : MonoBehaviour {

    public void Quit() {
        //show modal for confirmation before quitting
        Pause();
        gameObject.SetActive(true);
    }

    public void ConfirmQuit() {
        //doesnt work on editor or webplayer
        print("Quit!");
        gameObject.SetActive(false);
        Application.Quit();
    }

    public void CancelQuit() {
        Unpause();
        gameObject.SetActive(false);
    }

    private void Pause() {
        Time.timeScale = 0;
    }

    private void Unpause() {
        Time.timeScale = 1;
    }

}
