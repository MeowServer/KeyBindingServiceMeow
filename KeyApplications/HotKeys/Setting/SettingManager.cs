using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using KeyBindingServiceMeow.API.Features.HotKey;

namespace KeyBindingServiceMeow.KeyApplications.HotKeys.Setting
{
    internal class SettingManager
    {
        public static SettingManager Instance { get; private set;}

        public readonly Dictionary<string, List<HotKeySetting>> Settings = new Dictionary<string, List<HotKeySetting>>();

        private string _baseDirectory;

        public static void Initialize()
        {
            Instance = new SettingManager();

            string directory = Config.instance.HotKeySettingDirectory;

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            Instance._baseDirectory = directory;
        }

        public static void Destruct()
        {
            Instance = null;
        }

        private string GetFilePath(string userID)
        {
            return Path.Combine(_baseDirectory, $"{userID}.json");
        }

        private void SaveSettings(string userID)
        {
            var filePath = GetFilePath(userID);

            if (!Settings.ContainsKey(userID))
            {
                LoadSettings(userID); //Initialize the setting if no setting found
            }

            string json = JsonConvert.SerializeObject(Settings[userID], Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Try load the setting from the file, if no setting found, initialize the setting
        /// </summary>
        /// <param name="userID">The user id of the Player</param>
        /// <returns></returns>
        private List<HotKeySetting> LoadSettings(string userID)
        {
            string filePath = GetFilePath(userID);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Settings[userID] = JsonConvert.DeserializeObject<List<HotKeySetting>>(json);
            }
            else
            {
                Settings[userID] = new List<HotKeySetting>();//Initialize the setting if no setting found
                SaveSettings(userID);
            }

            return Settings[userID];
        }

        public IReadOnlyList<HotKeySetting> GetSettings(string userID)
        {
            if (!Settings.ContainsKey(userID))
            {
                LoadSettings(userID);
            }

            return Settings[userID].AsReadOnly();
        }

        public void ChangeSettings(string userID, HotKeySetting config)
        {
            if (!Settings.ContainsKey(userID))
            {
                LoadSettings(userID);
            }

            Settings[userID].RemoveAll(x => x.id == config.id);
            Settings[userID].Add(config);

            SaveSettings(userID);
        }
    }
}
