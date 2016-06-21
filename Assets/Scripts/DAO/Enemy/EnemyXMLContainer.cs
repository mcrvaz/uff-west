using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("EnemyCollection")]
public class EnemyXMLContainer : XMLContainer<EnemyXMLContainer, EnemyXML> {

    [XmlArray("Enemies")]
    [XmlArrayItem("Enemy")]
    public List<EnemyXML> enemies = new List<EnemyXML>();
    [XmlIgnore]
    public string path = Path.Combine(Application.dataPath + "/Data/Enemies", "enemies.xml");

    public EnemyXMLContainer() {
        this.Load();
    }

    public void Save() {
        base.Save(this.path);
    }

    public EnemyXMLContainer Load() {
        return base.Load(this.path);
    }

    public override void Push(EnemyXML enemy) {
        enemies.Add(enemy);
    }

    public override void Remove(EnemyXML enemy) {
        enemies.Remove(enemy);
    }

}
