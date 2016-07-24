using UnityEngine;

public abstract class ModalController : MonoBehaviour {

    void Start() {
        Pause();
    }

    public abstract void Confirm();
    public abstract void Cancel();

    protected void Pause() {
        Time.timeScale = 0;
    }

    protected void Unpause() {
        Time.timeScale = 1;
    }

    void OnDestroy() {
        GameController.Instance.modalActive = false;
    }
}
