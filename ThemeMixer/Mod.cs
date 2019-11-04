using System;
using System.Reflection;
using ColossalFramework.Plugins;
using Harmony;
using ICities;
using JetBrains.Annotations;
using ThemeMixer.Locale;
using ThemeMixer.Patching;
using ThemeMixer.Resources;
using ThemeMixer.Serialization;
using ThemeMixer.Themes;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI;
using UnityEngine;

namespace ThemeMixer
{
    public class Mod : IUserMod, ILoadingExtension
    {
        public string Name => "Theme Mixer 2";

        public string Description => Translation.Instance.GetTranslation(TranslationID.MOD_DESCRIPTION);

        public static bool InGame => (ToolManager.instance.m_properties.m_mode == ItemClass.Availability.Game);

        public static bool ThemeDecalsEnabled => IsModEnabled(895061550UL, "Theme Decals");

        private static HarmonyInstance Harmony { get; set; }

        private static UltimateEyeCandyPatch UltimateEyeCandyPatch { get; set; }

        [UsedImplicitly]
        public void OnEnabled() {
            EnsureManagers();
            ManagersOnEnabled();
            InstallHarmony();
        }

        [UsedImplicitly]
        public void OnDisabled() {
            ReleaseManagers();
            UnInstallHarmony();
        }

        public void OnCreated(ILoading loading) { }

        public void OnReleased() { }

        public void OnLevelLoaded(LoadMode mode) {
            ThemeSprites.CreateAtlas();
            ManagersOnLevelLoaded();
        }

        public void OnLevelUnloading() {
            ManagersOnLevelUnloaded();
        }

        private static void EnsureManagers() {
            SerializationService.Ensure();
            ThemeManager.Ensure();
            UIController.Ensure();
        }

        private static void ManagersOnEnabled() {
            SerializationService.Instance.OnEnabled();
            ThemeManager.Instance.OnEnabled();
            UIController.Instance.OnEnabled();

        }

        private static void ReleaseManagers() {
            UIController.Release();
            ThemeManager.Release();
            SerializationService.Release();
        }

        private static void ManagersOnLevelLoaded() {
            SerializationService.Instance.OnLevelLoaded();
            ThemeManager.Instance.OnLevelLoaded();
            UIController.Instance.OnLevelLoaded();
        }

        private static void ManagersOnLevelUnloaded() {
            ThemeManager.Instance.OnLevelUnloaded();
            UIController.Instance.OnLevelUnloaded();
        }

        private static void InstallHarmony() {
            Harmony = HarmonyInstance.Create("com.tpb.thememixer2");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());
            if (IsModEnabled(672248733UL, "UltimateEyeCandy")) {
                UltimateEyeCandyPatch = new UltimateEyeCandyPatch();
                UltimateEyeCandyPatch.Patch(Harmony);
            }
        }

        private static void UnInstallHarmony() {
            if (Harmony == null) return;
            try {
                UltimateEyeCandyPatch?.Unpatch(Harmony);
                Harmony.UnpatchAll("com.tpb.thememixer2");
            } catch (Exception e) {
                Debug.LogError(e);
            } finally {
                UltimateEyeCandyPatch = null;
                Harmony = null;
            }
        }

        private static bool IsModEnabled(ulong publishedFileID, string modName) {
            foreach (var plugin in PluginManager.instance.GetPluginsInfo()) {
                if (plugin.publishedFileID.AsUInt64 == publishedFileID
                    || plugin.name == modName) {
                    return plugin.isEnabled;
                }
            }
            return false;
        }
    }
}
