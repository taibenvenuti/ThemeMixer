using System;
using ColossalFramework.Packaging;
using ColossalFramework.UI;
using ThemeMixer.Resources;
using ThemeMixer.Themes;
using ThemeMixer.UI;
using ThemeMixer.UI.FastList;
using UnityEngine;

namespace ThemeMixer
{
    public delegate void ItemClickedEventHandler(ListItem item);
    public delegate void UIDirtyEventHandler(ThemeMix mix);
    public class UIController: MonoBehaviour
    {
        public event UIDirtyEventHandler EventUIDirty;

        private static UIController _instance;
        public static UIController Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<UIController>();
                    if (_instance == null) {
                        GameObject gameObject = GameObject.Find("ThemeMixer");
                        if (gameObject == null) gameObject = new GameObject("ThemeMixer");
                        _instance = gameObject.AddComponent<UIController>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        public Mode Mode { get; set; } = Mode.Category;
        public TextureID TextureID { get; set; } = TextureID.None;

        public ThemeMix Mix => ThemeManager.Instance.CurrentMix;

        public UITextureAtlas ThemeAtlas => ThemeSprites.Atlas;

        private bool InGame => ToolManager.instance?.m_properties != null && (ToolManager.instance.m_properties?.m_mode & ItemClass.Availability.GameAndMap) != 0;

        private ThemeMixerUI ThemeMixerUI { get; set; }
        private UIToggle UIToggle { get; set; }

        public static UIController Ensure() => Instance;

        public void OnEnabled() {
            if (!InGame) return;
            OnLevelLoaded();
        }

        public void OnLevelLoaded() {
            UIToggle = UIView.GetAView().AddUIComponent(typeof(UIToggle)) as UIToggle;
            UIToggle.EventUIToggleClicked += OnUIToggleClicked;
        }

        public string GetTextureSpriteName(TextureID textureID) {
            string packageName = string.Empty;
            switch (textureID) {
                case TextureID.GrassDiffuseTexture:
                    packageName = Mix.Terrain.GrassDiffuseTexture.PackageID;
                    break;
                case TextureID.RuinedDiffuseTexture:
                    packageName = Mix.Terrain.RuinedDiffuseTexture.PackageID;
                    break;
                case TextureID.PavementDiffuseTexture:
                    packageName = Mix.Terrain.PavementDiffuseTexture.PackageID;
                    break;
                case TextureID.GravelDiffuseTexture:
                    packageName = Mix.Terrain.GravelDiffuseTexture.PackageID;
                    break;
                case TextureID.CliffDiffuseTexture:
                    packageName = Mix.Terrain.CliffDiffuseTexture.PackageID;
                    break;
                case TextureID.SandDiffuseTexture:
                    packageName = Mix.Terrain.SandDiffuseTexture.PackageID;
                    break;
                case TextureID.OilDiffuseTexture:
                    packageName = Mix.Terrain.OilDiffuseTexture.PackageID;
                    break;
                case TextureID.OreDiffuseTexture:
                    packageName = Mix.Terrain.OreDiffuseTexture.PackageID;
                    break;
                case TextureID.CliffSandNormalTexture:
                    packageName = Mix.Terrain.CliffSandNormalTexture.PackageID;
                    break;
                case TextureID.UpwardRoadDiffuse:
                    packageName = Mix.Structures.UpwardRoadDiffuse.PackageID;
                    break;
                case TextureID.DownwardRoadDiffuse:
                    packageName = Mix.Structures.DownwardRoadDiffuse.PackageID;
                    break;
                case TextureID.BuildingFloorDiffuse:
                    packageName = Mix.Structures.BuildingFloorDiffuse.PackageID;
                    break;
                case TextureID.BuildingBaseDiffuse:
                    packageName = Mix.Structures.BuildingBaseDiffuse.PackageID;
                    break;
                case TextureID.BuildingBaseNormal:
                    packageName = Mix.Structures.BuildingBaseNormal.PackageID;
                    break;
                case TextureID.BuildingBurntDiffuse:
                    packageName = Mix.Structures.BuildingBurntDiffuse.PackageID;
                    break;
                case TextureID.BuildingAbandonedDiffuse:
                    packageName = Mix.Structures.BuildingAbandonedDiffuse.PackageID;
                    break;
                case TextureID.LightColorPalette:
                    packageName = Mix.Structures.LightColorPalette.PackageID;
                    break;
                case TextureID.MoonTexture:
                    packageName = Mix.Atmosphere.MoonTexture.PackageID;
                    break;
                case TextureID.WaterFoam:
                    packageName = Mix.Water.WaterFoam.PackageID;
                    break;
                case TextureID.WaterNormal:
                    packageName = Mix.Water.WaterNormal.PackageID;
                    break;
                default:
                    break;
            }
            return string.Concat(packageName, Enum.GetName(typeof(TextureID), textureID));
        }

        internal void OnLevelUnloaded() {
            DestroyUI();
        }

        public static void Release() {
            if (_instance != null) {
                _instance.DestroyUI();
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }

        private void DestroyUI() {
            if (UIToggle != null) {
                Destroy(UIToggle.gameObject);
                UIToggle = null;
            }
            if (ThemeMixerUI != null) {
                Destroy(ThemeMixerUI.gameObject);
                ThemeMixerUI = null;
            }
        }

        private void OnUIToggleClicked() {
            if (ThemeMixerUI != null) {
                ThemeMixerUI.EventThemeClicked -= OnThemeClicked;
                Destroy(ThemeMixerUI.gameObject);
                ThemeMixerUI = null;
                return;
            }
            ThemeMixerUI = UIView.GetAView().AddUIComponent(typeof(ThemeMixerUI)) as ThemeMixerUI;
            ThemeMixerUI.EventThemeClicked += OnThemeClicked;
        }

        private void OnThemeClicked(ListItem item) {
            switch (Mode) {
                case Mode.Category:
                    Debug.LogError(string.Concat("Load Category: ", item.Category));
                    ThemeManager.Instance.LoadCategory(item.Category, item.ID);
                    break;
                case Mode.Texture:
                    ThemeManager.Instance.LoadTexture(TextureID, item.ID);
                    break;
                case Mode.Color:
                    break;
                case Mode.Number:
                    break;
                case Mode.Offset:
                    break;
                default:
                    break;
            }
            Mode = Mode.Category;
            TextureID = TextureID.None;
        }

        public bool IsSelected(Package.Asset asset) {
            return SimulationManager.instance.m_metaData.m_MapThemeMetaData?.assetRef == asset;
        }
    }

    public enum Mode
    {
        Category,
        Texture,
        Color,
        Number,
        Offset
    }


    public enum TextureID
    {
        None,
        GrassDiffuseTexture,
        RuinedDiffuseTexture,
        PavementDiffuseTexture,
        GravelDiffuseTexture,
        CliffDiffuseTexture,
        SandDiffuseTexture,
        OilDiffuseTexture,
        OreDiffuseTexture,
        CliffSandNormalTexture,
        UpwardRoadDiffuse,
        DownwardRoadDiffuse,
        BuildingFloorDiffuse,
        BuildingBaseDiffuse,
        BuildingBaseNormal,
        BuildingBurntDiffuse,
        BuildingAbandonedDiffuse,
        LightColorPalette,
        MoonTexture,
        WaterFoam,
        WaterNormal
    }
}
