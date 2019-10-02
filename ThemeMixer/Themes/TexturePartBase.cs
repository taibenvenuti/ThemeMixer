using ColossalFramework.Packaging;
using System.Xml.Serialization;
using UnityEngine;

namespace ThemeMixer.Themes
{
    public abstract class TexturePartBase : ThemePartBase
    {
        [XmlIgnore]
        public Texture2D Texture { get; set; }

        public TexturePartBase(string packageID) : base(packageID) { }


        public override bool Load(string packageID = null) {
            if (packageID != null) PackageID = packageID;
            if (Texture == null && !SetFromTheme()) return false;
            LoadValue();
            return true;
        }

        public bool SetValue(Package.Asset asset) {
            if (asset == null) return false;
            Texture2D oldTexture = Texture;
            Texture = asset.Instantiate<Texture2D>();
            if (Texture == null) {
                Texture = oldTexture;
                return false;
            }
            Texture.anisoLevel = 8;
            Texture.filterMode = FilterMode.Trilinear;
            Texture.Apply();
            if (oldTexture != null) UnityEngine.Object.Destroy(oldTexture);
            return true;
        }
    }
}
