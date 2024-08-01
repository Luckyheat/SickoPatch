using System;
using System.Collections.Generic;
using System.Globalization;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;

namespace SickoPatch;

[BepInProcess("Among Us.exe")]
public class Main : BasePlugin
{

    public static readonly string ModName = "SickoPatch"; // Module Name
    public const string PluginVersion = "1.0.0"; //Module Version
    public static BepInEx.Logging.ManualLogSource Logger;
    
    public static Main Instance; //Set up the Main instance
    public static ConfigEntry<string> BetaBuildURL { get; private set; }
    public static Version version = Version.Parse(PluginVersion);
    public override void Load()
    {
        Instance = this; 

        BetaBuildURL = Config.Bind("Other", "BetaBuildURL", "");
        
        Logger = BepInEx.Logging.Logger.CreateLogSource("SickoPatch"); //Output prefix

        Logger.LogInfo($"SickoPatch loaded.");
    }
    
    public static readonly string ForkId = "SickoPatch";
    public static Dictionary<byte, PlayerVersion> playerVersion = new();
    


}

public class PlayerVersion
{
    public readonly Version version;
    public readonly string tag;
    public readonly string forkId;
    public PlayerVersion(string ver, string tag_str, string forkId) : this(Version.Parse(ver), tag_str, forkId) { }
    public PlayerVersion(Version ver, string tag_str, string forkId)
    {
        version = ver;
        tag = tag_str;
        this.forkId = forkId;
    }
}
