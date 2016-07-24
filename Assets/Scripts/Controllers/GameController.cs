using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Enum = System.Enum;

public class GameController : Singleton<GameController> {
    public enum GameMode { Endless, Story };

    // guarantee this will be always a singleton only - can't use the constructor!
    protected GameController() { }

    #region Control
    public StatisticsController stats;
    public bool lastDuel { get; private set; }
    public bool victory { get; private set; } //true if player won last duel
    //everytime the player loses a regular duel, start a death duel
    //if the player wins death duel, restart previous duel, else, game over.
    public bool isDeathDuel { get; set; }
    public GameMode gameMode = GameMode.Endless;
    public bool modalActive;
    private GameObject quitModal, menuModal;
    private int currentLevel;
    private int currentDeathLevel;
    #endregion

    #region SaveGame
    private SaveGameController saveGameController = new SaveGameController(
        new GameStateXMLContainer("saveGame")
    );
    #endregion

    #region Story
    private List<Enemy> enemies = new List<Enemy>();
    private IEnumerator<Enemy> enemyEnumerator;

    private List<Enemy> deathEnemies = new List<Enemy>();
    private IEnumerator<Enemy> deathEnemyEnumerator;

    private List<Player> players = new List<Player>();
    private IEnumerator<Player> playerEnumerator;

    private List<Player> deathPlayers = new List<Player>();
    private IEnumerator<Player> deathPlayerEnumerator;

    private List<Duel> duels = new List<Duel>();
    private IEnumerator<Duel> duelEnumerator;

    private List<Duel> deathDuels = new List<Duel>();
    private IEnumerator<Duel> deathDuelEnumerator;

    private List<Contract> contracts = new List<Contract>();
    private IEnumerator<Contract> contractEnumerator;
    #endregion

    #region Endless
    private PlayerGenerator playerGenerator = new PlayerGenerator(
        damage: 500f, health: 100f
    );
    private Player endlessPlayer;

    private DuelGenerator duelGenerator = new DuelGenerator(
        newTimeLimit: 30,
        newTargetMinTime: 0.2f, newTargetMaxTime: 0.5f,
        newEvadeMinTime: 1f, newEvadeMaxTime: 3f,
        newPowerupMinTime: 2f, newPowerupMaxTime: 5f
    );
    private Duel endlessDuel;

    private EnemyGenerator enemyGenerator = new EnemyGenerator(
        healthAdd: 0, damageAdd: 0,
        minTimeToClickAdd: 0.1f, maxTimeToClickAdd: 0.1f
    );
    private Enemy endlessEnemy;

    private ContractGenerator contractGenerator = new ContractGenerator();
    private Contract endlessContract;
    #endregion

    void Awake() {
        quitModal = Resources.Load("Prefabs/ModalCanvas") as GameObject;
        menuModal = Resources.Load("Prefabs/MenuModalCanvas") as GameObject;
        LoadContracts();
        LoadDuels();
        LoadCharacters();
    }

