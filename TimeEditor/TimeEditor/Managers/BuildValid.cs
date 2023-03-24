using System;
using System.Net.Http;
using System.Xml.Serialization;
using UnityEngine;
using Valve.VR;

namespace TimeEditor.Managers
{
    internal class BuildValid : MonoBehaviour
    {
        public static bool VersionValid;
        public static string Version;
        public static string VersionDescription;

        private HttpClient Client;
        private string Url;
        private BuildInfo BuildInfo_Instance;

        public BuildValid()
        {
            Client = new HttpClient();
            Url = "https://raw.githubusercontent.com/CrafterBotOfficial/TimeEditor-Gorilla-Tag/main/modinfo.xml";
        }

        private void Awake()
        {
            return;
            var RawData = Client.GetStreamAsync(Url);
            var Data = RawData.Result;

            XmlSerializer Xml = new XmlSerializer(typeof(BuildInfo));
            BuildInfo_Instance = (BuildInfo)Xml.Deserialize(Data);

            //string BuildType = BuildInfo_Instance.BuildType;
            //if (BuildType == "Debug") return;

            bool Invalid = BuildInfo_Instance.BuildId > ModInfo.BuildId; // If internet version is greater then modinfo local build
            VersionValid = !Invalid;

            if (Invalid)
            {
                Version = BuildInfo_Instance.Version;
                VersionDescription = BuildInfo_Instance.Description;

                Debug.LogWarning("Your version of Time Editor is outdated. Please update to the latest version.");
            }
        }
    }

    [Serializable]
    public class BuildInfo
    {
        public string Description;

        public string Version;
        public int BuildId;
        public string BuildType;
    }
}
