using ColossalFramework.UI;
using ColossalFramework.Packaging;
using System.Collections.Generic;
using ThemeMixer.Themes;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.UI.FastList;
using UnityEngine;
using ColossalFramework.PlatformServices;

namespace ThemeMixer.UI.Parts
{
    public class ThemesPanel : PanelBase
    {
        public delegate void ItemClickEventHandler(ListItem item);
        public event ItemClickEventHandler EventItemClick;

        protected UIFastList fastList;
        protected static List<Package.Asset> FavouritesList = new List<Package.Asset>();
        protected static List<Package.Asset> Blacklist = new List<Package.Asset>();
        protected static List<Package.Asset> NormalList = new List<Package.Asset>();

        public override void Start() {
            base.Start();
            name = "ThemesPanel";
            float width = ThemeManager.Instance.Themes.Length > 7 ? 468.0f : 456.0f;
            CreateFastList(new Vector2(width, 720.0f), 76.0f);
            SetupRowsData();
            BindEvents();
            this.CreateSpace(width, 0.0f);
        }

        protected bool IsFavourite(string itemID) {
            return Data.IsFavourite(itemID, ThemePart);
        }

        protected bool IsBlacklisted(string itemID) {
            return Data.IsBlacklisted(itemID, ThemePart);
        }


        public override void OnDestroy() {
            UnbindEvents();
            base.OnDestroy();
        }

        private void OnItemClick(UIComponent component, int itemIndex) {
            ListItem item = fastList.RowsData[itemIndex] as ListItem;
            if (item != null) {
                EventItemClick?.Invoke(item);
            }
        }

        private void OnFavouriteChanged(string itemID, bool favourite) {
            if (favourite) {
                Data.AddToFavourites(itemID, ThemePart);
            } else Data.RemoveFromFavourites(itemID, ThemePart);
        }

        private void OnBlacklistedChanged(string itemID, bool blacklisted) {
            if (blacklisted) {
                Data.AddToBlacklist(itemID, ThemePart);
            } else Data.RemoveFromBlacklist(itemID, ThemePart);
        }

        private void CreateFastList(Vector2 size, float rowHeight) {
            fastList = UIFastList.Create<ListRow>(this);
            fastList.BackgroundSprite = "UnlockingPanel";
            fastList.size = size;
            fastList.RowHeight = rowHeight;
            fastList.CanSelect = true;
            fastList.AutoHideScrollbar = true;
            fastList.EventItemClick += OnItemClick;
        }

        private void BindEvents() {
            for (int rowIndex = 0; rowIndex < fastList.Rows.m_size; rowIndex++) {
                if (fastList.Rows[rowIndex] is ListRow row) {
                    row.EventFavouriteChanged += OnFavouriteChanged;
                    row.EventBlacklistedChanged += OnBlacklistedChanged;
                }
            }
        }

        private void UnbindEvents() {
            for (int rowIndex = 0; rowIndex < fastList.Rows.m_size; rowIndex++) {
                if (fastList.Rows[rowIndex] is ListRow row) {
                    row.EventFavouriteChanged -= OnFavouriteChanged;
                    row.EventBlacklistedChanged -= OnBlacklistedChanged;
                }
            }
        }
        protected void SetupRowsData() {
            int selectedIndex = 0;
            if (fastList.RowsData == null) {
                fastList.RowsData = new FastList<object>();
            }
            fastList.RowsData.Clear();
            FavouritesList.Clear();
            Blacklist.Clear();
            NormalList.Clear();
            int index = 0;
            int count = 0;
            List<string> favList = Data.GetFavourites(ThemePart);
            List<string> blacklist = Data.GetBlacklisted(ThemePart);
            foreach (Package.Asset asset in ThemeManager.Instance.Themes) {
                if (favList.Contains(asset.package.packageName)) {
                    FavouritesList.Add(asset);
                } else if (blacklist.Contains(asset.package.packageName)) {
                    Blacklist.Add(asset);
                } else NormalList.Add(asset);
            }
            for (int i = 0; i < FavouritesList.Count; i++) {
                Package.Asset asset = FavouritesList[i];
                ListItem listItem = CreateListItem(asset);
                if (Controller.IsSelected(asset)) selectedIndex = index;
                fastList.RowsData.Add(listItem);
                count++;
                index++;
            }
            for (int i = 0; i < NormalList.Count; i++) {
                Package.Asset asset = NormalList[i];
                ListItem listItem = CreateListItem(asset);
                if (Controller.IsSelected(asset)) selectedIndex = index;
                fastList.RowsData.Add(listItem);
                count++;
                index++;
            }
            if (!Data.HideBlacklisted) {
                for (int i = 0; i < Blacklist.Count; i++) {
                    Package.Asset asset = Blacklist[i];
                    ListItem listItem = CreateListItem(asset);
                    if (Controller.IsSelected(asset)) selectedIndex = index;
                    fastList.RowsData.Add(listItem);
                    count++;
                    index++;
                }
            }
            fastList.RowsData.SetCapacity(count);
            count = Mathf.Clamp(count, 0, 7);
            fastList.height = count * 76.0f;
            fastList.DisplayAt(selectedIndex);
            fastList.SelectedIndex = selectedIndex;
        }

        protected ListItem CreateListItem(Package.Asset asset) {
            string id = asset.package.packageName;
            string displayName = asset.Instantiate<MapThemeMetaData>().name;
            string author = GetAuthorName(asset);
            bool isFavourite = IsFavourite(id);
            bool isBlacklisted = IsBlacklisted(id);
            return new ListItem(id, displayName, author, isFavourite, isBlacklisted, ThemePart);
        }

        private static string GetAuthorName(Package.Asset asset) {
            if (ulong.TryParse(asset.package.packageAuthor.Substring("steamid:".Length), out ulong authorID)) {
                string author = new Friend(new UserID(authorID)).personaName;
                return author;
            }
            return "N/A";
        }
    }
}
