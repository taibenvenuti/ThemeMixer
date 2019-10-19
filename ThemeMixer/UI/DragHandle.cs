using ColossalFramework.UI;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.Resources;
using UnityEngine;

namespace ThemeMixer.UI
{
    public class DragHandle : PanelBase
    {
        private UIDragHandle dragHandle;
        private UIPanel panel;
        public delegate void DragEndEventHandler();
        public event DragEndEventHandler EventDragEnd;

        public override void Awake() {
            base.Awake();
            Setup("Drag Handle", 40.0f, 18.0f, 0, false, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            dragHandle = AddUIComponent<UIDragHandle>();
            dragHandle.isInteractive = true;
            dragHandle.size = new Vector2(width, height);
            dragHandle.relativePosition = new Vector2(0.0f, 0.0f);
            dragHandle.eventMouseUp += OnDragEnd;

            panel = AddUIComponent<UIPanel>();
            panel.size = new Vector2(width, height);
            panel.relativePosition = new Vector2(0.0f, 0.0f);
            panel.atlas = UISprites.Atlas;
            panel.backgroundSprite = UISprites.DragHandle;
            panel.isInteractive = false;
            panel.color = UIColorDark;
        }

        public override void Start() {
            base.Start();
            dragHandle.target = parent.parent;
        }

        public override void OnDestroy() {
            dragHandle.eventMouseUp -= OnDragEnd;

            base.OnDestroy();
        }

        private void OnDragEnd(UIComponent component, UIMouseEventParameter eventParam) {
            EventDragEnd?.Invoke();
        }
    }
}
