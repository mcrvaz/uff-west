using System.Xml;
using System.Xml.Serialization;

public class EnemyXML {

    [XmlAttribute("characterName")]
    public string characterName;
    public float health;
    public float damage;
    public float minTimeToClick, maxTimeToClick;

    public EnemyXML() { }

    public EnemyXML(string characterName, float damage, float minTimeToClick, float maxTimeToClick, float health = 100) {
        this.characterName = characterName;
        this.damage = damage;
        this.minTimeToClick = minTimeToClick;
        this.maxTimeToClick = maxTimeToClick;
        this.health = health;
    }
}
