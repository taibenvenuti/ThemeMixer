using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace ThemeMixer.Themes.Structures
{
    public class StructuresTheme
    {
        public StructureTexture upwardRoadDiffuse;
        public StructureTexture downwardRoadDiffuse;
        public StructureTexture buildingFloorDiffuse;
        public StructureTexture buildingBaseDiffuse;
        public StructureTexture buildingBaseNormal;
        public StructureTexture buildingBurntDiffuse;
        public StructureTexture buildingAbandonedDiffuse;
        public StructureTexture lightColorPalette;
    }

    public class StructureTexture
    {
        public Name TextureName;
        public string PackageID;
        [XmlIgnore]
        public Texture2D Texture;

        public StructureTexture() { }

        public StructureTexture(string packageID, Name textureName) {
            PackageID = packageID;
            TextureName = textureName;
        }

        public enum Name
        {
            UpwardRoadDiffuse,
            DownwardRoadDiffuse,
            FloorDiffuse,
            BaseDiffuse,
            BaseNormal,
            BurntDiffuse,
            AbandonedDiffuse,
            LightColorPalette,
            Count
        }
    }
}
