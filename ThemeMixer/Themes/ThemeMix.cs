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
        public TerrainPart Terrain;
        public WaterPart Water;
        public AtmosphericPart Atmosphere;
        public StructuresPart Structures;
        public WeatherPart Weather;

        public ThemeMix() { }

        public void OnPreSerialize() { }

        public void OnPostDeserialize() { }
    }
}
