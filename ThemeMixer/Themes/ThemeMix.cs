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
        public Terrain.ThemeTerrain Terrain;
        public Water.ThemeWater Water;
        public Atmosphere.ThemeAtmosphere Atmosphere;
        public Structures.ThemeStructures Structures;
        public Weather.ThemeWeather Weather;

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
            Terrain = new ThemeTerrain();
            Water = new ThemeWater();
            Atmosphere = new ThemeAtmosphere();
            Structures = new ThemeStructures();
            Weather = new ThemeWeather();
        }
    }
}
