using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace Landoria.MapTeleport
{
    // Owns the plugin metadata and Harmony patch lifecycle.
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "Landoria.MapTeleport";
        public const string PluginName = "Landoria.MapTeleport";
        public const string PluginVersion = "1.0.0";
        internal static ConfigEntry<ModifierKey> TeleportKey { get; private set; }
        internal static ConfigEntry<int> MinimumTeleportDistance { get; private set; }
        private Harmony _harmony;

        // Installs the map click patch.
        private void Awake()
        {
            TeleportKey = Config.Bind("Controls", "TeleportKey", ModifierKey.LeftShift,
                "Modifier held while left-clicking the large map: LeftAlt, LeftCtrl, or LeftShift.");
            MinimumTeleportDistance = Config.Bind("Teleport", "MinimumDistance", 200,
                new ConfigDescription("Minimum map teleport distance in meters (100-1000).",
                    new AcceptableValueRange<int>(100, 1000)));
            ConfigWatcher.Initialize(Config, Logger, "MapTeleport", RestoreDefaults);
            _harmony = new Harmony(PluginGuid);
            _harmony.PatchAll();
            Logger.LogInfo($"{PluginName} {PluginVersion} loaded.");
        }

        // Reloads the saved configuration from Unity's main thread.
        private void Update()
        {
            ConfigWatcher.Update();
        }

        // Recreates the configuration with its defaults when its file is deleted.
        private void RestoreDefaults()
        {
            TeleportKey.Value = ModifierKey.LeftShift;
            MinimumTeleportDistance.Value = 200;
            Config.Save();
            ConfigWatcher.IgnoreCurrentFileVersion();
        }

        // Removes only this plugin's patches.
        private void OnDestroy()
        {
            ConfigWatcher.Dispose();
            _harmony?.UnpatchSelf();
            TeleportKey = null;
            MinimumTeleportDistance = null;
        }
    }
}
