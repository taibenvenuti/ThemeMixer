using ColossalFramework.Packaging;
using System.Linq;
using UnityEngine;

namespace ThemeMixer.Themes
{
    public class ThemeManager : MonoBehaviour
    {
        private static ThemeManager _instance;

        public static ThemeManager Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<ThemeManager>();
                    if (_instance == null) {
                        GameObject gameObject = GameObject.Find("ThemeMixer");
                        if (gameObject == null) gameObject = new GameObject("ThemeMixer");
                        _instance = gameObject.AddComponent<ThemeManager>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        public static ThemeManager Ensure() => Instance;

        private bool InGame => ToolManager.instance?.m_properties != null && (ToolManager.instance.m_properties?.m_mode & ItemClass.Availability.GameAndMap) != 0;

        public Package.Asset[] Themes { get; private set; } = ThemeUtils.GetThemes().ToArray();

        private ThemeMix _currentMix;
        public ThemeMix CurrentMix {
            get {
                if (_currentMix == null) _currentMix = new ThemeMix();
                return _currentMix;
            }
            private set {
                _currentMix = value;
            }
        }

        public void OnEnabled() {
            if (!InGame) return;
            OnLevelLoaded();
        }

        public void OnLevelLoaded() {
            PackageManager.eventPackagesChanged += OnPackagesChanged;

        }

        private void OnPackagesChanged() {
            Themes = ThemeUtils.GetThemes().ToArray();
        }

        internal void OnLevelUnloaded() {

        }

        public static void Release() {
            if (_instance != null) {
                PackageManager.eventPackagesChanged -= Instance.OnPackagesChanged;
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }

        public void LoadCategory(ThemeCategory category, string packageID) {
            switch (category) {
                case ThemeCategory.Themes:
                    CurrentMix = new ThemeMix(packageID);
                    break;
                case ThemeCategory.Terrain:
                    CurrentMix.Terrain.Load(packageID);
                    break;
                case ThemeCategory.Water:
                    CurrentMix.Water.Load(packageID);
                    break;
                case ThemeCategory.Structures:
                    CurrentMix.Structures.Load(packageID);
                    break;
                case ThemeCategory.Atmosphere:
                    CurrentMix.Atmosphere.Load(packageID);
                    break;
                case ThemeCategory.Weather:
                    CurrentMix.Weather.Load(packageID);
                    break;
                default:
                    break;
            }
        }

        internal void LoadTexture(TextureID textureID, string packageID) {
            switch (textureID) {
                case TextureID.GrassDiffuseTexture:
                    CurrentMix.Terrain.GrassDiffuseTexture.Load(packageID);
                    break;
                case TextureID.RuinedDiffuseTexture:
                    CurrentMix.Terrain.RuinedDiffuseTexture.Load(packageID);
                    break;
                case TextureID.PavementDiffuseTexture:
                    CurrentMix.Terrain.PavementDiffuseTexture.Load(packageID);
                    break;
                case TextureID.GravelDiffuseTexture:
                    CurrentMix.Terrain.GravelDiffuseTexture.Load(packageID);
                    break;
                case TextureID.CliffDiffuseTexture:
                    CurrentMix.Terrain.CliffDiffuseTexture.Load(packageID);
                    break;
                case TextureID.SandDiffuseTexture:
                    CurrentMix.Terrain.SandDiffuseTexture.Load(packageID);
                    break;
                case TextureID.OilDiffuseTexture:
                    CurrentMix.Terrain.OilDiffuseTexture.Load(packageID);
                    break;
                case TextureID.OreDiffuseTexture:
                    CurrentMix.Terrain.OreDiffuseTexture.Load(packageID);
                    break;
                case TextureID.CliffSandNormalTexture:
                    CurrentMix.Terrain.CliffSandNormalTexture.Load(packageID);
                    break;
                case TextureID.UpwardRoadDiffuse:
                    CurrentMix.Structures.UpwardRoadDiffuse.Load(packageID);
                    break;
                case TextureID.DownwardRoadDiffuse:
                    CurrentMix.Structures.DownwardRoadDiffuse.Load(packageID);
                    break;
                case TextureID.BuildingFloorDiffuse:
                    CurrentMix.Structures.BuildingFloorDiffuse.Load(packageID);
                    break;
                case TextureID.BuildingBaseDiffuse:
                    CurrentMix.Structures.BuildingBaseDiffuse.Load(packageID);
                    break;
                case TextureID.BuildingBaseNormal:
                    CurrentMix.Structures.BuildingBaseNormal.Load(packageID);
                    break;
                case TextureID.BuildingBurntDiffuse:
                    CurrentMix.Structures.BuildingBurntDiffuse.Load(packageID);
                    break;
                case TextureID.BuildingAbandonedDiffuse:
                    CurrentMix.Structures.BuildingAbandonedDiffuse.Load(packageID);
                    break;
                case TextureID.LightColorPalette:
                    CurrentMix.Structures.LightColorPalette.Load(packageID);
                    break;
                case TextureID.MoonTexture:
                    CurrentMix.Atmosphere.MoonTexture.Load(packageID);
                    break;
                case TextureID.WaterFoam:
                    CurrentMix.Water.WaterFoam.Load(packageID);
                    break;
                case TextureID.WaterNormal:
                    CurrentMix.Water.WaterNormal.Load(packageID);
                    break;
                default:
                    break;
            }
        }
    }
}
