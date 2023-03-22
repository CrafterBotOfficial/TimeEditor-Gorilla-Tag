using System.Net.Http;
using System.Xml.Serialization;
using UnityEngine;

namespace TimeEditor.Managers
{
    internal class BuildValid : MonoBehaviour
    {
        public static bool VersionValid;

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
            var RawData = Client.GetStreamAsync(Url);
            var Data = RawData.Result;

            XmlSerializer Xml = new XmlSerializer(typeof(BuildInfo));
            BuildInfo_Instance = (BuildInfo)Xml.Deserialize(Data);

            if (!VersionMissMatch(ModInfo.BuildId, BuildInfo_Instance.BuildId)) return;
            string BuildType = BuildInfo_Instance.BuildType;
            //if (BuildType == "Debug") return;

            bool Invalid = BuildInfo_Instance.BuildId > ModInfo.BuildId; // If internet version is greater then modinfo local build
            VersionValid = !Invalid;
        }

        private bool VersionMissMatch(int LoadedBuild, int OnlineBuild)
        {
            bool Valid = LoadedBuild == OnlineBuild;
            return Valid;
        }
    }

    public class BuildInfo
    {
        public string Description;

        public string Version;
        public int BuildId;
        public string BuildType;
    }
}
