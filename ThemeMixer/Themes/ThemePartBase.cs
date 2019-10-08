using System;
using System.Xml.Serialization;

namespace ThemeMixer.Themes
{
    [Serializable]
    public abstract class ThemePartBase : IMixable
    {
        public string PackageID;
        [XmlIgnore]
        public object Value = null;
        public object CustomValue = null;

        public ThemePartBase() { }

        public ThemePartBase(string packageID) {
            PackageID = packageID;
        }

        protected abstract bool SetFromTheme();

        protected abstract void LoadValue();

        public virtual bool Load(string packageID = null) {
            if (packageID != null) PackageID = packageID;
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
    }
}
