using AmongUs.QuickChat;
using HarmonyLib;
using UnityEngine;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Configuration;
using Rewired.Utils.Platforms.Windows;
using AmongUs.Data;
using InnerNet;

[BepInPlugin("SickoPatch", "SickoPatch", "1.0.0")]
public class Plugin : BasePlugin
{
    public Harmony Harmony { get; } = new Harmony("SickoPatch, by Luckyheat.\nOptimized source code [DarkMode] by Techiee.");
    public static ConfigEntry<bool> DarkModeConfig;

    public override void Load()
    {
        DarkModeConfig = Config.Bind("SickoChatPatch",
                                     "SickoChatPatch",
                                     true,
                                     "Disclaimer: This is optimized source code of DarkMode\nSet this to false if you don't want dark UI for now.\nCredits to Techiee | Dark Mode");
        Harmony.PatchAll();
    }

    [HarmonyPatch(typeof(ChatBubble))]
    public static class ChatBubblePatch
    {
        public static string ColorString(Color32 color, string str) => $"<color=#{color.r:x2}{color.g:x2}{color.b:x2}{color.a:x2}><font=\"Barlow-Bold SDF\" material=\"Barlow-Italic SDF Outline\"><cspace=+0.15><b>{str}</font></color>";

        [HarmonyPatch(nameof(ChatBubble.SetText)), HarmonyPrefix]
        public static void SetText_Prefix(ChatBubble __instance, ref string chatText)
        {
            var sr = __instance.transform.Find("Background").GetComponent<SpriteRenderer>();
            if (Plugin.DarkModeConfig.Value) sr.color = new Color(0, 0, 0, 64);

            if (chatText.Contains("░") ||
                chatText.Contains("▄") ||
                chatText.Contains("█") ||
                chatText.Contains("▌") ||
                chatText.Contains("▒")) ;
            else
            {
                if (Plugin.DarkModeConfig.Value) chatText = ColorString(Color.white, chatText.TrimEnd('\0')); // The color of the output text
            }
        }
    }
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.Update))]
    class ChatControllerUpdatePatch
    {
        public static int CurrentHistorySelection = -1;

        private static SpriteRenderer QuickChatIcon;
        private static SpriteRenderer OpenBanMenuIcon;
        private static SpriteRenderer OpenKeyboardIcon;

        public static void Prefix()
        {
            if (AmongUsClient.Instance.AmHost && DataManager.Settings.Multiplayer.ChatMode == QuickChatModes.QuickChatOnly)
                DataManager.Settings.Multiplayer.ChatMode = QuickChatModes.FreeChatOrQuickChat;
        }

        public static void Postfix(ChatController __instance)
        {
            if (Plugin.DarkModeConfig.Value)
            {
                __instance.freeChatField.background.color = new Color32(30, 30, 30, byte.MaxValue); //Bg chat text-outpit [free chat]
                __instance.freeChatField.textArea.outputText.color = Color.white; // Output colored text in area

                __instance.quickChatField.background.color = new Color32(30, 30, 30, byte.MaxValue); //Bg chat text-outpit [Quick chat]
                __instance.quickChatField.text.color = Color.white; //Output color in quick chat

                if (QuickChatIcon == null) QuickChatIcon = GameObject.Find("QuickChatIcon")?.transform.GetComponent<SpriteRenderer>();
                else QuickChatIcon.sprite = SickoPatch.Modules.Utils.LoadSprite("SickoPatch.ImageResource.DarkQuickChat.png", 100f); //Dark quick chat icon

                if (OpenBanMenuIcon == null) OpenBanMenuIcon = GameObject.Find("OpenBanMenuIcon")?.transform.GetComponent<SpriteRenderer>();
                else OpenBanMenuIcon.sprite = SickoPatch.Modules.Utils.LoadSprite("SickoPatch.ImageResource.DarkReport.png", 100f); //Dark ban icon

                if (OpenKeyboardIcon == null) OpenKeyboardIcon = GameObject.Find("OpenKeyboardIcon")?.transform.GetComponent<SpriteRenderer>();
                else OpenKeyboardIcon.sprite = SickoPatch.Modules.Utils.LoadSprite("SickoPatch.ImageResource.DarkKeyboard.png", 100f); //Dark keyboard icon
            }
            else
            {
                __instance.freeChatField.textArea.outputText.color = Color.white;
            }

            if (!__instance.freeChatField.textArea.hasFocus) return;
            __instance.freeChatField.textArea.characterLimit = AmongUsClient.Instance.AmHost ? 120 : 120; //Your character limit

            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.C))
                ClipboardHelper.PutClipboardString(__instance.freeChatField.textArea.text);
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.V))
                __instance.freeChatField.textArea.SetText(__instance.freeChatField.textArea.text + GUIUtility.systemCopyBuffer);
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.X))
            {
                ClipboardHelper.PutClipboardString(__instance.freeChatField.textArea.text);
                __instance.freeChatField.textArea.SetText("");
            }
        }
    }
}
