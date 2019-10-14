using System;
using System.Xml.Serialization;

namespace ThemeMixer.Themes
{
    [Serializable]
    public abstract class ThemePartBase : IMixable
    {
        public string ThemeID;
        [XmlIgnore]
        public object Value = null;
        public object CustomValue = null;

        public ThemePartBase() { }

        public ThemePartBase(string themeID) {
            ThemeID = themeID;
        }

        protected abstract bool SetFromTheme();

        protected abstract void LoadValue();

        public virtual bool Load(string themeID = null) {
            if (themeID != null) ThemeID = themeID;
            if (!SetFromTheme() && Value == null && CustomValue == null) return false;
            LoadValue();
            return true;
        }

        public virtual bool SetValue(object value) {
            if (value == null) return false;
            Value = value;
            return true;
        }

        public virtual void SetCustomValue(object value) {
            CustomValue = value;
            Load();
        }

        protected ThemeManager ThemeManager => ThemeManager.Instance;
    }
}
