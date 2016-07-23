using System.Xml;
using System.Xml.Serialization;

public class Contract {

    public string name, facePrefab, crime, reward;

    public Contract() { }

    public Contract(string name, string crime, string reward, string facePrefab) {
        this.name = name;
        this.crime = crime;
        this.reward = reward;
        this.facePrefab = facePrefab;
    }

}
