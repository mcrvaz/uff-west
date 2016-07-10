using UnityEngine;

public class QuitModalController : MonoBehaviour {

    void Start() {
        Pause();
    }

    public void ConfirmQuit() {
        //doesnt work on editor or webplayer
        print("Quit!");
        Destroy(transform.parent.gameObject);
        Application.Quit();
    }

    public void CancelQuit() {
        Unpause();
        Destroy(transform.parent.gameObject);
    }

    void OnDestroy() {
        GameController.Instance.modalActive = false; //sorry
    }

    private void Pause() {
        Time.timeScale = 0;
    }

    private void Unpause() {
        Time.timeScale = 1;
    }

}
