using Epic.OnlineServices.Presence;
using HarmonyLib;
using InnerNet;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder;
namespace SickoPatch
{
    
    class C
    {
        public static bool CDS = false;
    }
    
    [HarmonyPatch(typeof(ControllerManager), nameof(ControllerManager.Update))]
    class ControllerManagerUpdatePatch
    {
        public static void Postfix()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && GameStates.IsLobby)
                PlayerControl.LocalPlayer.GetComponent<CircleCollider2D>().enabled = !PlayerControl.LocalPlayer.gameObject.GetComponent<CircleCollider2D>().enabled;

            // Force the chat box to be displayed [Everywhere]
            if (GetKeysDown(new[] { KeyCode.KeypadPlus}))
            {
                HudManager.Instance.Chat.SetVisible(true);
                SIGP.SIGD("<#F60><b><indent=-0.3>〖SickoPatch〗 <#F00>Chat button showed!");
                Log.Warning("Chat button showed!");
            }
            if (GetKeysDown(new[] { KeyCode.KeypadMinus }))
            {
                HudManager.Instance.Chat.SetVisible(false);
                SIGP.SIGD("<#F60><b><indent=-0.3>〖SickoPatch〗 <#F00>Chat button hidden!");
                Log.Warning("Chat button hidden!");
            }

            if (!AmongUsClient.Instance.AmHost) return;

            // Close the meeting [HOST]
            if (GetKeysDown(new[] { KeyCode.Slash }))
            {
                MeetingHud.Instance.RpcClose();
                Log.Warning("Meeting closed");
                SIGP.SIGD("<#F60><b><indent=-0.3>〖SickoPatch〗 <#F00>Meeting closed!");
            }

            // Reset start time [HOST]
            if (Input.GetKeyDown(KeyCode.R) && GameStartManager.InstanceExists && GameStartManager.Instance.startState == GameStartManager.StartingStates.Countdown)
            {
                GameStartManager.Instance.ResetStartState();
                Log.Warning("Reseted start time");
                SIGP.SIGD("<#F60><b><indent=-0.3>〖SickoPatch〗 <#F00>Start Countdown is forcibly stopped!");
            }
            
            // Force start the game [HOST]
            if (Input.GetKeyDown(KeyCode.LeftShift) && GameStartManager.InstanceExists && GameStartManager.Instance.startState == GameStartManager.StartingStates.Countdown)
            {
                GameStartManager.Instance.countDownTimer = 0f;
                Log.Warning("Changed start time to 0");
                SIGP.SIGD("<#F60><b><indent=-0.3>〖SickoPatch〗 <#F00>Game is forcibly started!");
            }
            if (Input.GetKeyDown(KeyCode.RightShift) && GameStartManager.InstanceExists && GameStartManager.Instance.startState == GameStartManager.StartingStates.Countdown)
            {
                GameStartManager.Instance.countDownTimer = 20f;
                Log.Warning("Changed start time to 20");
                SIGP.SIGD("<#F60><b><indent=-0.3>〖SickoPatch〗 <#F00>Increased time to start!");
            }
            if (Input.GetKeyDown(KeyCode.RightAlt) && GameStartManager.InstanceExists && GameStartManager.Instance.startState == GameStartManager.StartingStates.Countdown)
            {
                GameStartManager.Instance.countDownTimer = 99999f; //From IS file
                Log.Warning("Changed start time to 99999");
                SIGP.SIGD("<#F00><b><indent=-0.3>〖IS〗 POGGERS!");
            }


            if (Input.GetKeyDown(KeyCode.F1) && GameStates.IsOnlineGame)//Fuck Hudson everywhere!

            {
                SIGP.SIGM ("<#G0J><b><size=+1>＃ＦｕｃｋＨｕｄｓｏｎ</size>");
            }
            if (Input.GetKeyDown(KeyCode.F1) && GameStates.IsFreePlay) // Fuck Hudson for FreePlay

            {
                SIGP.SIGM("<#G0J><b><size=+1>＃ＦｕｃｋＨｕｄｓｏｎ</size>"); 
            }
            /* if (GetKeysDown(new[] { KeyCode.F2 }))

             {
                 SIGP.SIGL("#FuckHudson</size>"); -> WIP [Message in chat]
             }*/

            if (Input.GetKeyDown(KeyCode.Backslash) && GameStates.IsFreePlay)
            {
                HudManager.Instance.StartCoroutine(HudManager.Instance.CoFadeFullScreen(Color.clear, Color.black)); //Only for FreePlay [AU roles intro]
                HudManager.Instance.StartCoroutine(DestroyableSingleton<HudManager>.Instance.CoShowIntro());
            }
            
        }
        
        static bool GetKeysDown(KeyCode[] keys)
        {
            if (keys.Any(k => Input.GetKeyDown(k)) && keys.All(k => Input.GetKey(k)))
            {
                return true;
            }
            return false;
        }
        
    }
}
