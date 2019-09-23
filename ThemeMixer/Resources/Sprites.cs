using ColossalFramework.UI;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ThemeMixer.Resources
{
    public class Sprites
    {
        public static Sprites Atlas { get; private set; } = new Sprites();

        public static UITextureAtlas DefaultAtlas => (ToolManager.instance.m_properties.m_mode == ItemClass.Availability.Game) ? UIView.GetAView().defaultAtlas : UIView.library?.Get<OptionsMainPanel>("OptionsPanel")?.GetComponent<UIPanel>()?.atlas;

        public static string DragHandle = "DragHandle";

        public static string ThemesIcon = "ThemesIcon";
        public static string ThemesIconHovered = "ThemesIconHovered";
        public static string ThemesIconPressed = "ThemesIconPressed";
        public static string ThemesIconFocused = "ThemesIconFocused";

        public static string TerrainIcon = "TerrainIcon";
        public static string TerrainIconHovered = "TerrainIconHovered";
        public static string TerrainIconPressed = "TerrainIconPressed";
        public static string TerrainIconFocused = "TerrainIconFocused";

        public static string WaterIcon = "WaterIcon";
        public static string WaterIconHovered = "WaterIconHovered";
        public static string WaterIconPressed = "WaterIconPressed";
        public static string WaterIconFocused = "WaterIconFocused";

        public static string AtmosphereIcon = "AtmosphereIcon";
        public static string AtmosphereIconHovered = "AtmosphereIconHovered";
        public static string AtmosphereIconPressed = "AtmosphereIconPressed";
        public static string AtmosphereIconFocused = "AtmosphereIconFocused";

        public static string StructuresIcon = "StructuresIcon";
        public static string StructuresIconHovered = "StructuresIconHovered";
        public static string StructuresIconPressed = "StructuresIconPressed";
        public static string StructuresIconFocused = "StructuresIconFocused";

        public static string WeatherIcon = "WeatherIcon";
        public static string WeatherIconHovered = "WeatherIconHovered";
        public static string WeatherIconPressed = "WeatherIconPressed";
        public static string WeatherIconFocused = "WeatherIconFocused";

        public static string SettingsIcon = "SettingsIcon";
        public static string SettingsIconHovered = "SettingsIconHovered";
        public static string SettingsIconPressed = "SettingsIconPressed";
        public static string SettingsIconFocused = "SettingsIconFocused";

        public static string UIToggleIcon = "UIToggleIcon";
        public static string UIToggleIconHovered = "UIToggleIconHovered";
        public static string UIToggleIconPressed = "UIToggleIconPressed";
        public static string UIToggleIconFocused = "UIToggleIconFocused";

        private UITextureAtlas UITextureAtlas { get; set; }

        private readonly string[] _spriteNames = new string[] {
            "DragHandle",

            "ThemesIcon",
            "ThemesIconHovered",
            "ThemesIconPressed",
            "ThemesIconFocused",

            "TerrainIcon",
            "TerrainIconHovered",
            "TerrainIconPressed",
            "TerrainIconFocused",

            "WaterIcon",
            "WaterIconHovered",
            "WaterIconPressed",
            "WaterIconFocused",

            "AtmosphereIcon",
            "AtmosphereIconHovered",
            "AtmosphereIconPressed",
            "AtmosphereIconFocused",

            "StructuresIcon",
            "StructuresIconHovered",
            "StructuresIconPressed",
            "StructuresIconFocused",

            "WeatherIcon",
            "WeatherIconHovered",
            "WeatherIconPressed",
            "WeatherIconFocused",

            "SettingsIcon",
            "SettingsIconHovered",
            "SettingsIconPressed",
            "SettingsIconFocused",

            "UIToggleIcon",
            "UIToggleIconHovered",
            "UIToggleIconPressed",
            "UIToggleIconFocused"
        };

        public Sprites() {
            CreateAtlas();
        }

        public static implicit operator UITextureAtlas(Sprites atlas) {
            return atlas.UITextureAtlas;
        }

        private void CreateAtlas() {
            UITextureAtlas textureAtlas = ScriptableObject.CreateInstance<UITextureAtlas>();

            Texture2D[] textures = new Texture2D[_spriteNames.Length];
            Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);

            for (int i = 0; i < _spriteNames.Length; i++)
                textures[i] = GetTextureFromAssemblyManifest(_spriteNames[i] + ".png");

            int maxSize = 1024;
            Rect[] regions = texture2D.PackTextures(textures, 2, maxSize);

            Material material = Object.Instantiate(DefaultAtlas.material);
            material.mainTexture = texture2D;
            textureAtlas.material = material;
            textureAtlas.name = "ThemeMixerAtlas";

            for (int i = 0; i < _spriteNames.Length; i++) {
                UITextureAtlas.SpriteInfo item = new UITextureAtlas.SpriteInfo {
                    name = _spriteNames[i],
                    texture = textures[i],
                    region = regions[i],
                };

                textureAtlas.AddSprite(item);
            }

            UITextureAtlas = textureAtlas;
        }

        private Texture2D GetTextureFromAssemblyManifest(string file) {
            string path = string.Concat(GetType().Namespace, ".Files.", file);
            Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)) {
                byte[] array = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(array, 0, array.Length);
                texture2D.LoadImage(array);
            }
            texture2D.wrapMode = TextureWrapMode.Clamp;
            texture2D.Apply();
            return texture2D;
        }
    }
}
