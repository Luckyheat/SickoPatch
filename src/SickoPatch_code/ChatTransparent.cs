using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace SickoPatch
{
    [HarmonyPatch(typeof(ChatBubble))]
    public static class ChatBGColorPatch
    {
        public static string ColorString(Color32 color, string str) => $"<size=100%><font=\"Barlow-Bold SDF\" material=\"Barlow-Italic SDF Outline\"><b><cspace=+0.15>{str}</font></material></size></b></cspace>";
        
        public static void SetText_Prefix(ChatBubble __instance, ref string chatText)
        {
            var sr = __instance.transform.Find("Background").GetComponent<SpriteRenderer>();
            sr.color = new Color(255, 255, 255, 255);

            chatText = ColorString(Color.black, chatText.TrimEnd('\0'));
        }

    }
}
