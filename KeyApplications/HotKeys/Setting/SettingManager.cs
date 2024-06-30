using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg;
using KeyBindingServiceMeow.API.Features.HotKey;

namespace KeyBindingServiceMeow.KeyApplications.HotKeys.Setting
{
    internal class SettingManager
    {
        public static SettingManager instance { get; private set;}

        public readonly Dictionary<string, List<HotKeySetting>> settings = new Dictionary<string, List<HotKeySetting>>();

        private string baseDirectory;

        public static void Initialize()
        {
            instance = new SettingManager();

            string directory = Config.instance.HotKeySettingDirectory;

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            instance.baseDirectory = directory;
        }

        public static void Destruct()
        {
            instance = null;
        }

        private string GetFilePath(string userID)
        {
            return Path.Combine(baseDirectory, $"{userID}.json");
        }

        private void SaveSettings(string userID)
        {
            var filePath = GetFilePath(userID);

            if (!settings.ContainsKey(userID))
            {
                LoadSettings(userID); //Initialize the setting if no setting found
            }

            string json = JsonConvert.SerializeObject(settings[userID], Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Try load the setting from the file, if no setting found, initialize the setting
        /// </summary>
        /// <param name="userID">The user id of the player</param>
        /// <returns></returns>
        private List<HotKeySetting> LoadSettings(string userID)
        {
            string filePath = GetFilePath(userID);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                settings[userID] = JsonConvert.DeserializeObject<List<HotKeySetting>>(json);
            }
            else
            {
                settings[userID] = new List<HotKeySetting>();//Initialize the setting if no setting found
                SaveSettings(userID);
            }

            return settings[userID];
        }

        public IReadOnlyList<HotKeySetting> GetSettings(string userID)
        {
            if (!settings.ContainsKey(userID))
            {
                LoadSettings(userID);
            }

            return settings[userID].AsReadOnly();
        }

        public void ChangeSettings(string userID, HotKeySetting config)
        {
            if (!settings.ContainsKey(userID))
            {
                LoadSettings(userID);
            }

            settings[userID].RemoveAll(x => x.id == config.id);
            settings[userID].Add(config);

            SaveSettings(userID);
        }
    }
}
