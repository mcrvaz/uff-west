using System.Xml;
using System.Xml.Serialization;

public class DuelXML {

    public float timeLimit;
    public string backgroundPath;
    public float targetMinTime, targetMaxTime;
    public float evadeMinTime, evadeMaxTime;
    public float powerupMinTime, powerupMaxTime;

    public DuelXML() { }

    public DuelXML(
        float timeLimit, string backgroundPath,
        float targetMinTime, float targetMaxTime,
        float evadeMinTime, float evadeMaxTime,
        float powerupMinTime, float powerupMaxTime
    ) {
        this.timeLimit = timeLimit;
        this.backgroundPath = backgroundPath;
        this.targetMinTime = targetMinTime;
        this.targetMaxTime = targetMaxTime;
        this.evadeMinTime = evadeMinTime;
        this.evadeMaxTime = evadeMaxTime;
        this.powerupMinTime = powerupMinTime;
        this.powerupMaxTime = powerupMaxTime;
    }
}
