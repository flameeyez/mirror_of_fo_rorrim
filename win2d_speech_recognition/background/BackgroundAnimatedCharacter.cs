using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace win2d_speech_recognition {
    class BackgroundAnimatedCharacter {
        #region Static
        private static CanvasTextFormat HarryP;
        static BackgroundAnimatedCharacter() {
            HarryP = new CanvasTextFormat() {
                FontFamily = "Harry P",
                FontSize = 60,
                WordWrapping = CanvasWordWrapping.NoWrap
            };
        }
        private static Random r = new Random(DateTime.Now.Millisecond);
        #endregion

        #region Position / Movement / Color
        private int loopCount;
        private int _rotation;
        #endregion

        #region Bounds
        private Rect _boundary;
        public int Width { get { return (int)_boundary.Width; } }
        public int Height { get { return (int)_boundary.Height; } }
        #endregion

        #region Character
        private Color _color;
        public char Character { get; set; }
        public CanvasTextLayout TextLayout { get; set; }
        #endregion

        #region Constructor
        public BackgroundAnimatedCharacter(CanvasDevice device, char c, Color color) {
            _color = color;
            Character = c;
            TextLayout = new CanvasTextLayout(device, c.ToString(), HarryP, 0, 0);
            _boundary = new Rect(0, 0, 100, 150);
        }
        #endregion

        #region Draw / Update
        public void Draw(CanvasAnimatedDrawEventArgs args, Vector2 position) {
            args.DrawingSession.DrawTextLayout(TextLayout, position, _color);
        }
        public void DrawMirrored(CanvasAnimatedDrawEventArgs args, Vector2 position) {
            Matrix3x2 push = args.DrawingSession.Transform;
            args.DrawingSession.Transform = Matrix3x2.CreateRotation((float)(_rotation * Math.PI / 180), position) * Matrix3x2.CreateScale(-1, 1, position);
            args.DrawingSession.DrawTextLayout(TextLayout, position, _color);
            args.DrawingSession.Transform = push;
        }
        public void Update(CanvasAnimatedUpdateEventArgs args) {
            loopCount++;
            _rotation = (_rotation + 1) % 360;
        }
        #endregion
    }
}
