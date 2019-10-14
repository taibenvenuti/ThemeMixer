using System;

namespace ThemeMixer.Themes.Weather
{
    [Serializable]
    public class ThemeWeather
    {
        public WeatherValue MinTemperatureDay;
        public WeatherValue MaxTemperatureDay;
        public WeatherValue MinTemperatureNight;
        public WeatherValue MaxTemperatureNight;
        public WeatherValue MinTemperatureRain;
        public WeatherValue MaxTemperatureRain;
        public WeatherValue MinTemperatureFog;
        public WeatherValue MaxTemperatureFog;

        public WeatherValue RainProbabilityDay;
        public WeatherValue RainProbabilityNight;
        public WeatherValue FogProbabilityDay;
        public WeatherValue FogProbabilityNight;
        public WeatherValue NorthernLightsProbability;

        public ThemeWeather() {
            Initialize();
        }

        private void Initialize() {
            MinTemperatureDay = new WeatherValue(WeatherValue.ValueName.MinTemperatureDay);
            MaxTemperatureDay = new WeatherValue(WeatherValue.ValueName.MaxTemperatureDay);
            MinTemperatureNight = new WeatherValue(WeatherValue.ValueName.MinTemperatureNight);
            MaxTemperatureNight = new WeatherValue(WeatherValue.ValueName.MaxTemperatureNight);
            MinTemperatureRain = new WeatherValue(WeatherValue.ValueName.MinTemperatureRain);
            MaxTemperatureRain = new WeatherValue(WeatherValue.ValueName.MaxTemperatureRain);
            MinTemperatureFog = new WeatherValue(WeatherValue.ValueName.MinTemperatureFog);
            MaxTemperatureFog = new WeatherValue(WeatherValue.ValueName.MaxTemperatureFog);
            RainProbabilityDay = new WeatherValue(WeatherValue.ValueName.RainProbabilityDay);
            RainProbabilityNight = new WeatherValue(WeatherValue.ValueName.RainProbabilityNight);
            FogProbabilityDay = new WeatherValue(WeatherValue.ValueName.FogProbabilityDay);
            FogProbabilityNight = new WeatherValue(WeatherValue.ValueName.FogProbabilityNight);
            NorthernLightsProbability = new WeatherValue(WeatherValue.ValueName.NorthernLightsProbability);
        }

        public void Set(string themeID) {
            SetAll(themeID);
        }

        public bool Load(string themeID = null) {
            if (themeID != null) {
                Set(themeID);
            }
            return LoadAll();
        }

        private void SetAll(string themeID) {
            for (int i = 0; i < (int)WeatherValue.ValueName.Count; i++) {
                SetValue(themeID, (WeatherValue.ValueName)i);
            }
        }

        private bool LoadAll() {
            bool success = true;
            for (int i = 0; i < (int)WeatherValue.ValueName.Count; i++) {
                if (!LoadValue((WeatherValue.ValueName)i)) success = false;
            }
            return success;
        }

        private void SetValue(string themeID, WeatherValue.ValueName name) {
            switch (name) {
                case WeatherValue.ValueName.MinTemperatureDay:
                    MinTemperatureDay = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.MaxTemperatureDay:
                    MaxTemperatureDay = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.MinTemperatureNight:
                    MinTemperatureNight = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.MaxTemperatureNight:
                    MaxTemperatureNight = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.MinTemperatureRain:
                    MinTemperatureRain = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.MaxTemperatureRain:
                    MaxTemperatureRain = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.MinTemperatureFog:
                    MinTemperatureFog = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.MaxTemperatureFog:
                    MaxTemperatureFog = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.RainProbabilityDay:
                    RainProbabilityDay = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.RainProbabilityNight:
                    RainProbabilityNight = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.FogProbabilityDay:
                    FogProbabilityDay = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.FogProbabilityNight:
                    FogProbabilityNight = new WeatherValue(themeID, name);
                    break;
                case WeatherValue.ValueName.NorthernLightsProbability:
                    NorthernLightsProbability = new WeatherValue(themeID, name);
                    break;
                default:
                    break;
            }
        }

        private bool LoadValue(WeatherValue.ValueName name) {
            switch (name) {
                case WeatherValue.ValueName.MinTemperatureDay:
                    return MinTemperatureDay.Load();
                case WeatherValue.ValueName.MaxTemperatureDay:
                    return MaxTemperatureDay.Load();
                case WeatherValue.ValueName.MinTemperatureNight:
                    return MinTemperatureNight.Load();
                case WeatherValue.ValueName.MaxTemperatureNight:
                    return MaxTemperatureNight.Load();
                case WeatherValue.ValueName.MinTemperatureRain:
                    return MinTemperatureRain.Load();
                case WeatherValue.ValueName.MaxTemperatureRain:
                    return MaxTemperatureRain.Load();
                case WeatherValue.ValueName.MinTemperatureFog:
                    return MinTemperatureFog.Load();
                case WeatherValue.ValueName.MaxTemperatureFog:
                    return MaxTemperatureFog.Load();
                case WeatherValue.ValueName.RainProbabilityDay:
                    return RainProbabilityDay.Load();
                case WeatherValue.ValueName.RainProbabilityNight:
                    return RainProbabilityNight.Load();
                case WeatherValue.ValueName.FogProbabilityDay:
                    return FogProbabilityDay.Load();
                case WeatherValue.ValueName.FogProbabilityNight:
                    return FogProbabilityNight.Load();
                case WeatherValue.ValueName.NorthernLightsProbability:
                    return NorthernLightsProbability.Load();
                default: return false;
            }
        }
    }
}
