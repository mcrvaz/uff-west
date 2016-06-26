using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("PlayerCollection")]
public class PlayerXMLContainer : XMLContainer<PlayerXMLContainer, PlayerXML> {

    [XmlArray("Players")]
    [XmlArrayItem("Player")]
    public List<PlayerXML> players = new List<PlayerXML>();
    [XmlIgnore]
    public string path;

    public PlayerXMLContainer() { } //Needed for XMLSerializer

    public PlayerXMLContainer(string fileName) {
        path = Path.Combine(base.basePath + "Characters/Players", fileName);
    }

    public void Save() {
        base.Save(this.path);
    }

    public void Load() {
        var loaded = base.Load(this.path);
        players = loaded.players;
    }

    public override void Push(PlayerXML player) {
        players.Add(player);
    }

    public override void Remove(PlayerXML player) {
        players.Remove(player);
    }
}
