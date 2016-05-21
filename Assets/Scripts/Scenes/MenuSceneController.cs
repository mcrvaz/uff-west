using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour {

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


}
