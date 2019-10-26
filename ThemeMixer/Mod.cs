using System;
using System.Linq;
using System.Reflection;
using ColossalFramework.UI;
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

        private static HarmonyInstance Harmony { get; set; }

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

        public void OnCreated(ILoading loading) {
            UITextureAtlas atlas = ThemeSprites.Atlas;
        }

        public void OnReleased() { }

        public void OnLevelLoaded(LoadMode mode) {
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
            SerializationService.Instance.OnLevelUnloaded();
        }

        private static void InstallHarmony() {
            Harmony = HarmonyInstance.Create("com.tpb.thememixer2");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        private static void UnInstallHarmony() {
            if (Harmony == null) return;
            try {
                Harmony.UnpatchAll("com.tpb.thememixer2");
            } catch (Exception e) {
                Debug.LogError(e);
                Harmony = null;
            }
        }
    }
}
