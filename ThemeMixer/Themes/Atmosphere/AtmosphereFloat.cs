using JetBrains.Annotations;
using ThemeMixer.Themes.Abstraction;

namespace ThemeMixer.Themes.Atmosphere
{
    public class AtmosphereFloat : ThemePartBase
    {
        public FloatName Name;

        [UsedImplicitly]
        public AtmosphereFloat() { }

        public AtmosphereFloat(FloatName floatName) {
            Name = floatName;
        }

        public AtmosphereFloat(string themeID, FloatName floatName) : base(themeID) {
            Name = floatName;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeManager.GetTheme(ThemeID);
            if (metaData == null) return false;
            switch (Name) {
                case FloatName.Longitude:
                    SetValue(metaData.longitude);
                    break;
                case FloatName.Latitude:
                    SetValue(metaData.latitude);
                    break;
                case FloatName.SunSize:
                    SetValue(metaData.sunSize);
                    break;
                case FloatName.SunAnisotropy:
                    SetValue(metaData.sunAnisotropy);
                    break;
                case FloatName.MoonSize:
                    SetValue(metaData.moonSize);
                    break;
                case FloatName.Rayleigh:
                    SetValue(metaData.rayleight);
                    break;
                case FloatName.Mie:
                    SetValue(metaData.mie);
                    break;
                case FloatName.Exposure:
                    SetValue(metaData.exposure);
                    break;
                case FloatName.StarsIntensity:
                    SetValue(metaData.starsIntensity);
                    break;
                case FloatName.OuterSpaceIntensity:
                    SetValue(metaData.outerSpaceIntensity);
                    break;
            }
            return true;
        }

        protected override void LoadValue() {
            DayNightProperties properties = DayNightProperties.instance;
            switch (Name) {
                case FloatName.Longitude:
                    properties.m_Longitude = (float) (CustomValue ?? Value);
                    break;
                case FloatName.Latitude:
                    properties.m_Latitude = (float)(CustomValue ?? Value);
                    break;
                case FloatName.SunSize:
                    properties.m_SunSize = (float)(CustomValue ?? Value);
                    break;
                case FloatName.SunAnisotropy:
                    properties.m_SunAnisotropyFactor = (float)(CustomValue ?? Value);
                    break;
                case FloatName.MoonSize:
                    properties.m_MoonSize = (float)(CustomValue ?? Value);
                    break;
                case FloatName.Rayleigh:
                    properties.m_RayleighScattering = (float)(CustomValue ?? Value);
                    break;
                case FloatName.Mie:
                    properties.m_MieScattering = (float)(CustomValue ?? Value);
                    break;
                case FloatName.Exposure:
                    properties.m_Exposure = (float)(CustomValue ?? Value);
                    break;
                case FloatName.StarsIntensity:
                    properties.m_StarsIntensity = (float)(CustomValue ?? Value);
                    break;
                case FloatName.OuterSpaceIntensity:
                    properties.m_OuterSpaceIntensity = (float)(CustomValue ?? Value);
                    break;
            }
        }

        public enum FloatName
        {
            Longitude,
            Latitude,

            SunSize,
            SunAnisotropy,
            MoonSize,

            Rayleigh,
            Mie,
            Exposure,
            StarsIntensity,
            OuterSpaceIntensity,

            Count
        }
    }
}
