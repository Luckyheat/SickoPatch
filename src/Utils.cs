using System;
using System.Collections.Generic;
using System.Linq;
using AmongUs.GameOptions;
using Sentry.Internal.Extensions;
using UnityEngine;
using static InnerNet.InnerNetClient;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using Il2CppInterop.Runtime.Injection;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace SickoPatch;

public class Utils
{
    public static string GetPing(int ping)
    {

        return $"\n<color=#FF6000>Ping: {ping} ms</color>";



    }


    public bool canstartgame = true;
    public static string ColorString(Color32 color, string str) => $"";

}
