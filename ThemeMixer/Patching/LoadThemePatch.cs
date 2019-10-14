using Harmony;
using System.Collections.Generic;
using System.Linq;
using static LoadSavePanelBase<MapThemeMetaData>;

namespace ThemeMixer.Patching
{
    [HarmonyPatch(typeof(LoadThemePanel), "Refresh")]
    public static class LoadThemePatch
    {
        static List<ListItem> removeItems = new List<ListItem>();
        static string[] forbidden = new string[] {
            "CO-Boreal-Theme",
            "CO-Temperate-Theme",
            "CO-Winter-Theme",
            "CO-Europe-Theme",
            "CO-Tropical-Theme"
        };

        static void Postfix(ref List<ListItem> ___m_Items) {
            removeItems.Clear();
            foreach (var item in ___m_Items) {
                if (forbidden.Contains(item.asset.fullName)) removeItems.Add(item);
            }
            foreach (var item in removeItems) {
                ___m_Items.Remove(item);
            }
        }
    }
}
