using ColossalFramework.UI;
using ThemeMixer.UI.Abstraction;

namespace ThemeMixer.UI
{
    public class ToolBar : PanelBase
    {
        public event DragHandle.DragEndEventHandler EventDragEnd;
        public event ButtonBar.ButtonClickedEventHandler EventButtonClicked;

        private DragHandle dragBar;
        private ButtonBar buttonBar;

        public override void Awake() {
            base.Awake();
            Setup("Tool Bar", 40.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
            color = UIColor;

            dragBar = AddUIComponent<DragHandle>();
            dragBar.EventDragEnd += OnDragBarDragEnd;

            buttonBar = AddUIComponent<ButtonBar>();
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
