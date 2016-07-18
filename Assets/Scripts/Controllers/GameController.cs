﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : Singleton<GameController> {
    // guarantee this will be always a singleton only - can't use the constructor!
    protected GameController() { }

    public StatisticsController stats;
    public bool lastDuel { get; private set; }
    public bool victory { get; private set; } //true if player won last duel
    //everytime the player loses a regular duel, start a death duel
    //if the player wins death duel, restart previous duel, else, game over.
    public bool isDeathDuel { get; set; }

    private List<Enemy> enemies = new List<Enemy>();
    private IEnumerator<Enemy> enemyEnumerator;

    private List<Enemy> deathEnemies = new List<Enemy>();
    private IEnumerator<Enemy> deathEnumerator;

    private List<Player> players = new List<Player>();
    private IEnumerator<Player> playerEnumerator;

    private List<Duel> duels = new List<Duel>();
    private IEnumerator<Duel> duelEnumerator;

    private List<Duel> deathDuels = new List<Duel>();
    private IEnumerator<Duel> deathDuelEnumerator;

    private List<Contract> contracts = new List<Contract>();
    private IEnumerator<Contract> contractEnumerator;

    private GameObject quitModal;
    public bool modalActive;

    void Awake() {
        quitModal = Resources.Load("Prefabs/ModalCanvas") as GameObject;
        LoadCharacters();
        LoadDuels();
        LoadContracts();
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (!modalActive) {
                OpenQuitModal();
                modalActive = true;
            }
        }
    }

    public void OpenQuitModal() {
        Instantiate(quitModal);
    }

    private void LoadCharacters() {
        LoadPlayers();
        LoadEnemies();
        LoadDeathEnemies();
        LoadDeathDuels();
    }

    private void LoadDeathDuels() {
        var container = new DuelXMLContainer("deathDuels");
        container.Load();
        this.deathDuels = container.duels;
        deathDuelEnumerator = deathDuels.GetEnumerator();
        deathDuelEnumerator.MoveNext();
    }

    private void LoadDuels() {
        var container = new DuelXMLContainer("duels");
        container.Load();
        this.duels = container.duels;
        duelEnumerator = duels.GetEnumerator();
        duelEnumerator.MoveNext();
    }

    private void LoadContracts() {
        var container = new ContractXMLContainer("contracts");
        container.Load();
        this.contracts = container.contracts;
        contractEnumerator = contracts.GetEnumerator();
        contractEnumerator.MoveNext();
    }

    private void LoadPlayers() {
        var container = new PlayerXMLContainer("players");
        container.Load();
        this.players = container.players;
        playerEnumerator = players.GetEnumerator();
        playerEnumerator.MoveNext();
    }

    private void LoadEnemies() {
        var container = new EnemyXMLContainer("enemies");
        container.Load();
        this.enemies = container.enemies;
        enemyEnumerator = enemies.GetEnumerator();
        enemyEnumerator.MoveNext();
    }

    private void LoadDeathEnemies() {
        var container = new EnemyXMLContainer("deathEnemies");
        container.Load();
        this.deathEnemies = container.enemies;
        deathEnumerator = deathEnemies.GetEnumerator();
        deathEnumerator.MoveNext();
    }

    public Contract GetContract() {
        return contractEnumerator.Current;
    }

    public Duel GetDuel() {
        if (!isDeathDuel) {
            return duelEnumerator.Current;
        } else {
            return deathDuelEnumerator.Current;
        }
    }

    public Enemy GetEnemy() {
        if (isDeathDuel) {
            return deathEnumerator.Current;
        } else {
            return enemyEnumerator.Current;
        }
    }

    public Player GetPlayer() {
        return playerEnumerator.Current;
    }

    private void SetNextPlayer() {
        playerEnumerator.MoveNext();
    }

    private void SetNextEnemy() {
        if (!isDeathDuel) {
            enemyEnumerator.MoveNext();
        } else {
            deathEnumerator.MoveNext();
        }
    }

    private void SetNextDuel() {
        if (!isDeathDuel) {
            lastDuel = !duelEnumerator.MoveNext();
        } else {
            deathDuelEnumerator.MoveNext(); //repeats last one until player is dead.
        }
    }

    private void SetNextContract() {
        if (!isDeathDuel) {
            contractEnumerator.MoveNext();
        }
    }

    public void EndDuel(DuelCharacterController winnerCharacter) {
        SetNextPlayer();

        if (winnerCharacter is EnemyCharacterController) {
            victory = false;
            if (isDeathDuel) {
                NewGame();
                print("Player died. Forever.");
            } else {
                print("Player lost!");
            }
        } else {
            print("Player won!");
            SetNextDuel();
            SetNextEnemy();
            SetNextContract();
            victory = true;
            isDeathDuel = false;
        }

        SceneManager.LoadScene(SceneNames.DUEL_STATISTICS);
    }

    public void NewGame() {
        contractEnumerator.Reset();
        contractEnumerator.MoveNext();

        playerEnumerator.Reset();
        playerEnumerator.MoveNext();

        enemyEnumerator.Reset();
        enemyEnumerator.MoveNext();

        deathEnumerator.Reset();
        deathEnumerator.MoveNext();

        duelEnumerator.Reset();
        duelEnumerator.MoveNext();

        deathDuelEnumerator.Reset();
        deathDuelEnumerator.MoveNext();
    }

}
