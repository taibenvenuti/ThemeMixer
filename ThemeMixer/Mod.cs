using ColossalFramework.UI;
using ICities;
using JetBrains.Annotations;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Serialization;
using ThemeMixer.Themes;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI;

namespace ThemeMixer
{
    public class Mod : IUserMod, ILoadingExtension
    {
        public string Name => "Theme Mixer 2";

        public string Description => Translation.Instance.GetTranslation(TranslationID.MOD_DESCRIPTION);

        public static bool InGame => (ToolManager.instance.m_properties.m_mode == ItemClass.Availability.Game);

        [UsedImplicitly]
        public void OnEnabled() {
            EnsureManagers();
            ManagersOnEnabled();
        }

        [UsedImplicitly]
        public void OnDisabled() {
            ReleaseManagers();
        }

        public void OnCreated(ILoading loading) {
            UITextureAtlas atlas = ThemeSprites.Atlas;
        }

        public void OnReleased() { }
        
        public void OnLevelLoaded(LoadMode mode) {
            ManagersOnLevelLoaded(mode);
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

        private static void ManagersOnLevelLoaded(LoadMode mode) {
            SerializationService.Instance.OnLevelLoaded();
            ThemeManager.Instance.OnLevelLoaded();
            UIController.Instance.OnLevelLoaded();
        }

        private static void ManagersOnLevelUnloaded() {
            ThemeManager.Instance.OnLevelUnloaded();
            UIController.Instance.OnLevelUnloaded();
            SerializationService.Instance.OnLevelUnloaded();
        }
    }
}
