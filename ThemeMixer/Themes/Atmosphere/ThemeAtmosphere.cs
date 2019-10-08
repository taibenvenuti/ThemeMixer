using System;
namespace ThemeMixer.Themes.Atmosphere
{
    [Serializable]
    public class ThemeAtmosphere
    {
        public AtmosphereFloat Longitude;
        public AtmosphereFloat Latitude;

        public AtmosphereFloat SunSize;
        public AtmosphereFloat SunAnisotropy;

        public MoonTexture MoonTexture;

        public AtmosphereFloat MoonSize;

        public AtmosphereColor MoonInnerCorona;
        public AtmosphereColor MoonOuterCorona;

        public AtmosphereFloat Rayleight;
        public AtmosphereFloat Mie;
        public AtmosphereFloat Exposure;
        public AtmosphereFloat StarsIntensity;
        public AtmosphereFloat OuterSpaceIntensity;

        public AtmosphereColor SkyTint;
        public AtmosphereColor NightHorizonColor;
        public AtmosphereColor EarlyNightZenithColor;
        public AtmosphereColor LateNightZenithColor;

        public ThemeAtmosphere() {
            Initialize();
        }

        private void Initialize() {
            Longitude = new AtmosphereFloat(AtmosphereFloat.FloatName.Longitude);
            Latitude = new AtmosphereFloat(AtmosphereFloat.FloatName.Latitude);
            SunSize = new AtmosphereFloat(AtmosphereFloat.FloatName.SunSize);
            SunAnisotropy = new AtmosphereFloat(AtmosphereFloat.FloatName.SunAnisotropy);
            Rayleight = new AtmosphereFloat(AtmosphereFloat.FloatName.Rayleight);
            Mie = new AtmosphereFloat(AtmosphereFloat.FloatName.Mie);
            Exposure = new AtmosphereFloat(AtmosphereFloat.FloatName.Exposure);
            StarsIntensity = new AtmosphereFloat(AtmosphereFloat.FloatName.StarsIntensity);
            OuterSpaceIntensity = new AtmosphereFloat(AtmosphereFloat.FloatName.OuterSpaceIntensity);
            MoonSize = new AtmosphereFloat(AtmosphereFloat.FloatName.MoonSize);
            MoonTexture = new MoonTexture();
            MoonInnerCorona = new AtmosphereColor(AtmosphereColor.ColorName.MoonInnerCorona);
            MoonOuterCorona = new AtmosphereColor(AtmosphereColor.ColorName.MoonOuterCorona);
            SkyTint = new AtmosphereColor(AtmosphereColor.ColorName.SkyTint);
            NightHorizonColor = new AtmosphereColor(AtmosphereColor.ColorName.NightHorizonColor);
            EarlyNightZenithColor = new AtmosphereColor(AtmosphereColor.ColorName.EarlyNightZenithColor);
            LateNightZenithColor = new AtmosphereColor(AtmosphereColor.ColorName.LateNightZenithColor);
        }

        public void Set(string packageID) {
            SetAll(packageID);
        }

        public bool Load(string packageID = null) {
            if (packageID != null) {
                Set(packageID);
            }
            return LoadAll();
        }

        private void SetAll(string packageID) {
            for (int i = 0; i < (int)AtmosphereFloat.FloatName.Count; i++) {
                SetFloat(packageID, (AtmosphereFloat.FloatName)i);
            }
            for (int j = 0; j < (int)AtmosphereColor.ColorName.Count; j++) {
                SetColor(packageID, (AtmosphereColor.ColorName)j);
            }
            SetMoon(packageID);
        }

        private bool LoadAll() {
            bool success = true;
            for (int i = 0; i < (int)AtmosphereFloat.FloatName.Count; i++) {
                if (!LoadFloat((AtmosphereFloat.FloatName)i)) success = false;
            }
            for (int j = 0; j < (int)AtmosphereColor.ColorName.Count; j++) {
                if (!LoadColor((AtmosphereColor.ColorName)j)) success = false;
            }
            if (!LoadMoon()) success = false;
            return success;
        }

