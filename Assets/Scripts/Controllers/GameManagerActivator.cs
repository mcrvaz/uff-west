#pragma warning disable 0414

using UnityEngine;
using System.Collections;

public class GameManagerActivator : MonoBehaviour {

    private GameController gameController;

    void Start() {
        //intiliazes the singleton, making the gamecontroller available on the scene.
        //warning unused variable is disable above.
        gameController = GameController.Instance;
    }
}
