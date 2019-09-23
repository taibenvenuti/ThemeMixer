using ColossalFramework.UI;
using ThemeMixer.TranslationFramework;

namespace ThemeMixer.UI
{
    public static class UIUtils
    {
        public const int DEFAULT_SPACING = 5; 

        public static void ShowExceptionPanel(string title, string message, bool error) {
            UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                 Translation.Instance.GetTranslation(title),
                 Translation.Instance.GetTranslation(message),
                 error);
        }
    }
}
