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

    protected string basePath = "XML";

    protected void Save(string path) {
        var serializer = new XmlSerializer(typeof(T));
        var combinedPath = Path.Combine(Application.dataPath, "Resources");
        combinedPath = Path.Combine(combinedPath, path);
        Stream stream = new FileStream(combinedPath, FileMode.Create, FileAccess.Write);
        serializer.Serialize(stream, this);
        stream.Close();
    }

    protected T Load(string path) {
        var serializer = new XmlSerializer(typeof(T));
        Stream reader = new MemoryStream((Resources.Load(path, typeof(TextAsset)) as TextAsset).bytes);
        StreamReader textReader = new StreamReader(reader);
        T result = serializer.Deserialize(textReader) as T;
        reader.Dispose();
        return result;
    }

    public abstract void Push(P obj);
    public abstract void Remove(P obj);

}
