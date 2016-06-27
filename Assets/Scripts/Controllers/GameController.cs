using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//should have every attribute needed for generating a new duel or contract
public class GameController : Singleton<GameController> {
    // guarantee this will be always a singleton only - can't use the constructor!
    protected GameController() { }

    //everytime the player loses a regular duel, start a death duel
    //if the player wins death duel, restart previous duel, else, game over.
    private bool isDeathDuel;

    private List<Enemy> enemies = new List<Enemy>();
    private IEnumerator<Enemy> enemyEnumerator;

    private List<Enemy> deathEnemies = new List<Enemy>();
    private IEnumerator<Enemy> deathEnumerator;

    private List<Player> players = new List<Player>();
    private IEnumerator<Player> playerEnumerator;

    private List<Duel> duels = new List<Duel>();
    private IEnumerator<Duel> duelEnumerator;

    void Awake() {
        LoadCharacters();
        LoadDuels();
    }

    private void LoadCharacters() {
        LoadPlayers();
        LoadEnemies();
        LoadDeathEnemies();
    }

    private void LoadDuels() {
        var container = new DuelXMLContainer("duels.xml");
        container.Load();
        this.duels = container.duels;
        duelEnumerator = duels.GetEnumerator();
    }

    private void LoadPlayers() {
        var container = new PlayerXMLContainer("players.xml");
        container.Load();
        this.players = container.players;
        playerEnumerator = players.GetEnumerator();
    }

    private void LoadEnemies() {
        var container = new EnemyXMLContainer("enemies.xml");
        container.Load();
        this.enemies = container.enemies;
        enemyEnumerator = enemies.GetEnumerator();
    }

    private void LoadDeathEnemies() {
        var container = new EnemyXMLContainer("deathEnemies.xml");
        container.Load();
        this.deathEnemies = container.enemies;
        deathEnumerator = deathEnemies.GetEnumerator();
    }

    public Duel GetNextDuel() {
        duelEnumerator.MoveNext();
        return duelEnumerator.Current;
    }

    public Enemy GetNextEnemy() {
        Enemy enemy;
        if (!isDeathDuel) {
            enemyEnumerator.MoveNext();
            enemy = enemyEnumerator.Current;
        } else {
            deathEnumerator.MoveNext();
            enemy = deathEnumerator.Current;
        }
        return enemy;
    }

    public Player GetNextPlayer() {
        playerEnumerator.MoveNext();
        return playerEnumerator.Current;
    }

    public void EndDuel(DuelCharacterController winnerCharacter) {
        if (winnerCharacter is EnemyCharacterController) {
            if (isDeathDuel) {
                print("Player died. Forever.");
                SceneManager.LoadScene(SceneNames.GAME_OVER);
            } else {
                isDeathDuel = true;
                print("Player lost!");
                SceneManager.LoadScene(SceneNames.CONTRACT);
            }
        } else {
            print("Player won!");
            isDeathDuel = false;
            SceneManager.LoadScene(SceneNames.DUEL_STATISTICS);
        }
    }

}
