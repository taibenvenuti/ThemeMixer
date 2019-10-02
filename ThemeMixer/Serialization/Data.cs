using System.Collections.Generic;
using System.Xml.Serialization;
using ThemeMixer.Themes;
using UnityEngine;

namespace ThemeMixer.Serialization
{
    [XmlRoot("ThemeMixerSettings")]
    public class Data
    {
        public bool HideBlacklisted { get; set; } = false;

        public Vector2? ToolbarPosition { get; set; } = null;

        public Vector2? UITogglePosition { get; set; } = null;

        public List<string>[] Favourites { get; set; } = new List<string>[(int)ThemeCategory.Count];

        public List<string>[] Blacklisted { get; set; } = new List<string>[(int)ThemeCategory.Count];

        public Data() {
            for (int i = 0; i < (int)ThemeCategory.Count; i++) {
                Favourites[i] = new List<string>();
                Blacklisted[i] = new List<string>();
            }
        }

        public void OnPreSerialize() {
        }

        public void OnPostDeserialize() {
        }
    }
}
