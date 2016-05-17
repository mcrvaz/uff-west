using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour {

    public void StartGame() {
        SceneManager.LoadScene(SceneNames.CONTRACT);
    }

    public void Instructions() {
        SceneManager.LoadScene(SceneNames.INSTRUCTIONS);
    }

    public void Statistics() {
        SceneManager.LoadScene(SceneNames.STATISTICS);
    }

    public void Achievements() {
        SceneManager.LoadScene(SceneNames.ACHIEVEMENTS);
    }

    public void Menu() {
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }

    public void Quit() {
        //show modal for confirmation before quitting
        //doesnt work on editor or webplayer
        Application.Quit();
    }
}
