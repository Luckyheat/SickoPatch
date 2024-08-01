using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace SickoWarn
{
    [HarmonyPatch(typeof(ChatBubble))]
    public static class ChatBubblePatch
    {
        private static string[] Warn_words;

        static ChatBubblePatch()
        {
            LoadWarning_wordsFromFile("SickoPatch.Resources.Warningwords.txt"); // 
        }

        public static string ColorString(Color32 color, string str) => $"<size=100%>{str}</size>";

        [HarmonyPatch(nameof(ChatBubble.SetText)), HarmonyPrefix]
        public static void SetText_Prefix(ChatBubble __instance, ref string chatText)
        {
            var sr = __instance.transform.Find("Background").GetComponent<SpriteRenderer>();
            sr.color = new Color(255, 255, 255, 255);

            foreach (string word in Warn_words)
            {
                if (chatText.Contains(word))
                {
                    chatText = "<#F60><font=\"Barlow-Italic SDF\" material=\"Barlow-Italic SDF Outline\">[</color><#0O0>Sicko</color><#F00>Warn</color><#F60>]</font></material><#F00></color><#F00><#F00><font=\"Barlow-Bold SDF\" material=\"Barlow-Italic SDF Outline\"><b><cspace=+0.15>\n" + ColorString(Color.red, chatText.TrimEnd('\0'));
                    return; // Only process the first occurrence of a sensitive word
                }
            }

            chatText = ColorString(Color.black, chatText.TrimEnd('\0'));
        }

        private static void LoadWarning_wordsFromFile(string filename)
        {
            try
            {
                // Get the current assembly
                Assembly assembly = Assembly.GetExecutingAssembly();

                // Load the embedded resource file content
                using (Stream stream = assembly.GetManifestResourceStream(filename))
                {
                    if (stream != null)
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            Warn_words = reader.ReadToEnd().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                    }
                    else
                    {
                        Debug.LogError($"Failed to load sensitive words file from embedded resources: {filename}");
                        Warn_words = new string[0]; // Assign an empty array in case of failure
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading sensitive words file from embedded resources: {ex.Message}");
                Warn_words = new string[0]; // Handle exceptions by assigning an empty array
            }
        }
    }
}
