using System.Collections.Generic;
using HarmonyLib;

namespace SickoPatch
{
	[HarmonyPatch(typeof(TextBoxTMP))]
	public class TextBoxPatch
	{
		[HarmonyPatch("SetText")]
		[HarmonyPrefix]
		public static bool ModifyCharacterLimit(TextBoxTMP __instance, [HarmonyArgument(0)] string input, [HarmonyArgument(1)] string inputCompo = "")
		{
			__instance.characterLimit = (AmongUsClient.Instance.AmHost ? 120 : 120);
			if (input.Length < 1)
			{
				return true;
			}
			int length = input.Length;
			int num = length - 1;
			string text = input.Substring(num, length - num);
			string newValue;
			if (TextBoxPatch.replaceDic.TryGetValue(text, out newValue))
			{
				__instance.SetText(input.Replace(text, newValue), "");
				return false;
			}
			return true;
		}

		private static Dictionary<string, string> replaceDic = new Dictionary<string, string>
		{
			{
				"（",
				"("
			},
			{
				"）",
				")"
			},
			{
				"，",
				","
			},
			{
				"：",
				":"
			},
			{
				"[",
				"【"
			},
			{
				"]",
				"】"
			},
			{
				"‘",
				"'"
			},
			{
				"’",
				"'"
			},
			{
				"“",
				"''"
			},
			{
				"”",
				"''"
			},
			{
				"！",
				"!"
            },
            {
                ">",
                "<"
            }
		};
	}
}
