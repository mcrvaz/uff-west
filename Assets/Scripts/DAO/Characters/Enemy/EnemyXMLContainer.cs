using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("EnemyCollection")]
public class EnemyXMLContainer : XMLContainer<EnemyXMLContainer, EnemyXML> {

    [XmlArray("Enemies")]
    [XmlArrayItem("Enemy")]
    public List<EnemyXML> enemies = new List<EnemyXML>();
    [XmlIgnore]
    public string path;

    public EnemyXMLContainer() { } //Needed for XMLSerializer

    public EnemyXMLContainer(string fileName) {
        path = Path.Combine(base.basePath + "Characters/Enemies", fileName);
    }

    public void Save() {
        base.Save(this.path);
    }

    public void Load() {
        var loaded = base.Load(this.path);
        enemies = loaded.enemies;
    }

    public override void Push(EnemyXML enemy) {
        enemies.Add(enemy);
    }

    public override void Remove(EnemyXML enemy) {
        enemies.Remove(enemy);
    }

}
