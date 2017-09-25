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

    protected void Save(string fileName) {
        return;
//         var serializer = new XmlSerializer(typeof(T));
// #if UNITY_EDITOR
//         var combinedPath = Path.Combine(Application.dataPath, "Resources");
// #elif UNITY_ANDROID
//         var combinedPath = Path.Combine(Application.persistentDataPath, "Resources");
// #endif

//         combinedPath = Path.Combine(combinedPath, fileName);
//         if (!Directory.Exists(combinedPath)) {
//             Directory.CreateDirectory(combinedPath);
//         }
//         if (!File.Exists(combinedPath)) {
//             TextAsset xmlAsset = Resources.Load<TextAsset>(fileName);
//             File.WriteAllText(combinedPath + ".xml", xmlAsset.text);
//         }

        //Stream stream = new FileStream(combinedPath, FileMode.Create, FileAccess.Write);
        //serializer.Serialize(stream, this);
        //stream.Close();
    }

    protected T Load(string path) {
        var serializer = new XmlSerializer(typeof(T));
        Stream reader = new MemoryStream((Resources.Load(path, typeof(TextAsset)) as TextAsset).bytes);
        StreamReader textReader = new StreamReader(reader);
        T result = serializer.Deserialize(textReader) as T;
        reader.Dispose();
        return result;
    }

    protected T LoadFromPersistentData(string path) {
        var serializer = new XmlSerializer(typeof(T));
        var combinedPath = Path.Combine(Application.persistentDataPath, "Resources");
        combinedPath = Path.Combine(combinedPath, path + ".xml");
        using (var stream = new FileStream(combinedPath, FileMode.Open)) {
            return serializer.Deserialize(stream) as T;
        }
    }

}