        private void SetFloat(string packageID, AtmosphereFloat.FloatName name) {
            switch (name) {
                case AtmosphereFloat.FloatName.Longitude:
                    Longitude = new AtmosphereFloat(packageID, name);
                    break;
                case AtmosphereFloat.FloatName.Latitude:
                    Latitude = new AtmosphereFloat(packageID, name);
                    break;
                case AtmosphereFloat.FloatName.SunSize:
                    SunSize = new AtmosphereFloat(packageID, name);
                    break;
                case AtmosphereFloat.FloatName.SunAnisotropy:
                    SunAnisotropy = new AtmosphereFloat(packageID, name);
                    break;
                case AtmosphereFloat.FloatName.MoonSize:
                    MoonSize = new AtmosphereFloat(packageID, name);
                    break;
                case AtmosphereFloat.FloatName.Rayleight:
                    Rayleight = new AtmosphereFloat(packageID, name);
                    break;
                case AtmosphereFloat.FloatName.Mie:
                    Mie = new AtmosphereFloat(packageID, name);
                    break;
                case AtmosphereFloat.FloatName.Exposure:
                    Exposure = new AtmosphereFloat(packageID, name);
                    break;
                case AtmosphereFloat.FloatName.StarsIntensity:
                    StarsIntensity = new AtmosphereFloat(packageID, name);
                    break;
                case AtmosphereFloat.FloatName.OuterSpaceIntensity:
                    OuterSpaceIntensity = new AtmosphereFloat(packageID, name);
                    break;
                default:
                    break;
            }
        }

        private bool LoadFloat(AtmosphereFloat.FloatName name) {
            switch (name) {
                case AtmosphereFloat.FloatName.Longitude:
                    return Longitude.Load();
                case AtmosphereFloat.FloatName.Latitude:
                    return Latitude.Load();
                case AtmosphereFloat.FloatName.SunSize:
                    return SunSize.Load();
                case AtmosphereFloat.FloatName.SunAnisotropy:
                    return SunAnisotropy.Load();
                case AtmosphereFloat.FloatName.MoonSize:
                    return MoonSize.Load();
                case AtmosphereFloat.FloatName.Rayleight:
                    return Rayleight.Load();
                case AtmosphereFloat.FloatName.Mie:
                    return Mie.Load();
                case AtmosphereFloat.FloatName.Exposure:
                    return Exposure.Load();
                case AtmosphereFloat.FloatName.StarsIntensity:
                    return StarsIntensity.Load();
                case AtmosphereFloat.FloatName.OuterSpaceIntensity:
                    return OuterSpaceIntensity.Load();
                default: return false;
            }
        }

        private void SetColor(string packageID, AtmosphereColor.ColorName name) {
            switch (name) {
                case AtmosphereColor.ColorName.MoonInnerCorona:
                    MoonInnerCorona = new AtmosphereColor(packageID, name);
                    break;
                case AtmosphereColor.ColorName.MoonOuterCorona:
                    MoonOuterCorona = new AtmosphereColor(packageID, name);
                    break;
                case AtmosphereColor.ColorName.SkyTint:
                    SkyTint = new AtmosphereColor(packageID, name);
                    break;
                case AtmosphereColor.ColorName.NightHorizonColor:
                    NightHorizonColor = new AtmosphereColor(packageID, name);
                    break;
                case AtmosphereColor.ColorName.EarlyNightZenithColor:
                    EarlyNightZenithColor = new AtmosphereColor(packageID, name);
                    break;
                case AtmosphereColor.ColorName.LateNightZenithColor:
                    LateNightZenithColor = new AtmosphereColor(packageID, name);
                    break;
                default:
                    break;
            }
        }

        private bool LoadColor(AtmosphereColor.ColorName name) {
            switch (name) {
                case AtmosphereColor.ColorName.MoonInnerCorona:
                    return MoonInnerCorona.Load();
                case AtmosphereColor.ColorName.MoonOuterCorona:
                    return MoonOuterCorona.Load();
                case AtmosphereColor.ColorName.SkyTint:
                    return SkyTint.Load();
                case AtmosphereColor.ColorName.NightHorizonColor:
                    return NightHorizonColor.Load();
                case AtmosphereColor.ColorName.EarlyNightZenithColor:
                    return EarlyNightZenithColor.Load();
                case AtmosphereColor.ColorName.LateNightZenithColor:
                    return LateNightZenithColor.Load();
                default: return false;
            }
        }

        private void SetMoon(string packageID) {
            MoonTexture = new MoonTexture(packageID);
        }

        private bool LoadMoon() {
            return MoonTexture.Load();
        }
    }
}
