using System.Threading.Tasks;
using AmongUs.GameOptions;
using HarmonyLib;
using InnerNet;
using UnityEngine;

namespace SickoPatch
{
    [HarmonyPatch]
    public static class Options
    {
        //Main settings
        public static OptionItem NoGameEnd;
        public static OptionItem GameTime;
        
        public static OptionItem DisableMeetings;
        
        public static OptionItem EnableMidGameChat;
        
    }  
}