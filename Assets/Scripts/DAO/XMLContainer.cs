using System.Xml.Serialization;
using System.IO;
using UnityEngine;

/// <summary>
/// Generic XML Container
/// </summary>
/// <typeparam name="T">The container class</typeparam>
/// <typeparam name="P">The object contained in T</typeparam>
public abstract class XMLContainer<T, P> where T : class
                                         where P : class {

    protected string basePath = Application.dataPath + "/Data/";

    protected void Save(string path) {
        var serializer = new XmlSerializer(typeof(T));
        using (var stream = new FileStream(path, FileMode.Create)) {
            serializer.Serialize(stream, this);
        }
    }

    protected T Load(string path) {
        var serializer = new XmlSerializer(typeof(T));
        using (var stream = new FileStream(path, FileMode.Open)) {
            return serializer.Deserialize(stream) as T;
        }
    }

    public abstract void Push(P obj);
    public abstract void Remove(P obj);

}
