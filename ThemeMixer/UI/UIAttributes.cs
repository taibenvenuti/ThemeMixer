using ColossalFramework.UI;
using System;
using ThemeMixer.Themes.Enums;
using UnityEngine;

namespace ThemeMixer.UI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UIProperties : Attribute
    {
        public string Name;
        public Vector2 Size;
        public int Spacing;
        public bool AutoLayout;
        public LayoutDirection LayoutDirection;
        public LayoutStart LayoutStart;
        public string BackgroundSprite;

        public UIProperties(string name, float width, float height, int spacing = 0, bool autoLayout = false, LayoutDirection layoutDirection = LayoutDirection.Horizontal, LayoutStart layoutStart = LayoutStart.TopLeft, string backgroundSprite = "") {
            Name = name;
            Size = new Vector2(width, height);
            Spacing = spacing;
            AutoLayout = autoLayout;
            LayoutDirection =layoutDirection;
            LayoutStart = layoutStart;
            BackgroundSprite = backgroundSprite;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UICategoryAttribute : Attribute
    {
        public ThemeCategory Category;

        public UICategoryAttribute(ThemeCategory category) {
            Category = category;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UITextureIDAttribute : Attribute
    {
        public TextureID TextureID;

        public UITextureIDAttribute(TextureID textureID) {
            TextureID = textureID;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UIOffsetIDAttribute : Attribute
    {
        public OffsetID OffsetID;

        public UIOffsetIDAttribute(OffsetID offsetID) {
            OffsetID = offsetID;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UIColorIDAttribute : Attribute
    {
        public ColorID ColorID;

        public UIColorIDAttribute(ColorID colorID) {
            ColorID = colorID;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UIValueIDAttribute : Attribute
    {
        public ValueID ValueID;

        public UIValueIDAttribute(ValueID valueID) {
            ValueID = valueID;
        }
    }
}
