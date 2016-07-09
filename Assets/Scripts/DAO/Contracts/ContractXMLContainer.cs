using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ContractCollection")]
public class ContractXMLContainer : XMLContainer<ContractXMLContainer, Contract> {
    [XmlArray("Contracts")]
    [XmlArrayItem("Contract")]
    public List<Contract> contracts = new List<Contract>();
    [XmlIgnore]
    public string path;

    public ContractXMLContainer() { } //Needed for XMLSerializer

    public ContractXMLContainer(string fileName) {
        path = Path.Combine(base.basePath, "Contracts");
        path = Path.Combine(path, fileName);
    }

    public void Save() {
        base.Save(this.path);
    }

    public void Load() {
        var loaded = base.Load(this.path);
        contracts = loaded.contracts;
    }

    public void Push(Contract contract) {
        contracts.Add(contract);
    }

    public void Remove(Contract contract) {
        contracts.Remove(contract);
    }
}
