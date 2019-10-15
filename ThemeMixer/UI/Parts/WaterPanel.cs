﻿using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.UI.Color;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{
    [UICategory(ThemeCategory.Water)]
    [UIProperties("Water Panel", 360.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel")]
    public class WaterPanel : PanelBase
    {
        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;

        [UICategory(ThemeCategory.Water)]
        [UITextureID(TextureID.WaterFoam)]
        protected TexturePanel waterFoamPanel;

        [UICategory(ThemeCategory.Water)]
        [UITextureID(TextureID.WaterNormal)]
        protected TexturePanel waterNormalPanel;

        [UICategory(ThemeCategory.Water)]
        [UIProperties("Colors Panel", 360.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase colorsPanel;

        [UICategory(ThemeCategory.Water)]
        [UIColorID(ColorID.WaterClean)]
        [UIProperties("WaterClean Panel", 350.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect")]
        protected ColorPanel waterCleanColorPanel;

        [UICategory(ThemeCategory.Water)]
        [UIColorID(ColorID.WaterUnder)]
        [UIProperties("WaterUnder Panel", 350.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect")]
        protected ColorPanel waterUnderColorPanel;

        [UICategory(ThemeCategory.Water)]
        [UIColorID(ColorID.WaterDirty)]
        [UIProperties("WaterDirty Panel", 350.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect")]
        protected ColorPanel waterDirtyColorPanel;


        public override void Awake() {
            base.Awake();
            CreateLabel();
            CreateTexturePanels();
            CreateColorsPanel();
        }

        private void CreateLabel() {
            labelPanel = AddUIComponent<UIPanel>();
            labelPanel.size = new Vector2(width, 22.0f);
            label = labelPanel.AddUIComponent<UILabel>();
            label.font = UIUtils.BoldFont;
            label.textScale = 1.0f;
            label.textAlignment = UIHorizontalAlignment.Center;
            label.verticalAlignment = UIVerticalAlignment.Middle;
            label.padding = new RectOffset(0, 0, 4, 0);
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_WATER);
            label.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;
            string loadTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_LOADFROMTHEME);
            loadButton = UIUtils.CreateButton(label, new Vector2(22.0f, 22.0f), tooltip: loadTooltip, backgroundSprite: "ThemesIcon", atlas: UISprites.Atlas);
            loadButton.relativePosition = new Vector2(label.width + 5.0f, 0.0f);
            loadButton.eventClicked += OnLoadWaterFromTheme;
        }

        private void CreateTexturePanels() {
            waterNormalPanel = AddUIComponent<TexturePanel>();
            waterFoamPanel = AddUIComponent<TexturePanel>();
        }

        private void CreateColorsPanel() {
            colorsPanel = AddUIComponent<PanelBase>();
            waterCleanColorPanel = colorsPanel.AddUIComponent<ColorPanel>();
            colorsPanel.CreateSpace(1.0f, 5.0f);
            waterUnderColorPanel = colorsPanel.AddUIComponent<ColorPanel>();
            colorsPanel.CreateSpace(1.0f, 5.0f);
            waterDirtyColorPanel = colorsPanel.AddUIComponent<ColorPanel>();
            colorsPanel.CreateSpace(1.0f, 5.0f);
        }

        private void OnLoadWaterFromTheme(UIComponent component, UIMouseEventParameter eventParam) {
            Controller.OnLoadFromTheme(ThemeCategory.Water, ThemeCategory.Water);
        }
    }
}
