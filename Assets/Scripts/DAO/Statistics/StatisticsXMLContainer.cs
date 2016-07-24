using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Statistics")]
public class StatisticsXMLContainer : XMLContainer<StatisticsXMLContainer, Statistics> {

    public Statistics stats = new Statistics();

    [XmlIgnore]
    public string path;

    public StatisticsXMLContainer() { } //Needed for XMLSerializer

    public StatisticsXMLContainer(string fileName) {
        path = Path.Combine(base.basePath, "Statistics");
        path = Path.Combine(path, fileName);
    }

    public void Save() {
        base.Save(this.path + ".xml");
    }

    public void Load() {
        var loaded = base.Load(this.path);
        stats = loaded.stats;
    }

}
