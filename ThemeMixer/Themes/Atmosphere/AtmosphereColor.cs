using UnityEngine;

namespace ThemeMixer.Themes.Atmosphere
{
    public class AtmosphereColor : ThemePartBase
    {
        public ColorName Name;

        public AtmosphereColor(string packageID, ColorName floatName) : base(packageID) {
            Name = floatName;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            switch (Name) {
                case ColorName.MoonInnerCorona:
                    SetValue(metaData.moonInnerCorona);
                    break;
                case ColorName.MoonOuterCorona:
                    SetValue(metaData.moonInnerCorona);
                    break;
                case ColorName.SkyTint:
                    SetValue(metaData.moonInnerCorona);
                    break;
                case ColorName.NightHorizonColor:
                    SetValue(metaData.moonInnerCorona);
                    break;
                case ColorName.EarlyNightZenithColor:
                    SetValue(metaData.moonInnerCorona);
                    break;
                case ColorName.LateNightZenithColor:
                    SetValue(metaData.moonInnerCorona);
                    break;
                default:
                    break;
            }
            return true;
        }

        protected override void LoadValue() {
            DayNightProperties properties = DayNightProperties.instance;
            switch (Name) {
                case ColorName.MoonInnerCorona:
                    properties.m_MoonInnerCorona = (Color)(CustomValue ?? Value);
                    break;
                case ColorName.MoonOuterCorona:
                    properties.m_MoonInnerCorona = (Color)(CustomValue ?? Value);
                    break;
                case ColorName.SkyTint:
                    properties.m_SkyTint = (Color)(CustomValue ?? Value);
                    break;
                case ColorName.NightHorizonColor:
                    properties.m_NightHorizonColor = (Color)(CustomValue ?? Value);
                    break;
                case ColorName.EarlyNightZenithColor: {
                    GradientColorKey[] c = properties.m_NightZenithColor.colorKeys;
                    GradientAlphaKey[] a = properties.m_NightZenithColor.alphaKeys;
                    c[0].color = c[3].color = properties.m_NightZenithColor.colorKeys[0].color;
                    c[1].color = c[2].color = (Color)(CustomValue ?? Value);
                    properties.m_NightZenithColor.SetKeys(c, a);
                }
                    break;
                case ColorName.LateNightZenithColor: {
                    GradientColorKey[] c = properties.m_NightZenithColor.colorKeys;
                    GradientAlphaKey[] a = properties.m_NightZenithColor.alphaKeys;
                    c[0].color = c[3].color = (Color)(CustomValue ?? Value);
                    c[1].color = c[2].color = properties.m_NightZenithColor.colorKeys[1].color;
                    properties.m_NightZenithColor.SetKeys(c, a);
                }
                    break;
                default:
                    break;
            }
        }

        public enum ColorName
        {
            MoonInnerCorona,
            MoonOuterCorona,

            SkyTint,
            NightHorizonColor,
            EarlyNightZenithColor,
            LateNightZenithColor,

            Count
        }
    }
}
