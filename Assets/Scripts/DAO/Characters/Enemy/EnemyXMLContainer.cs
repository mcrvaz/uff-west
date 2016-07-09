using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("EnemyCollection")]
public class EnemyXMLContainer : XMLContainer<EnemyXMLContainer, Enemy> {

    [XmlArray("Enemies")]
    [XmlArrayItem("Enemy")]
    public List<Enemy> enemies = new List<Enemy>();
    [XmlIgnore]
    public string path;

    public EnemyXMLContainer() { } //Needed for XMLSerializer

    public EnemyXMLContainer(string fileName) {
        path = Path.Combine(base.basePath, "Characters");
        path = Path.Combine(path, "Enemies");
        path = Path.Combine(path, fileName);
    }

    public void Save() {
        base.Save(this.path);
    }

    public void Load() {
        var loaded = base.Load(this.path);
        enemies = loaded.enemies;
    }

    public void Push(Enemy enemy) {
        enemies.Add(enemy);
    }

    public void Remove(Enemy enemy) {
        enemies.Remove(enemy);
    }

}
