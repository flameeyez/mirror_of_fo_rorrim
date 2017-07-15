using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    static class SolveIcons {
        private static List<SolveIcon> _icons = new List<SolveIcon>();
        private static SOLVE_ICON_STATE _state = SOLVE_ICON_STATE.FADING_IN;

        public static bool Done {
            get {
                foreach (SolveIcon icon in _icons) { if (icon.State != SOLVE_ICON_STATE.NORMAL) { return false; } }
                return true;
            }
        }

        public static void Initialize(int iconCount) {
            _icons.Clear();

            // calculate icon positions based on count and canvas width
            int padding = 50;
            int imageWidth = (int)Images.Wand.Bounds.Width;
            int imageHeight = (int)Images.Wand.Bounds.Height;
            int totalWidth = (imageWidth * iconCount) + (padding * (iconCount - 1));
            int centerX = (int)Statics.CanvasWidth / 2;
            int leftX = centerX - totalWidth / 2;

            int x = leftX;
            int y = (int)(Statics.CanvasHeight - 30 - Images.Wand.Bounds.Height);
            for (int i = 0; i < iconCount; i++) {
                _icons.Add(new SolveIcon(new Vector2(x, y)));
                x += imageWidth + padding;
            }
        }

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            foreach (SolveIcon icon in _icons) {
                icon.Draw(args);
            }
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach (SolveIcon icon in _icons) {
                icon.Update(args);
            }
        }

        public static void FadeIn(int index) {
            if (index < 0) { return; }
            if (index >= _icons.Count) { return; }
            _icons[index].FadeIn();
        }
    }
}
