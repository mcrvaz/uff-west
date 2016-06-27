using System.Xml;
using System.Xml.Serialization;

public class Player : Character {

    public Player() { }

    public Player(string characterName, float damage, float health = 100) {
        this.characterName = characterName;
        this.damage = damage;
        this.health = health;
    }
}
