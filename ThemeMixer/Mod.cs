using System;
using ICities;
using ThemeMixer.Locale;
using ThemeMixer.Serialization;
using ThemeMixer.Themes;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI;

namespace ThemeMixer
{
    public class Mod : IUserMod, ILoadingExtension
    {
        public string Name => "Theme Mixer 2.0";

        public string Description => Translation.Instance.GetTranslation(TranslationID.MOD_DESCRIPTION);

        public void OnEnabled() {
            EnsureManagers();
            ManagersOnEnabled();
        }

        public void OnDisabled() {
            ReleaseManagers();
        }

        public void OnCreated(ILoading loading) {
            var atlas = UIController.Instance.ThemeAtlas;
        }

        public void OnReleased() { }
        
        public void OnLevelLoaded(LoadMode mode) {
            ManagersOnLevelLoaded(mode);
        }

        public void OnLevelUnloading() {
            ManagersOnLevelUnloaded();
        }

        private void EnsureManagers() {
            SerializationService.Ensure();
            ThemeManager.Ensure();
            UIController.Ensure();
        }

        private void ManagersOnEnabled() {
            SerializationService.Instance.OnEnabled();
            ThemeManager.Instance.OnEnabled();
            UIController.Instance.OnEnabled();

        }

        private void ReleaseManagers() {
            UIController.Release();
            ThemeManager.Release();
            SerializationService.Release();
        }

        private void ManagersOnLevelLoaded(LoadMode mode) {
            SerializationService.Instance.OnLevelLoaded();
            ThemeManager.Instance.OnLevelLoaded();
            UIController.Instance.OnLevelLoaded();
        }

        private void ManagersOnLevelUnloaded() {
            ThemeManager.Instance.OnLevelUnloaded();
            UIController.Instance.OnLevelUnloaded();
            SerializationService.Instance.OnLevelUnloaded();
        }
    }
}
