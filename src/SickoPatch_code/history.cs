using AmongUs.Data;
using HarmonyLib;
using UnityEngine;

namespace SickoPatch
{
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.Update))]
    class CH
    {
        public static int CHS = -1;
        public static void Prefix()
        {
            if (AmongUsClient.Instance.AmHost && DataManager.Settings.Multiplayer.ChatMode == InnerNet.QuickChatModes.QuickChatOnly)
                DataManager.Settings.Multiplayer.ChatMode = InnerNet.QuickChatModes.FreeChatOrQuickChat; //Open free chat at all times only host to hit the command
        }
        public static void Postfix(ChatController __instance)
        {
            
            if (Input.GetKeyDown(KeyCode.UpArrow) && ChathistoryPatch.ChatHistory.Count > 0)
            {
                CHS = Mathf.Clamp(--CHS, 0, ChathistoryPatch.ChatHistory.Count - 1);
                __instance.freeChatField.textArea.SetText(ChathistoryPatch.ChatHistory[CHS]);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && ChathistoryPatch.ChatHistory.Count > 0)
            {
                CHS++;
                if (CHS < ChathistoryPatch.ChatHistory.Count)
                    __instance.freeChatField.textArea.SetText(ChathistoryPatch.ChatHistory[CHS]);
                else __instance.freeChatField.textArea.SetText("");
            }
        }
    }
}