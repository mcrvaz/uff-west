public class SpeechText {

    public enum Source {
        Player, Enemy
    }

    public enum Phase {
        Beginning, Victory, Defeat
    }

    public string text { get; private set; }
    public Source source { get; private set; }
    public Phase phase { get; private set; }

    public SpeechText(string text, Source source, Phase phase) {
        this.text = text;
        this.source = source;
        this.phase = phase;
    }

}
