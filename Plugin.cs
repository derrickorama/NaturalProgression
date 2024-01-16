using BepInEx;
using HarmonyLib;
using BepInEx.Logging;
using NaturalProgression.Patches;
using GameNetcodeStuff;

namespace NaturalProgression
{
    [BepInPlugin(modGUID, modName, modVersion)]
    internal class NaturalProgressionMod : BaseUnityPlugin
    {
        private const string modAuthor = "derrickorama";
        private const string modGUID = "derrickorama.NaturalProgression";
        private const string modName = "Natural Progression";
        private const string modVersion = "0.1.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        public static ManualLogSource mls;
        internal static NaturalProgressionMod Instance { get; private set; }

        internal static PlayerControllerB playerRef;

        private void Awake()
        {
            Instance = this;
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Patching");

            harmony.PatchAll(typeof(NaturalProgressionMod));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
        }
    }
}