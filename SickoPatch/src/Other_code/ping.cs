using HarmonyLib;
using UnityEngine;
using TMPro;

namespace SickoPatch;

[HarmonyPriority(Priority.Low)]
[HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
public static class PingTracker_Update
{
    private static float deltaTime;
    
    [HarmonyPostfix]
    public static void Postfix(PingTracker __instance)
    {
        var offset_x = 3.1f; //Offset from the right edge [X]
        var offset_y = 6f; //Offset from the right edge [Y]
        if (HudManager.InstanceExists && HudManager._instance.Chat.chatButton.gameObject.active) offset_x -= 0.8f; //If there is a chat button, there is an additional offset
        __instance.GetComponent<AspectPosition>().DistanceFromEdge = new Vector3(offset_x, offset_y, 0f);

        __instance.text.text = __instance.ToString();
        __instance.text.alignment = TextAlignmentOptions.TopRight;
        __instance.text.text =
            $"<font=\"Barlow-Italic SDF\" material=\"Barlow-Italic SDF Outline\"><color=#FF6000>{Main.ModName} </color><color=#F60> v1.0.0 Pre_Build</color>";

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = Mathf.Ceil(1.0f / deltaTime);
     __instance.text.text +="\n<color=#FF0000>By Luckyheat</color>";
        __instance.text.text += Utils.GetPing(AmongUsClient.Instance.Ping);
    }
}