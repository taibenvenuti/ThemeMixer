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
        public TerrainTheme Terrain;
        public WaterTheme Water;
        public AtmosphericTheme Atmosphere;
        public StructuresTheme Structures;
        public WeatherTheme Weather;

        public ThemeMix() { }

        public void OnPreSerialize() { }

        public void OnPostDeserialize() { }
    }
}
