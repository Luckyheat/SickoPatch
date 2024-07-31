using System.Collections.Generic;
using HarmonyLib;

namespace SickoPatch
{
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.SendChat))]
    class ChathistoryPatch
    {
        public static List<string> ChatHistory = new();

        public static bool Prefix(ChatController __instance)
        {
            // if it's a quick chat, it's a crosswalk.
            if (__instance.quickChatField.Visible)
            {
                return true;
            }
            // If nothing is written in the input field, block
            if (__instance.freeChatField.textArea.text == "")
            {
                return true;
            }
            __instance.timeSinceLastMessage = 3f;
            var text = __instance.freeChatField.textArea.text;
            if (ChatHistory.Count == 0 || ChatHistory[^1] != text) ChatHistory.Add(text);
            ChatControllerUpdatePatch.CurrentHistorySelection = ChatHistory.Count;
            string[] args = text.Split(' ');
            var canceled = false;
            
            if (canceled)
            {}
            return !canceled;
        }

        
            }
    }
    
    