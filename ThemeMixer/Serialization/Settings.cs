using System.Xml.Serialization;
using UnityEngine;

namespace ThemeMixer.Serialization
{
    [XmlRoot("ThemeMixerSettings")]
    public class Settings
    {
        public Vector2? ToolbarPosition { get; set; } = null;

        public Vector2? UITogglePosition { get; set; } = null;

        public string OverrideThemeID { get; set; } = null;

        public Settings() {
        }

        public void OnPreSerialize() {
        }

        public void OnPostDeserialize() {
        }

    }
}
