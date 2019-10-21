using System;
using System.Xml.Serialization;
using UnityEngine;

namespace ThemeMixer.Themes
{
    [Serializable]
    public abstract class ThemePartBase : IMixable
    {
        public string ThemeID;
        [XmlIgnore]
        public object Value = null;
        [XmlElement("Float", typeof(float))]
        [XmlElement("Int", typeof(int))]
        [XmlElement("Vector3", typeof(Vector3))]
        [XmlElement("Color", typeof(Color))]
        public object CustomValue;

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
