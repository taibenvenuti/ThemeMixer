using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace ThemeMixer.Themes.Water
{
    public class WaterTheme
    {
        public WaterTexture WaterFoam;
        public WaterTexture WaterNormal;

        public Color WaterClean;
        public Color WaterDirty;
        public Color WaterUnder;
    }

    public class WaterTexture
    {
        public Name TextureName;
        public string PackageName;
        [XmlIgnore]
        public Texture2D Texture;

        public WaterTexture() { }

        public WaterTexture(string packageName, Name textureName) {
            PackageName = packageName;
            TextureName = textureName;
        }

        public enum Name
        {
            WaterNormal,
            WaterFoam
        }
    }
}
