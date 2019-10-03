using System;
using ThemeMixer.Themes.Atmosphere;
using ThemeMixer.Themes.Structures;
using ThemeMixer.Themes.Terrain;
using ThemeMixer.Themes.Water;
using ThemeMixer.Themes.Weather;

namespace ThemeMixer.Themes
{
    public class ThemeMix : ILoadable
    {
        public string Name;
        public TerrainPart Terrain;
        public WaterPart Water;
        public AtmospherePart Atmosphere;
        public StructuresPart Structures;
        public WeatherPart Weather;

        public ThemeMix() {
            InitializeMix();
        }

        public ThemeMix(string packageID) {
            InitializeMix();
            Load(packageID);
        }

        public void OnPreSerialize() { }

        public void OnPostDeserialize() { }

        public bool Load(string packageID) {
            bool success = true;
            if (!Terrain.Load(packageID)) success = false;
            if (!Water.Load(packageID)) success = false;
            if (!Atmosphere.Load(packageID)) success = false;
            if (!Structures.Load(packageID)) success = false;
            if (!Weather.Load(packageID)) success = false;
            return success;
        }

        private void InitializeMix() {
            Terrain = new TerrainPart();
            Water = new WaterPart();
            Atmosphere = new AtmospherePart();
            Structures = new StructuresPart();
            Weather = new WeatherPart();
        }
    }
}
