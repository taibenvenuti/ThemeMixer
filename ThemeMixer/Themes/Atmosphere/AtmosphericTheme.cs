using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace ThemeMixer.Themes.Atmosphere
{
    public class AtmosphericTheme : ILoadable
    {
        public float Longitude;
        public float Latitude;

        public float SunSize;
        public float SunAnisotropy;

        public MoonTexture MoonTexture;
        public float MoonSize;
        public Color MoonInnerCorona;
        public Color MoonOuterCorona;

        public float Rayleight;
        public float Mie;
        public float Exposure;
        public float StarsIntensity;
        public float OuterSpaceIntensity;
        public Color SkyTint;
        public Color NightHorizonColor;
        public Color EarlyNightZenithColor;
        public Color LateNightZenithColor;

        public bool Load(string packageID) {
            throw new NotImplementedException();
        }
    }

    public class MoonTexture
    {
        public string PackageName;
        [XmlIgnore]
        public Texture2D Texture;
    }
}
