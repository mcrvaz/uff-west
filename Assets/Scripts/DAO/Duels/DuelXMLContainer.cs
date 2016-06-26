using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("DuelCollection")]
public class DuelXMLContainer : XMLContainer<DuelXMLContainer, DuelXML> {

    [XmlArray("Duels")]
    [XmlArrayItem("Duel")]
    public List<DuelXML> duels = new List<DuelXML>();
    [XmlIgnore]
    public string path;

    public DuelXMLContainer() { } //Needed for XMLSerializer

    public DuelXMLContainer(string fileName) {
        path = Path.Combine(base.basePath + "Duels/", fileName);
    }

    public void Save() {
        base.Save(this.path);
    }

    public void Load() {
        var loaded = base.Load(this.path);
        duels = loaded.duels;
    }

    public override void Push(DuelXML duel) {
        duels.Add(duel);
    }

    public override void Remove(DuelXML duel) {
        duels.Remove(duel);
    }

}
