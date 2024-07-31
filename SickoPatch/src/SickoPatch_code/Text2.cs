
using System.Runtime.CompilerServices;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace SickoPatch
{
	[HarmonyPatch(typeof(FreeChatInputField), "UpdateCharCount")]
	internal class UpdateCharCountPatch
	{
		public static void Postfix(FreeChatInputField __instance)
		{
			int length = __instance.textArea.text.Length;
			TMP_Text charCountText = __instance.charCountText;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<int>(length);
			defaultInterpolatedStringHandler.AppendLiteral("/");
			defaultInterpolatedStringHandler.AppendFormatted<int>(__instance.textArea.characterLimit);
			charCountText.SetText(defaultInterpolatedStringHandler.ToStringAndClear(), true);
			if (length < (AmongUsClient.Instance.AmHost ? 120 : 120))
			{
				__instance.charCountText.color = Color.blue;
				return;
			}
			if (length < (AmongUsClient.Instance.AmHost ? 120 : 120))
			{
				__instance.charCountText.color = new Color(1f, 1f, 0f, 1f);
				return;
			}
			__instance.charCountText.color = Color.blue;
		}
	}
}
