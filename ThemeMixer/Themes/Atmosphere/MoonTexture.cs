using ThemeMixer.Themes.Abstraction;
using UnityEngine;

namespace ThemeMixer.Themes.Atmosphere
{
    public sealed class MoonTexture : TexturePartBase
    {
        public MoonTexture() { }

        public MoonTexture(string themeID) : base (themeID){
            ThemeID = themeID;
            Load(themeID);
        }

        public override bool Load(string themeID = null) {
            if (themeID != null) ThemeID = themeID;
            if (!SetFromTheme() && Texture == null) return false;
            LoadValue();
            return true;
        }

        public override bool SetValue(object asset) {
            bool result = base.SetValue(asset);
            if (!result) return false;
            return true;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeManager.GetTheme(ThemeID);
            if (metaData == null) return false;
            return SetTexture(metaData.moonTexture);
        }

        protected override void LoadValue() {
            DayNightProperties properties = DayNightProperties.instance;
            Texture oldTexture = properties.m_MoonTexture;
            Texture.wrapMode = TextureWrapMode.Clamp;
            properties.m_MoonTexture = Texture;
            if (oldTexture != null && !ReferenceEquals(oldTexture, Texture)) Object.Destroy(oldTexture);
        }
    }
}
