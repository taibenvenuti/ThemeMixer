using ColossalFramework.UI;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.Resources;
using UnityEngine;

namespace ThemeMixer.UI
{
    [UIProperties("Drag Handle", 40.0f, 18.0f, 0, false, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
    public class DragHandle : PanelBase
    {
        private UIDragHandle dragHandle;
        private UIPanel panel;
        public delegate void DragEndEventHandler();
        public event DragEndEventHandler EventDragEnd;

        public override void Awake() {
            base.Awake();

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
            panel.color = new Color32(54, 54, 54, 255);
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
