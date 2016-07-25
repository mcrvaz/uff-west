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
        try {
            base.Save(this.path + ".xml");
        } catch (System.Exception e) {
            UnityEngine.Debug.LogError(e);
        }
    }

    public void Load() {
        var loaded = base.Load(this.path);
        state = loaded.state;
    }

}
