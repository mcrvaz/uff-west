using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class GameState {

    public int level { get; private set; }
    public int deathLevel { get; private set; }
    public bool lastDuel { get; private set; }
    public bool isDeathDuel { get; private set; }

    public Duel endlessDuel { get; private set; }
    public Enemy endlessEnemy { get; private set; }
    public Player endlessPlayer { get; private set; }
    public Contract endlessContract { get; private set; }

    public GameState() { }

    public GameState(
        int level, int deathLevel, bool lastDuel,
        bool isDeathDuel, Duel endlessDuel,
        Enemy endlessEnemy, Player endlessPlayer, Contract endlessContract
    ) {
        this.level = level;
        this.deathLevel = deathLevel;
        this.lastDuel = lastDuel;
        this.isDeathDuel = isDeathDuel;
        this.endlessDuel = endlessDuel;
        this.endlessEnemy = endlessEnemy;
        this.endlessPlayer = endlessPlayer;
        this.endlessContract = endlessContract;
    }
}
