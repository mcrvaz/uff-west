using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuSceneController : MonoBehaviour {

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    private IEnumerator PlayTransition() {
        animator.Play("SlideUp");
        var animation = animator.GetCurrentAnimatorClipInfo(0)[0];
        yield return new WaitForSeconds(animation.clip.length);
    }

    private IEnumerator ChangeScene(string sceneName) {
        if (animator != null) {
            yield return StartCoroutine(PlayTransition());
        }
        SceneManager.LoadScene(sceneName);
    }

    public void Quit() {
        GameController.Instance.OpenQuitModal();
    }

    public void StartGame(string mode) {
        var gc = GameController.Instance;
        gc.SetGameMode(mode);
        if (gc.isDeathDuel) {
            this.Duel();
        } else {
            this.Contract();
        }
    }

    public void NewGame() {
        GameController.Instance.NewGame();
        this.GameMode();
    }

    public void ContinueGame() {
        GameController.Instance.LoadGame();
        this.GameMode();
    }

    public void Instructions() {
        StartCoroutine(ChangeScene(SceneNames.INSTRUCTIONS));
    }

    public void Statistics() {
        StartCoroutine(ChangeScene(SceneNames.STATISTICS));
    }

    public void Achievements() {
        StartCoroutine(ChangeScene(SceneNames.ACHIEVEMENTS));
    }

    public void Menu() {
        StartCoroutine(ChangeScene(SceneNames.MAIN_MENU));
    }

    public void Contract() {
        StartCoroutine(ChangeScene(SceneNames.CONTRACT));
    }

    public void GameMode() {
        StartCoroutine(ChangeScene(SceneNames.GAME_MODES));
    }

    public void Duel() {
        StartCoroutine(ChangeScene(SceneNames.DUEL));
    }

    public void DuelStatistics() {
        StartCoroutine(ChangeScene(SceneNames.DUEL_STATISTICS));
    }

    public void GameOver() {
        StartCoroutine(ChangeScene(SceneNames.GAME_OVER));
    }

    public void Credits() {
        StartCoroutine(ChangeScene(SceneNames.CREDITS));
    }

    public void EndGame() {
        StartCoroutine(ChangeScene(SceneNames.END_GAME));
    }

    public void PreDeathDuel() {
        StartCoroutine(ChangeScene(SceneNames.PRE_DEATH_DUEL));
    }

}
