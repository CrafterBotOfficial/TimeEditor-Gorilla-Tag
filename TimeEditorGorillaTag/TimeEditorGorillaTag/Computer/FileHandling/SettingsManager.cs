using ExitGames.Client.Photon.StructWrapping;
using Photon.Voice;
using PlayFab.ClientModels;
using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace TimeEditorGorillaTag.Computer.FileHandling
{

    [Serializable]
    public class Settings
    {
        public bool ModEnabled;
        public int Preset;
        public float Custom;
    }
    internal static class SettingsManager
    {
        //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/how-to-write-object-data-to-an-xml-file
        //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/how-to-read-object-data-from-an-xml-file
        private static string Path = Application.dataPath + "/timeeditorsavedata.xml";
        public static Settings BuilderSettings = new Settings();
        public static void Load()
        {
            if (!File.Exists(Path)) Save();

            XmlSerializer Reader = new XmlSerializer(typeof(Settings));
            StreamReader StreamReader = new StreamReader(Path);
            BuilderSettings = (Settings)Reader.Deserialize(StreamReader);
            
            StreamReader.Close();
        }

        public static void Save()
        {
            Debug.Log("Saving Data");
            XmlSerializer writer = new XmlSerializer(typeof(Settings));
            FileStream file = File.Create(Path);

            writer.Serialize(file, BuilderSettings);
            file.Close();
        }
    }
}
