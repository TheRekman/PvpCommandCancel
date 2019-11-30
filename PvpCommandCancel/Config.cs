using System;
using System.IO;
using Newtonsoft.Json;
using TShockAPI;

namespace PvpCommandCancel
{
    class Config
    {
        public static readonly string ConfigPath = Path.Combine(TShockAPI.TShock.SavePath, "PvpCmdCancel.json");

        public string[] UnallowedCommands =
            new string[]
            {
                "godmode",
                "heal",
                "buff",
                "gbuff",
                "tp",
                "warp"
            };

        public static Config Read()
        {
            if (!File.Exists(ConfigPath))
                return Create();
            try
            {
                string jsonString = File.ReadAllText(ConfigPath);
                var conf = JsonConvert.DeserializeObject<Config>(jsonString);
                return conf;
            }
            catch (Exception e)
            {
                TShock.Log.ConsoleError("[PvpCommandCancel] Failed read config file.");
                TShock.Log.ConsoleError(e.Message);
                return new Config();
            }
        }
        public static Config Create()
        {
            try
            {
                var conf = new Config();
                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(conf, Formatting.Indented));
                TShock.Log.Info("[PvpCommandCancel] New config file created.");
                return conf;
            }
            catch
            {
                TShock.Log.ConsoleError("[PvpCommandCancel] Failed create config file.");
                throw;
            }
        }
        public void Save() =>
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(this, Formatting.Indented));
        public void WriteUnallowedCommand(string cmdName)
        {
            Array.Resize(ref UnallowedCommands, UnallowedCommands.Length + 1);
            UnallowedCommands[UnallowedCommands.Length - 1] = cmdName;
            Save();
        }
    }
}
