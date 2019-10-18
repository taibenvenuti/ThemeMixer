using ColossalFramework.UI;
using ThemeMixer.Resources;
using UnityEngine;

namespace ThemeMixer.UI.Color
{
    public class SwatchButton : UIButton
    {
        public UnityEngine.Color swatch;
        public delegate void SwatchClickedEventHandler(UnityEngine.Color color, UIMouseEventParameter eventParam, UIComponent component);
        public event SwatchClickedEventHandler EventSwatchClicked;

        public void Build(UnityEngine.Color color) {
            size = new Vector2(19.0f, 19.0f);
            atlas = UISprites.Atlas;
            normalBgSprite = UISprites.Swatch;
            hoveredColor = new Color32((byte)Mathf.Min((color.r + 32), 255), (byte)Mathf.Min((color.g + 32), 255), (byte)Mathf.Min((color.b + 32), 255), 255);
            pressedColor = new Color32((byte)Mathf.Min((color.r + 64), 255), (byte)Mathf.Min((color.g + 64), 255), (byte)Mathf.Min((color.b + 64), 255), 255);
            focusedColor = color;
            this.color = color;
            swatch = color;
        }

        public override void OnDestroy() {
            EventSwatchClicked = null;
            eventClicked -= OnSwatchClicked;
            base.OnDestroy();
        }

        public override void Start() {
            eventClicked += OnSwatchClicked;
        }

        private void OnSwatchClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventSwatchClicked?.Invoke(swatch, eventParam, component);
        }
    }
}
