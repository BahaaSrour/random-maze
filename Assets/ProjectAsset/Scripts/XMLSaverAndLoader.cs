using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XMLSaverAndLoader : MonoBehaviour
{
    static Settings settings;
    static string xmlFilePath;
    private void Awake()
    {
        xmlFilePath = Application.dataPath + "Settings.xml";
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
            SaveToXML();
    }
    public void SaveToXML()
    {
        settings = BFS.GetSettings();
        if (File.Exists(xmlFilePath))
            File.Delete(xmlFilePath);
        var serializer = new XmlSerializer(typeof(Settings));
        FileStream xmlFile = File.Create(xmlFilePath);
        serializer.Serialize(xmlFile, settings);
        xmlFile.Close();

    }

    public static void LoadSettings(BFS bfsManager) 
    {
        LoadFromXML();
        bfsManager.SetSettings(settings);
    }
    
    public static void LoadFromXML()
    {
        Settings tempSettings = null;
        if (!File.Exists(xmlFilePath))
        {
            Debug.Log("Xml file not found.");
            tempSettings = null;
        }
        else 
        {
            var serializer = new XmlSerializer(typeof(Settings));
            var reader = new StreamReader(xmlFilePath);
            tempSettings = (Settings)serializer.Deserialize(reader);
            reader.Close();
        }
        if(tempSettings!=null)
        settings = tempSettings;
    }
}
[System.Serializable]
public class Settings
{
    public Vector2Int player;
    public Vector2Int goal;
    public int maxNumberOfBlock;

    public Settings() {}
}
