using System.Xml;
using System.Xml.Serialization;

public class Player : Character {

    public Player() { }

    public Player(string characterName, float damage, string prefab, float health = 100) {
        this.characterName = characterName;
        this.damage = damage;
        this.prefab = prefab;
        this.health = health;
    }
}
