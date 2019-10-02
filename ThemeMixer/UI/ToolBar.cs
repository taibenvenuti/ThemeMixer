using ColossalFramework.UI;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI
{
    public class ToolBar : PanelBase
    {
        public event DragHandle.DragEndEventHandler EventDragEnd;
        public event ButtonBar.ButtonClickedEventHandler EventButtonClicked;

        private DragHandle dragBar;
        private ButtonBar buttonBar;

        public override void Start() {
            base.Start();
            name = "ToolBar";

            color = UIColor;

            dragBar = AddUIComponent<DragHandle>();
            dragBar.Setup(new Vector2(size.x, 18.0f), 0, false, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            dragBar.EventDragEnd += OnDragBarDragEnd;

            buttonBar = AddUIComponent<ButtonBar>();
            buttonBar.Setup(new Vector2(size.x, 0.0f), UIUtils.DEFAULT_SPACING, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            buttonBar.EventButtonClicked += OnButtonClicked;
        }

        public override void OnDestroy() {
            dragBar.EventDragEnd -= OnDragBarDragEnd;
            buttonBar.EventButtonClicked -= OnButtonClicked;

            base.OnDestroy();
        }

        private void OnButtonClicked(Button button, Button[] buttons) {
            EventButtonClicked?.Invoke(button, buttons);
        }

        private void OnDragBarDragEnd() {
            EventDragEnd?.Invoke();
        }
    }
}
