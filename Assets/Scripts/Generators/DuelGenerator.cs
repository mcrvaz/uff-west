using System.Collections.Generic;

public class DuelGenerator : Generator<Duel> {

    public Duel lastDuel { get; private set; }

    private List<string> backgrounds = new List<string>(new string[] { });

    public Duel Generate() {
        return new Duel();
    }
}
