using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("DuelCollection")]
public class DuelXMLContainer : XMLContainer<DuelXMLContainer, Duel> {

    [XmlArray("Duels")]
    [XmlArrayItem("Duel")]
    public List<Duel> duels = new List<Duel>();
    [XmlIgnore]
    public string path;

    public DuelXMLContainer() { } //Needed for XMLSerializer

    public DuelXMLContainer(string fileName) {
        path = Path.Combine(base.basePath, "Duels");
        path = Path.Combine(path, fileName);
    }

    public void Save() {
        base.Save(this.path);
    }

    public void Load() {
        var loaded = base.Load(this.path);
        duels = loaded.duels;
    }

    public override void Push(Duel duel) {
        duels.Add(duel);
    }

    public override void Remove(Duel duel) {
        duels.Remove(duel);
    }

}
