using System.Xml;
using System.Xml.Serialization;

public abstract class CharacterXML {

    [XmlAttribute("characterName")]
    public string characterName;
    public float health;
    public float damage;

    public CharacterXML() { }

    public CharacterXML(string characterName, float damage, float health = 100) {
        this.characterName = characterName;
        this.damage = damage;
        this.health = health;
    }

}
