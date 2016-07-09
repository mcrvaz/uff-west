using System.Xml;
using System.Xml.Serialization;

public class Duel {

    public float timeLimit;
    public string background;
    public float targetMinTime, targetMaxTime;
    public float evadeMinTime, evadeMaxTime;
    public float powerupMinTime, powerupMaxTime;

    public Duel() { }

    public Duel(
        float timeLimit, string background,
        float targetMinTime, float targetMaxTime,
        float evadeMinTime, float evadeMaxTime,
        float powerupMinTime, float powerupMaxTime
    ) {
        this.timeLimit = timeLimit;
        this.background = background;
        this.targetMinTime = targetMinTime;
        this.targetMaxTime = targetMaxTime;
        this.evadeMinTime = evadeMinTime;
        this.evadeMaxTime = evadeMaxTime;
        this.powerupMinTime = powerupMinTime;
        this.powerupMaxTime = powerupMaxTime;
    }
}
