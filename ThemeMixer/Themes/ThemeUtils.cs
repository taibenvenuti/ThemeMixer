using ColossalFramework.Packaging;
using System.Collections.Generic;
using System.Linq;
using ThemeMixer.Locale;
using UnityEngine;

namespace ThemeMixer.Themes
{
    public static class ThemeUtils
    {
        internal static MapThemeMetaData GetThemeFromPackage(string packageID) {
            Package package = PackageManager.GetPackage(packageID);
            if (package == null) {
                UI.UIUtils.ShowExceptionPanel(TranslationID.ERROR_MISSING_PACKAGE_TITLE, TranslationID.ERROR_MISSING_PACKAGE_MESSAGE, true);
                Debug.LogError($"No Package named {packageID} was found.");
                return null;
            }

            MapThemeMetaData metaData = package.FilterAssets(new Package.AssetType[] { UserAssetType.MapThemeMetaData })?
                                                 .FirstOrDefault()?.Instantiate<MapThemeMetaData>();
            if (metaData == null) {
                UI.UIUtils.ShowExceptionPanel(TranslationID.ERROR_BROKEN_PACKAGE_TITLE, TranslationID.ERROR_BROKEN_PACKAGE_MESSAGE, true);
                Debug.LogError($"The {packageID} package is not a Map Theme.");
                return null;
            }
            return metaData;
        }

        public static IEnumerable<Package.Asset> GetThemes() {
            return PackageManager.FilterAssets(UserAssetType.MapThemeMetaData);
        }
    }
}
