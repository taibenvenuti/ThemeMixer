using UnityEngine;

namespace ThemeMixer.Themes.Atmosphere
{
    public class MoonTexture : TexturePartBase
    {
        public MoonTexture() { }

        public MoonTexture(string packageID) : base (packageID){
            PackageID = packageID;
            Load(packageID);
        }

        public override bool Load(string packageID = null) {
            if (packageID != null) PackageID = packageID;
            if (!SetFromTheme() && Texture == null) return false;
            LoadValue();
            return true;
        }

        public override bool SetValue(object asset) {
            bool result = base.SetValue(asset);
            if (!result) return false;
            Texture.wrapMode = TextureWrapMode.Clamp;
            return true;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            return SetTexture(metaData.moonTexture);
        }

        protected override void LoadValue() {
            DayNightProperties properties = DayNightProperties.instance;
            Texture oldTexture = properties.m_MoonTexture;
            properties.m_MoonTexture = Texture;
            if (oldTexture != null && !ReferenceEquals(oldTexture, Texture)) Object.Destroy(oldTexture);
        }
    }
}
