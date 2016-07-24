using UnityEngine;

public class QuitModalController : ModalController {

    public override void Confirm() {
        //doesnt work on editor or webplayer
        print("Quit!");
        Unpause();
        Destroy(transform.parent.gameObject);
        Application.Quit();
    }

    public override void Cancel() {
        Unpause();
        Destroy(transform.parent.gameObject);
    }

}
