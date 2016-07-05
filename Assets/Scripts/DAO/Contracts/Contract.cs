using System.Xml;
using System.Xml.Serialization;

public class Contract {

    public string name, crime, reward;

    public Contract() { }

    public Contract(string name, string crime, string reward) {
        this.name = name;
        this.crime = crime;
        this.reward = reward;
    }

}
