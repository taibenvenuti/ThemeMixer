using ColossalFramework.UI;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.Locale;
using ThemeMixer.Themes;
using ThemeMixer.TranslationFramework;
using UnityEngine;
using ThemeMixer.Resources;
using System.Text.RegularExpressions;
using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.FastList
{
    [UIProperties("List Row", 456.0f, 76.0f, UIUtils.DEFAULT_SPACING, true, LayoutDirection.Horizontal, LayoutStart.TopLeft, "WhiteRect")]
    public class ListRow : PanelBase, IUIFastListRow
    {
        public delegate void FavouriteChangedEventHandler(string itemID, bool favourite);
        public event FavouriteChangedEventHandler EventFavouriteChanged;

        public delegate void BlacklistedChangedEventHandler(string itemID, bool blacklisted);
        public event BlacklistedChangedEventHandler EventBlacklistedChanged;

        public delegate void ValuesButtonClickedEventHandler(string itemID);
        public event ValuesButtonClickedEventHandler EventValuesClicked;

        private UIPanel thumbnailPanel;
        private UISprite thumbnailSprite;


        [UIProperties("Labels Panel", 255.0f, 64.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        private PanelBase labelsPanel;

        private UILabel nameLabel;
        private UILabel authorLabel;

        private UIPanel checkboxPanel;
        private UICheckBox favouriteCheckbox;
        private UISprite checkedSprite;
        private UISprite uncheckedSprite;

        private UIButton valuesButton;
        private UILabel valuesLabel;
        private UISprite valuesSprite;

        private ListItem itemData;
        private Color32 EvenColor { get; } = new Color32(67, 76, 80, 255); 
        private Color32 OddColor { get; } = new Color32(57, 67, 70, 255);
        private Color32 SelectedColor { get; } = new Color32(20, 155, 215, 255);

        private bool isRowOdd;

        public override void Awake() {
            base.Awake();
            color = isRowOdd ? OddColor : EvenColor;
            CreateThumbnail();
            CreateLabels();
            CreateValuesPanel();
            CreateCheckbox();
            this.CreateSpace(0.0f, 30.0f);
            eventMouseEnter += OnMouseEnterEvent;
            eventMouseLeave += OnMouseLeaveEvent;
        }

        public override void OnDestroy() {
            base.OnDestroy();
            if (favouriteCheckbox != null)
                favouriteCheckbox.eventClicked -= OnFavouriteCheckboxMouseUp;
            eventMouseEnter -= OnMouseEnterEvent;
            eventMouseLeave -= OnMouseLeaveEvent;
            EventFavouriteChanged = null;
            EventBlacklistedChanged = null;
        }

        public void Select(bool isRowOdd) {
            //color = SelectedColor;
        }

        public void Deselect(bool isRowOdd) {

        }

        public void Display(object data, bool isRowOdd) {
            if (data is ListItem item) {
                itemData = item;
                this.isRowOdd = isRowOdd;
                DisplayItem(isRowOdd);
            }
        }

        private void CreateThumbnail() {
            thumbnailPanel = AddUIComponent<UIPanel>();
            thumbnailPanel.size = new Vector2(115.0f, 66.0f);
            thumbnailPanel.atlas = UISprites.DefaultAtlas;
            thumbnailPanel.backgroundSprite = "WhiteRect";
            thumbnailPanel.color = UIColor;

            thumbnailSprite = thumbnailPanel.AddUIComponent<UISprite>();
            thumbnailSprite.atlas = ThemeSprites.Atlas;
            thumbnailSprite.size = new Vector2(113.0f, 64.0f);
            thumbnailSprite.relativePosition = new Vector2(1.0f, 1.0f);
        }

        private void CreateLabels() {
            labelsPanel = AddUIComponent<PanelBase>();
            labelsPanel.autoFitChildrenHorizontally = true;

            nameLabel = labelsPanel.AddUIComponent<UILabel>();
            nameLabel.autoSize = false;
            nameLabel.size = new Vector2(width, 33.0f);
            nameLabel.padding = new RectOffset(5, 0, 8, 0);
            nameLabel.textScale = 1.0f;
            nameLabel.font = UIUtils.BoldFont;

            authorLabel = labelsPanel.AddUIComponent<UILabel>();
            authorLabel.autoSize = false;
            authorLabel.size = new Vector2(width, 33.0f);
            authorLabel.padding = new RectOffset(5, 0, 2, 0);
            authorLabel.textScale = 0.8f;
            authorLabel.font = UIUtils.BoldFont;
        }

        private void CreateValuesPanel() {
            valuesButton = AddUIComponent<UIButton>();
            valuesButton.size = new Vector2(66.0f, 66.0f);
            valuesButton.eventClicked += OnValuesButtonClicked;

            valuesLabel = valuesButton.AddUIComponent<UILabel>();
            valuesLabel.text = " ";
            valuesLabel.autoSize = false;
            valuesLabel.size = valuesButton.size;
            valuesLabel.relativePosition = new Vector2(0.0f, 0.0f);
            valuesLabel.textAlignment = UIHorizontalAlignment.Center;
            valuesLabel.verticalAlignment = UIVerticalAlignment.Middle;

            valuesSprite = valuesButton.AddUIComponent<UISprite>();
            valuesSprite.size = new Vector2(64.0f, 64.0f);
            valuesSprite.relativePosition = new Vector2(1.0f, 1.0f);

            valuesButton.isVisible = false;
        }

        private void OnValuesButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventValuesClicked?.Invoke(itemData.ID);
        }

        private void CreateCheckbox() {
            checkboxPanel = AddUIComponent<UIPanel>();
            checkboxPanel.size = new Vector2(66.0f, 66.0f);

            favouriteCheckbox = checkboxPanel.AddUIComponent<UICheckBox>();
            favouriteCheckbox.size = new Vector2(22f, 22f);
            favouriteCheckbox.relativePosition = new Vector3(22.0f, 22.0f);

            uncheckedSprite = favouriteCheckbox.AddUIComponent<UISprite>();
            uncheckedSprite.atlas = UISprites.Atlas;
            uncheckedSprite.spriteName = UISprites.StarOutline;
            uncheckedSprite.size = favouriteCheckbox.size;
            uncheckedSprite.relativePosition = Vector3.zero;

            checkedSprite = uncheckedSprite.AddUIComponent<UISprite>();
            checkedSprite.atlas = UISprites.Atlas;
            checkedSprite.spriteName = UISprites.Star;
            checkedSprite.size = favouriteCheckbox.size;
            checkedSprite.relativePosition = Vector2.zero;

            favouriteCheckbox.checkedBoxObject = checkedSprite;
            favouriteCheckbox.eventMouseUp += OnFavouriteCheckboxMouseUp;
            favouriteCheckbox.isChecked = false;
        }

        private void OnFavouriteCheckboxMouseUp(UIComponent component, UIMouseEventParameter eventParam) {
            if (eventParam.buttons == UIMouseButton.Right) {
                bool blackListed = !itemData.IsBlacklisted;
                itemData.IsBlacklisted = blackListed;
                if (blackListed) {
                    favouriteCheckbox.isChecked = true;
                    checkedSprite.spriteName = UISprites.Blacklisted;
                    uncheckedSprite.spriteName = "";
                    if (itemData.IsFavourite) {
                        itemData.IsFavourite = false;
                        EventFavouriteChanged?.Invoke(itemData.ID, false);
                    }
                } else {
                    if (!itemData.IsFavourite) {
                        favouriteCheckbox.isChecked = false;
                    }
                    uncheckedSprite.spriteName = UISprites.StarOutline;
                }
                EventBlacklistedChanged?.Invoke(itemData.ID, blackListed);
            } else if (eventParam.buttons == UIMouseButton.Left) {
                bool favourite = !itemData.IsFavourite;
                itemData.IsFavourite = favourite;
                if (favourite) {
                    favouriteCheckbox.isChecked = true;
                    checkedSprite.spriteName = UISprites.Star;
                    uncheckedSprite.spriteName = UISprites.StarOutline;
                    if (itemData.IsBlacklisted) {
                        itemData.IsBlacklisted = false;
                        EventBlacklistedChanged?.Invoke(itemData.ID, false);
                    }
                } else {
                    if (!itemData.IsBlacklisted) {
                        favouriteCheckbox.isChecked = false;
                    }
                }
                EventFavouriteChanged?.Invoke(itemData.ID, favourite);
            }
            UpdateCheckboxTooltip();
        }

        private void DisplayItem(bool isRowOdd) {
            color = isRowOdd ? OddColor : EvenColor;
            string spriteName = string.Concat(itemData.ID, itemData.DisplayName, "_", "Snapshot");
            spriteName = Regex.Replace(spriteName, @"(\s+|@|&|'|\(|\)|<|>|#|"")", "");
            thumbnailSprite.spriteName = spriteName;
            nameLabel.text = itemData.DisplayName;
            nameLabel.FitString();
            authorLabel.text = string.Concat(Translation.Instance.GetTranslation(TranslationID.LABEL_BY), " ", itemData.Author);
            authorLabel.FitString();
            favouriteCheckbox.isChecked = itemData.IsFavourite || itemData.IsBlacklisted;
            checkedSprite.spriteName = itemData.IsBlacklisted ? UISprites.Blacklisted : UISprites.Star;
            uncheckedSprite.spriteName = itemData.IsBlacklisted ? "" :  UISprites.StarOutline;
            float width = itemData.Category == ThemeCategory.Themes || itemData.Category == ThemeCategory.None ? 255.0f : 189.0f;
            authorLabel.width = nameLabel.width = labelsPanel.width = width;
            valuesButton.isVisible = itemData.Category != ThemeCategory.Themes && itemData.Category != ThemeCategory.None;
            //switch (itemData.ThemePart) {
            //    case Themes.ThemePart.Terrain:
            //        break;
            //    case Themes.ThemePart.Water:
            //        break;
            //    case Themes.ThemePart.Structures:
            //        break;
            //    case Themes.ThemePart.Atmosphere:
            //        break;
            //    case Themes.ThemePart.Weather:
            //        break;
            //    default:
            //        break;
            //}
            UpdateCheckboxTooltip();
        }

        private void UpdateCheckboxTooltip() {
            favouriteCheckbox.tooltip = itemData.IsFavourite
                            ? Translation.Instance.GetTranslation(TranslationID.TOOLTIP_REMOVEFAVOURITE)
                            : itemData.IsBlacklisted
                            ? Translation.Instance.GetTranslation(TranslationID.TOOLTIP_REMOVEBLACKLIST)
                            : Translation.Instance.GetTranslation(TranslationID.TOOLTIP_ADDFAVOURITE_ADDBLACKLIST);
            //favouriteCheckbox.RefreshTooltip();
        }

        private void OnMouseLeaveEvent(UIComponent component, UIMouseEventParameter eventParam) {
            if (itemData != null) {
                color = isRowOdd ? OddColor : EvenColor;
            }
        }

        private void OnMouseEnterEvent(UIComponent component, UIMouseEventParameter eventParam) {
            if (itemData != null) {
                color = new Color32((byte)(OddColor.r + 25), (byte)(OddColor.g + 25), (byte)(OddColor.b + 25), 255);
            }
        }
    }
}
