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
    public bool sequential { get; private set; }

    public SpeechText() { } //Needed for XMLSerializer

    public SpeechText(string text, Source source, Phase phase, bool hasNext) {
        this.text = text;
        this.source = source;
        this.phase = phase;
        this.sequential = hasNext;
    }

    public override string ToString() {
        return "Text: " + text + ", Source: " + source + ", Phase: " + phase + ", Sequential: " + sequential;
    }

}
