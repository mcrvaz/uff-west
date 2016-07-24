using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("GameStates")]
public class GameStateXMLContainer : XMLContainer<GameStateXMLContainer, GameState> {

    public GameState state = new GameState();

    [XmlIgnore]
    public string path;

    public GameStateXMLContainer() { } //Needed for XMLSerializer

    public GameStateXMLContainer(string fileName) {
        path = Path.Combine(base.basePath, "SaveGame");
        path = Path.Combine(path, fileName);
    }

    public void Save() {
        base.Save(this.path + ".xml");
    }

    public void Load() {
        var loaded = base.Load(this.path);
        state = loaded.state;
    }

}
