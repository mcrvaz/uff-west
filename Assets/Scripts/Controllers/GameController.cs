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

    private List<EnemyXML> enemies;
    private IEnumerator<EnemyXML> enemyEnumerator;

    private List<EnemyXML> deathEnemies;
    private IEnumerator<EnemyXML> deathEnumerator;

    private List<PlayerXML> players;
    private IEnumerator<PlayerXML> playerEnumerator;

    private List<DuelXML> duels;
    private IEnumerator<DuelXML> duelEnumerator;

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
        this.duels = new List<DuelXML>();
        var container = new DuelXMLContainer("duels.xml");
        container.Load();
        this.duels = container.duels;
        duelEnumerator = duels.GetEnumerator();
    }

    private void LoadPlayers() {
        this.players = new List<PlayerXML>();
        var container = new PlayerXMLContainer("players.xml");
        container.Load();
        this.players = container.players;
        playerEnumerator = players.GetEnumerator();
    }

    private void LoadEnemies() {
        this.enemies = new List<EnemyXML>();
        var container = new EnemyXMLContainer("enemies.xml");
        container.Load();
        this.enemies = container.enemies;
        enemyEnumerator = enemies.GetEnumerator();
    }

    private void LoadDeathEnemies() {
        this.deathEnemies = new List<EnemyXML>();
        var container = new EnemyXMLContainer("deathEnemies.xml");
        container.Load();
        this.deathEnemies = container.enemies;
        deathEnumerator = deathEnemies.GetEnumerator();
    }

    public DuelXML GetNextDuel() {
        duelEnumerator.MoveNext();
        return duelEnumerator.Current;
    }

    public EnemyXML GetNextEnemy() {
        EnemyXML enemy;
        if (!isDeathDuel) {
            enemyEnumerator.MoveNext();
            enemy = enemyEnumerator.Current;
        } else {
            deathEnumerator.MoveNext();
            enemy = deathEnumerator.Current;
        }
        return enemy;
    }

    public PlayerXML GetNextPlayer() {
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
