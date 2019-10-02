namespace ThemeMixer.Themes.Weather
{
    public class WeatherValue : ThemePartBase
    {
        public FloatName Name;

        public WeatherValue(string packageID, FloatName floatName) : base(packageID) {
            Name = floatName;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            switch (Name) {
                case FloatName.MinTemperatureDay:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.MaxTemperatureDay:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.MinTemperatureNight:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.MaxTemperatureNight:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.MinTemperatureRain:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.MaxTemperatureRain:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.MinTemperatureFog:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.MaxTemperatureFog:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.RainProbabilityDay:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.RainProbabilityNight:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.FogProbabilityDay:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.FogProbabilityNight:
                    SetValue(metaData.minTemperatureDay);
                    break;
                case FloatName.NorthernLightsProbability:
                    SetValue(metaData.minTemperatureDay);
                    break;
                default:
                    break;
            }
            return true;
        }

        protected override void LoadValue() {
            WeatherProperties properties = WeatherManager.instance.m_properties;
            switch (Name) {
                case FloatName.MinTemperatureDay:
                    properties.m_minTemperatureDay = (float)(CustomValue ?? Value);
                    break;
                case FloatName.MaxTemperatureDay:
                    properties.m_maxTemperatureDay = (float)(CustomValue ?? Value);
                    break;
                case FloatName.MinTemperatureNight:
                    properties.m_minTemperatureNight = (float)(CustomValue ?? Value);
                    break;
                case FloatName.MaxTemperatureNight:
                    properties.m_maxTemperatureNight = (float)(CustomValue ?? Value);
                    break;
                case FloatName.MinTemperatureRain:
                    properties.m_minTemperatureRain = (float)(CustomValue ?? Value);
                    break;
                case FloatName.MaxTemperatureRain:
                    properties.m_maxTemperatureRain = (float)(CustomValue ?? Value);
                    break;
                case FloatName.MinTemperatureFog:
                    properties.m_minTemperatureFog = (float)(CustomValue ?? Value);
                    break;
                case FloatName.MaxTemperatureFog:
                    properties.m_maxTemperatureFog = (float)(CustomValue ?? Value);
                    break;
                case FloatName.RainProbabilityDay:
                    properties.m_rainProbabilityDay = (int)(CustomValue ?? Value);
                    break;
                case FloatName.RainProbabilityNight:
                    properties.m_rainProbabilityNight = (int)(CustomValue ?? Value);
                    break;
                case FloatName.FogProbabilityDay:
                    properties.m_fogProbabilityDay = (int)(CustomValue ?? Value);
                    break;
                case FloatName.FogProbabilityNight:
                    properties.m_fogProbabilityNight = (int)(CustomValue ?? Value);
                    break;
                case FloatName.NorthernLightsProbability:
                    properties.m_northernLightsProbability = (int)(CustomValue ?? Value);
                    break;
                default:
                    break;
            }
        }

        public enum FloatName
        {
            MinTemperatureDay,
            MaxTemperatureDay,
            MinTemperatureNight,
            MaxTemperatureNight,
            MinTemperatureRain,
            MaxTemperatureRain,
            MinTemperatureFog,
            MaxTemperatureFog,

            RainProbabilityDay,
            RainProbabilityNight,
            FogProbabilityDay,
            FogProbabilityNight,
            NorthernLightsProbability,

            Count
        }
    }
}
