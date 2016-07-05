using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class StatisticsXMLContainer : XMLContainer<StatisticsXMLContainer, Statistics> {
    public List<Statistics> statistics = new List<Statistics>();
    [XmlIgnore]
    public string path;

    public StatisticsXMLContainer() { } //Needed for XMLSerializer

    public StatisticsXMLContainer(string fileName) {
        path = Path.Combine(base.basePath, "Statistics");
        path = Path.Combine(path, fileName);
    }

    public void Save() {
        base.Save(this.path);
    }

    public void Load() {
        var loaded = base.Load(this.path);
        statistics = loaded.statistics;
    }

    public override void Push(Statistics statistic) {
        statistics.Add(statistic);
    }

    public override void Remove(Statistics statistic) {
        statistics.Remove(statistic);
    }


}