    void Update() {
        //listens to back button
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (!modalActive) {
                OpenReturnToMenuModal();
                modalActive = true;
            }
        }
    }

    public void SetGameMode(string mode) {
        var enumValues = Enum.GetValues(typeof(GameMode));
        foreach (GameMode gm in enumValues) {
            if (mode.Equals(Enum.GetName(typeof(GameMode), gm))) {
                this.gameMode = gm;
                return;
            }
        }
        throw new System.Exception("Invalid enumerator name.");
    }

    public void OpenQuitModal() {
        Instantiate(quitModal);
    }

    public void OpenReturnToMenuModal() {
        Instantiate(menuModal);
    }

    public void LoadGame() {
        var state = saveGameController.currentState;
        ResetEnumerators();

        this.currentLevel = state.level;
        for (int i = 0; i < currentLevel; i++) {
            duelEnumerator.MoveNext();
            contractEnumerator.MoveNext();
            playerEnumerator.MoveNext();
            enemyEnumerator.MoveNext();
        }

        this.currentDeathLevel = state.deathLevel;
        for (int i = 0; i < currentDeathLevel; i++) {
            deathDuelEnumerator.MoveNext();
            deathEnemyEnumerator.MoveNext();
            deathPlayerEnumerator.MoveNext();
        }
        this.isDeathDuel = state.isDeathDuel;
        this.lastDuel = state.lastDuel;
        this.endlessEnemy = state.endlessEnemy;
        this.endlessPlayer = state.endlessPlayer;
        this.endlessDuel = state.endlessDuel;
        this.endlessContract = state.endlessContract;
    }

    public void SaveGame() {
        var state = new GameState(
            level: this.currentLevel, deathLevel: this.currentDeathLevel,
            lastDuel: this.lastDuel, isDeathDuel: this.isDeathDuel,
            endlessDuel: this.endlessDuel, endlessEnemy: this.endlessEnemy,
            endlessPlayer: this.endlessPlayer, endlessContract: this.endlessContract
        );
        saveGameController.currentState = state;
        saveGameController.SaveGame();
    }

    private void LoadCharacters() {
        LoadPlayers();
        LoadEnemies();
        LoadDeathPlayers();
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

        endlessDuel = duelGenerator.Generate();
    }

    private void LoadContracts() {
        var container = new ContractXMLContainer("contracts");
        container.Load();
        this.contracts = container.contracts;
        contractEnumerator = contracts.GetEnumerator();
        contractEnumerator.MoveNext();

        endlessContract = contractGenerator.Generate();
    }

    private void LoadPlayers() {
        var container = new PlayerXMLContainer("players");
        container.Load();
        this.players = container.players;
        playerEnumerator = players.GetEnumerator();
        playerEnumerator.MoveNext();

        endlessPlayer = playerGenerator.Generate();
    }

    private void LoadDeathPlayers() {
        var container = new PlayerXMLContainer("deathPlayers");
        container.Load();
        this.deathPlayers = container.players;
        deathPlayerEnumerator = deathPlayers.GetEnumerator();
        deathPlayerEnumerator.MoveNext();
    }

    private void LoadEnemies() {
        var container = new EnemyXMLContainer("enemies");
        container.Load();
        this.enemies = container.enemies;
        enemyEnumerator = enemies.GetEnumerator();
        enemyEnumerator.MoveNext();

        endlessEnemy = enemyGenerator.Generate(endlessContract.facePrefab);
    }

    private void LoadDeathEnemies() {
        var container = new EnemyXMLContainer("deathEnemies");
        container.Load();
        this.deathEnemies = container.enemies;
        deathEnemyEnumerator = deathEnemies.GetEnumerator();
        deathEnemyEnumerator.MoveNext();
    }

    public Contract GetContract() {
        switch (gameMode) {
            case GameMode.Endless:
                return GetEndlessContract();
            case GameMode.Story:
                return GetStoryContract();
            default:
                return null;
        }
    }

    private Contract GetEndlessContract() {
        return endlessContract;
    }

    private Contract GetStoryContract() {
        return contractEnumerator.Current;
    }

    public Duel GetDuel() {
        switch (gameMode) {
            case GameMode.Endless:
                return GetEndlessDuel();
            case GameMode.Story:
                return GetStoryDuel();
            default:
                return null;
        }
    }

    private Duel GetEndlessDuel() {
        if (!isDeathDuel) {
            return endlessDuel;
        } else {
            return deathDuelEnumerator.Current;
        }
    }

    private Duel GetStoryDuel() {
        if (!isDeathDuel) {
            return duelEnumerator.Current;
        } else {
            return deathDuelEnumerator.Current;
        }
    }

    public Enemy GetEnemy() {
        switch (gameMode) {
            case GameMode.Endless:
                return GetEndlessEnemy();
            case GameMode.Story:
                return GetStoryEnemy();
            default:
                return null;
        }
    }

    private Enemy GetEndlessEnemy() {
        if (isDeathDuel) {
            return deathEnemyEnumerator.Current;
        } else {
            return endlessEnemy;
        }
    }

    private Enemy GetStoryEnemy() {
        if (isDeathDuel) {
            return deathEnemyEnumerator.Current;
        } else {
            return enemyEnumerator.Current;
        }
    }

    public Player GetPlayer() {
        switch (gameMode) {
            case GameMode.Endless:
                return GetEndlessPlayer();
            case GameMode.Story:
                return GetStoryPlayer();
            default:
                return null;
        }
    }

    private Player GetEndlessPlayer() {
        if (isDeathDuel) {
            return deathPlayerEnumerator.Current;
        } else {
            return endlessPlayer;
        }
    }

    private Player GetStoryPlayer() {
        if (isDeathDuel) {
            return deathPlayerEnumerator.Current;
        } else {
            return playerEnumerator.Current;
        }
    }

    private void SetNextPlayer() {
        switch (gameMode) {
            case GameMode.Endless:
                SetNextPlayerEndless();
                break;
            case GameMode.Story:
                SetNextPlayerStory();
                break;
        }
    }

    private void SetNextPlayerEndless() {
        if (!isDeathDuel) {
            endlessPlayer = playerGenerator.Generate();
        } else {
            deathPlayerEnumerator.MoveNext();
        }
    }

    private void SetNextPlayerStory() {
        if (!isDeathDuel) {
            playerEnumerator.MoveNext();
        } else {
            deathPlayerEnumerator.MoveNext();
        }
    }

    private void SetNextEnemy() {
        switch (gameMode) {
            case GameMode.Endless:
                SetNextEnemyEndless();
                break;
            case GameMode.Story:
                SetNextEnemyStory();
                break;
        }
    }

    private void SetNextEnemyEndless() {
        if (!isDeathDuel) {
            endlessEnemy = enemyGenerator.Generate(endlessContract.facePrefab);
        } else {
            deathEnemyEnumerator.MoveNext();
        }
    }

    private void SetNextEnemyStory() {
        if (!isDeathDuel) {
            enemyEnumerator.MoveNext();
        } else {
            deathEnemyEnumerator.MoveNext();
        }
    }

    private void SetNextDuel() {
        switch (gameMode) {
            case GameMode.Endless:
                SetNextDuelEndless();
                break;
            case GameMode.Story:

                SetNextDuelStory();
                break;
        }
    }

    private void SetNextDuelEndless() {
        if (!isDeathDuel) {
            endlessDuel = duelGenerator.Generate();
        } else {
            deathDuelEnumerator.MoveNext(); //repeat last one until player is dead.
        }
    }

    private void SetNextDuelStory() {
        if (!isDeathDuel) {
            currentLevel++;
            lastDuel = !duelEnumerator.MoveNext();
        } else {
            currentDeathLevel++;
            deathDuelEnumerator.MoveNext(); //repeat last one until player is dead.
        }
    }

    private void SetNextContract() {
        switch (gameMode) {
            case GameMode.Endless:
                SetNextContractEndless();
                break;
            case GameMode.Story:
                SetNextContractStory();
                break;
        }
    }

    private void SetNextContractEndless() {
        if (!isDeathDuel) {
            endlessContract = contractGenerator.Generate();
        }
    }

    private void SetNextContractStory() {
        if (!isDeathDuel) {
            contractEnumerator.MoveNext();
        }
    }

    public void EndDuel(DuelCharacterController winnerCharacter) {
        if (winnerCharacter is EnemyCharacterController) {
            victory = false;
            if (isDeathDuel) {
                NewGame();
                //player just lost a death duel
            } else {
                //player just lost a regular duel
            }
        } else {
            //player won
            SetNextDuel();
            SetNextPlayer();
            SetNextEnemy();
            SetNextContract();
            victory = true;
            isDeathDuel = false;
        }

        SaveGame();
        SceneManager.LoadScene(SceneNames.DUEL_STATISTICS);
    }

    private void ResetEnumerators() {
        contractEnumerator.Reset();
        playerEnumerator.Reset();
        enemyEnumerator.Reset();
        deathEnemyEnumerator.Reset();
        duelEnumerator.Reset();
        deathDuelEnumerator.Reset();
        contractEnumerator.MoveNext();
        playerEnumerator.MoveNext();
        enemyEnumerator.MoveNext();
        deathEnemyEnumerator.MoveNext();
        duelEnumerator.MoveNext();
        deathDuelEnumerator.MoveNext();
    }

    public void NewGame() {
        ResetEnumerators();

        endlessContract = contractGenerator.Reset();
        endlessDuel = duelGenerator.Reset();
        endlessPlayer = playerGenerator.Reset();
        endlessEnemy = enemyGenerator.Reset();

        currentLevel = 0;
        currentDeathLevel = 0;

        SaveGame();
    }

}
