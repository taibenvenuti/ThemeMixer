using UnityEngine;

namespace ThemeMixer.Themes.Atmosphere
{
    public class MoonTexture : TexturePartBase
    {
        public MoonTexture(string packageID) : base (packageID){
            PackageID = packageID;
        }

        public override bool Load(string packageID = null) {
            if (packageID != null) PackageID = packageID;
            if (Texture == null && !SetFromTheme()) return false;
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
            return SetValue(metaData.moonTexture);
        }

        protected override void LoadValue() {
            DayNightProperties properties = DayNightProperties.instance;
            Texture oldTexture = properties.m_MoonTexture;
            properties.m_MoonTexture = Texture;
            if (oldTexture != null) Object.Destroy(oldTexture);
        }
    }
}
