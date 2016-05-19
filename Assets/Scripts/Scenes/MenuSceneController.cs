using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuSceneController : MonoBehaviour {

    public GameObject modal;

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

    public void Contract() {
        SceneManager.LoadScene(SceneNames.CONTRACT);
    }

    public void Duel() {
        SceneManager.LoadScene(SceneNames.DUEL);
    }

    public void DuelStatistics() {
        SceneManager.LoadScene(SceneNames.DUEL_STATISTICS);
    }

    public void Quit() {
        //show modal for confirmation before quitting
        modal.SetActive(true);
    }

    public void ConfirmQuit() {
        //doesnt work on editor or webplayer
        print("Quit!");
        modal.SetActive(false);
        Application.Quit();
    }

    public void CancelQuit() {
        modal.SetActive(false);
    }


}
