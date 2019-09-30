using ColossalFramework.UI;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI
{
    public static class UIExtensions
    {
        public static void CreateSpace(this PanelBase parent, float width, float height) {
            UIPanel panel = parent.AddUIComponent<UIPanel>();
            panel.size = new Vector2(width, height);
        }
    }
}
