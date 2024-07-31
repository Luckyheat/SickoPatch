using AmongUs.Data;
using HarmonyLib;
using InnerNet;
using UnityEngine;

namespace SickoPatch
{
	[HarmonyPatch(typeof(ChatController), "Update")]
	internal class ChatControllerUpdatePatch
	{

		public static void Postfix(ChatController __instance)
		{
			bool flag = !__instance.freeChatField.textArea.hasFocus;
			if (!flag)
			{
				bool flag2 = (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.F3)) && Input.GetKeyDown(KeyCode.C);
				if (flag2)
				{
					ClipboardHelper.PutClipboardString(__instance.freeChatField.textArea.text);
				}
				bool flag3 = (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.F2)) && Input.GetKeyDown(KeyCode.V);
				if (flag3)
				{
					__instance.freeChatField.textArea.SetText(__instance.freeChatField.textArea.text + GUIUtility.systemCopyBuffer, "");
				}
				bool flag4 = (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.F1)) && Input.GetKeyDown(KeyCode.X);
				if (flag4)
				{
					ClipboardHelper.PutClipboardString(__instance.freeChatField.textArea.text);
					__instance.freeChatField.textArea.SetText("", "");
				}
			}
		}

		public static int CurrentHistorySelection = -1;
	}
}
