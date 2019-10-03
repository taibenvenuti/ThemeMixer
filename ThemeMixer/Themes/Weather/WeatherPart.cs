using System;

namespace ThemeMixer.Themes.Weather
{
    [Serializable]
    public class WeatherPart : ILoadable, ISettable
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

        public WeatherPart() { }

        public void Set(string packageID) {
            SetAll(packageID);
        }

        public bool Load(string packageID = null) {
            if (packageID != null) {
                Set(packageID);
            }
            return LoadAll();
        }

        private void SetAll(string packageID) {
            for (int i = 0; i < (int)WeatherValue.FloatName.Count; i++) {
                SetValue(packageID, (WeatherValue.FloatName)i);
            }
        }

        private bool LoadAll() {
            bool success = true;
            for (int i = 0; i < (int)WeatherValue.FloatName.Count; i++) {
                if (!LoadValue((WeatherValue.FloatName)i)) success = false;
            }
            return success;
        }

        private void SetValue(string packageID, WeatherValue.FloatName name) {
            switch (name) {
                case WeatherValue.FloatName.MinTemperatureDay:
                    MinTemperatureDay = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.MaxTemperatureDay:
                    MaxTemperatureDay = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.MinTemperatureNight:
                    MinTemperatureNight = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.MaxTemperatureNight:
                    MaxTemperatureNight = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.MinTemperatureRain:
                    MinTemperatureRain = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.MaxTemperatureRain:
                    MaxTemperatureRain = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.MinTemperatureFog:
                    MinTemperatureFog = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.MaxTemperatureFog:
                    MaxTemperatureFog = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.RainProbabilityDay:
                    RainProbabilityDay = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.RainProbabilityNight:
                    RainProbabilityNight = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.FogProbabilityDay:
                    FogProbabilityDay = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.FogProbabilityNight:
                    FogProbabilityNight = new WeatherValue(packageID, name);
                    break;
                case WeatherValue.FloatName.NorthernLightsProbability:
                    NorthernLightsProbability = new WeatherValue(packageID, name);
                    break;
                default:
                    break;
            }
        }

        private bool LoadValue(WeatherValue.FloatName name) {
            switch (name) {
                case WeatherValue.FloatName.MinTemperatureDay:
                    return MinTemperatureDay.Load();
                case WeatherValue.FloatName.MaxTemperatureDay:
                    return MaxTemperatureDay.Load();
                case WeatherValue.FloatName.MinTemperatureNight:
                    return MinTemperatureNight.Load();
                case WeatherValue.FloatName.MaxTemperatureNight:
                    return MaxTemperatureNight.Load();
                case WeatherValue.FloatName.MinTemperatureRain:
                    return MinTemperatureRain.Load();
                case WeatherValue.FloatName.MaxTemperatureRain:
                    return MaxTemperatureRain.Load();
                case WeatherValue.FloatName.MinTemperatureFog:
                    return MinTemperatureFog.Load();
                case WeatherValue.FloatName.MaxTemperatureFog:
                    return MaxTemperatureFog.Load();
                case WeatherValue.FloatName.RainProbabilityDay:
                    return RainProbabilityDay.Load();
                case WeatherValue.FloatName.RainProbabilityNight:
                    return RainProbabilityNight.Load();
                case WeatherValue.FloatName.FogProbabilityDay:
                    return FogProbabilityDay.Load();
                case WeatherValue.FloatName.FogProbabilityNight:
                    return FogProbabilityNight.Load();
                case WeatherValue.FloatName.NorthernLightsProbability:
                    return NorthernLightsProbability.Load();
                default: return false;
            }
        }
    }
}
