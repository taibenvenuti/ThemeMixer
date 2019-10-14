using System;
using ThemeMixer.Themes.Atmosphere;
using ThemeMixer.Themes.Structures;
using ThemeMixer.Themes.Terrain;
using ThemeMixer.Themes.Water;
using ThemeMixer.Themes.Weather;

namespace ThemeMixer.Themes
{
    public class ThemeMix
    {
        public string Name;
        public ThemeTerrain Terrain;
        public ThemeWater Water;
        public ThemeAtmosphere Atmosphere;
        public ThemeStructures Structures;
        public ThemeWeather Weather;

        public ThemeMix() {
            InitializeMix();
        }

        public ThemeMix(string themeID) {
            InitializeMix();
            Load(themeID);
        }

        public void OnPreSerialize() { }

        public void OnPostDeserialize() { }

        public bool Load(string themeID) {
            bool success = true;
            if (!Terrain.Load(themeID)) success = false;
            if (!Water.Load(themeID)) success = false;
            if (!Atmosphere.Load(themeID)) success = false;
            if (!Structures.Load(themeID)) success = false;
            if (!Weather.Load(themeID)) success = false;
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
