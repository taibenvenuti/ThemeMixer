using ICities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ThemeMixer.Themes;
using UnityEngine;

namespace ThemeMixer.Serialization
{
    public class SerializableDataExtension : SerializableDataExtensionBase
    {

        public override void OnSaveData() {
            base.OnSaveData();
            ThemeManager.Instance.OnSaveData(serializableDataManager);
        }

        public override void OnLoadData() {
            base.OnLoadData();

            ThemeManager.Instance.OnLoadData(serializableDataManager);
        }
    }
}
