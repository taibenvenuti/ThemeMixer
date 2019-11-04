using Harmony;
using JetBrains.Annotations;
using ThemeMixer.Themes;
using Object = UnityEngine.Object;

namespace ThemeMixer.Patching
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(ColorCorrectionManager), "currentSelection", MethodType.Setter)]
    public static class ColorCorrectionManagerPatch
    {
        private static OptionsGraphicsPanel _ogp;
        private static OptionsGraphicsPanel Ogp => _ogp ?? (_ogp = Object.FindObjectOfType<OptionsGraphicsPanel>());

        private static void Postfix() {
            ThemeManager.Instance.SetLut();
        }
    }
}
