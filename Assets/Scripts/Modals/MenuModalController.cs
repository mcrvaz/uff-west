using UnityEngine.SceneManagement;

public class MenuModalController : ModalController {

    public override void Confirm() {
        Destroy(transform.parent.gameObject);
        Unpause();
        if (SceneManager.GetActiveScene().name == SceneNames.MAIN_MENU) {
            return;
        }
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }

    public override void Cancel() {
        Unpause();
        Destroy(transform.parent.gameObject);
    }

}
