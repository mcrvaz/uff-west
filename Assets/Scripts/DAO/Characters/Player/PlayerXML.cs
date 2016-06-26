using System.Xml;
using System.Xml.Serialization;

public class PlayerXML : CharacterXML {

    public PlayerXML() { }

    public PlayerXML(string characterName, float damage, float health = 100) {
        this.characterName = characterName;
        this.damage = damage;
        this.health = health;
    }
}
