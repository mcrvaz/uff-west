using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public abstract class Character {

    [XmlAttribute("characterName")]
    public string characterName;
    public string prefab;
    public float health;
    public float damage;
    

    [XmlArray("Dialogs")]
    [XmlArrayItem("Dialog")]
    public List<SpeechText> dialogs;

    public Character() { } //Needed for XMLSerializer

    public Character(string characterName, string prefab, float damage, List<SpeechText> dialogs, float health = 100) {
        this.characterName = characterName;
        this.prefab = prefab;
        this.damage = damage;
        this.dialogs = dialogs;
        this.health = health;
    }

}
