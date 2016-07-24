using System.Collections;

public class SaveGameController {

    public GameState currentState;

    private GameStateXMLContainer container;

    public SaveGameController(GameStateXMLContainer container) {
        this.currentState = new GameState(
            level: 0, deathLevel: 0,
            lastDuel: false, isDeathDuel: false,
            endlessDuel: null, endlessEnemy: null,
            endlessPlayer: null, endlessContract: null
        );
        this.container = container;
        currentState = LoadGame();
    }

    public void SaveGame() {
        container.state = currentState;
        container.Save();
    }

    public GameState LoadGame() {
        container.Load();
        return container.state;
    }
}
