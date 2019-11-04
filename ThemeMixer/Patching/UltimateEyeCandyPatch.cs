using System.Reflection;
using Harmony;
using UltimateEyecandy.GUI;
using UnityEngine;

namespace ThemeMixer.Patching
{ 
    public class UltimateEyeCandyPatch
    {
        public void Patch(HarmonyInstance harmonyInstance) {
            harmonyInstance.Patch(TargetMethodInfo, null, new HarmonyMethod(PostfixMethodInfo));
        }

        public void Unpatch(HarmonyInstance harmonyInstance) {
            harmonyInstance.Unpatch(TargetMethodInfo, PostfixMethodInfo);
        }

        private static void Postfix(ref ColorManagementPanel __instance) {
            __instance.loadLutButton.eventClicked += OnLoadLutButtonClicked;
        }

        private static void OnLoadLutButtonClicked(ColossalFramework.UI.UIComponent component, ColossalFramework.UI.UIMouseEventParameter eventParam) {
            Ogp.SendMessage("RefreshColorCorrectionLUTs");
        }

        private MethodInfo PostfixMethodInfo => typeof(UltimateEyeCandyPatch).GetMethod("Postfix", BindingFlags.Static | BindingFlags.NonPublic);

        private MethodInfo TargetMethodInfo => typeof(ColorManagementPanel).GetMethod("SetupControls", BindingFlags.Instance | BindingFlags.NonPublic);

        private static OptionsGraphicsPanel _ogp;
        private static OptionsGraphicsPanel Ogp => _ogp ?? (_ogp = Object.FindObjectOfType<OptionsGraphicsPanel>());
    }
}
