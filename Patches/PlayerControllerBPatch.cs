using UnityEngine;
using GameNetcodeStuff;
using HarmonyLib;

namespace NaturalProgression.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        static float sprintDuration = 0f;
        static float sprintXP = 0f;

        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void getNightVision(ref PlayerControllerB __instance)
        {
            NaturalProgressionMod.playerRef = __instance;
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void UpdatePatch()
        {
            if (NaturalProgressionMod.playerRef.isSprinting)
            {
                sprintDuration += Time.deltaTime;
                NaturalProgressionMod.mls.LogInfo("starting sprint timer");
            } else
            {
                if (sprintStart > 0f)
                {
                    NaturalProgressionMod.mls.LogInfo("done sprinting");
                    sprintXP += ((Time.deltaTime * 1000) - sprintStart);

                    NaturalProgressionMod.mls.LogInfo($"{Time.deltaTime.ToString()}");
                    NaturalProgressionMod.mls.LogInfo($"{sprintStart.ToString()}");
                    NaturalProgressionMod.mls.LogInfo($"{(Time.deltaTime - sprintStart).ToString()}");

                    sprintStart = 0f;

                    NaturalProgressionMod.mls.LogInfo($"sprintXP: {sprintXP.ToString()}");
                }
            }
        }
    }
}