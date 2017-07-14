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
    static class Boundary {
        public static Rect Large = new Rect(0, 0, 150, 200);
        public static Rect Small = new Rect(0, 0, 100, 150);
    }

    class PuzzleAnimatedCharacter {
        #region State
        public enum STATE {
            NORMAL,
            GROWING,
            SHRINKING,
            DONE,
            FADING_IN,
            FADING_OUT,
            SOLVE_EXIT_STAGE_LEFT,
            SOLVE_EXIT_STAGE_RIGHT
        }
        public enum SOLVED_HORIZONTAL_MOVEMENT {
            LEFT,
            RIGHT
        }
        public STATE State { get; set; }
        private SOLVED_HORIZONTAL_MOVEMENT HorizontalMovement { get; set; }
        public bool Done { get { return State == STATE.DONE; } }
        #endregion

        private byte opacity = 0;
        private bool large = false;

        #region Static
        private static Random r = new Random(DateTime.Now.Millisecond);
        private static CanvasTextFormat HarryP;
        private static CanvasTextFormat HarryPLarge;
        static PuzzleAnimatedCharacter() {
            HarryP = new CanvasTextFormat() {
                FontFamily = "Harry P",
                FontSize = 120,
                WordWrapping = CanvasWordWrapping.NoWrap
            };

            HarryPLarge = new CanvasTextFormat() {
                FontFamily = "Harry P",
                FontSize = 160,
                WordWrapping = CanvasWordWrapping.NoWrap
            };
        }
        #endregion

        #region Bounds
        public int Width { get { return (int)_boundary.Width; } }
        public int Height { get { return (int)_boundary.Height; } }
        private Rect _boundary;
        #endregion

        #region Position
        private Vector2 _position;
        public Vector2 Position {
            get {
                return _position;
            }
            set {
                _position = value;
                _boundary = large ? Boundary.Large : Boundary.Small;
                _solvedPosition.X = _position.X;
                _solvedPosition.Y = _position.Y;
            }
        }
        private Vector2 _solvedPosition;

        private int _solvedVelocityX = 5;
        private int _solvedVelocityY = 5;

        private int loopCount = r.Next();
        private int _offsetY;
        private int _rotation;
        private float scalingFactor = 1.0f;
        #endregion

        #region Character
        public char Character { get; set; }
        public CanvasTextLayout TextLayout { get; set; }
        private Color _color;
        #endregion

        #region Constructor / Initialization
        public PuzzleAnimatedCharacter(CanvasDevice device, char c, Color color, bool bUseLargeFont = false) {
            Character = c;
            CanvasTextFormat font = bUseLargeFont ? HarryPLarge : HarryP;
            TextLayout = new CanvasTextLayout(device, c.ToString(), font, 0, 0);
            _color = color;
            State = STATE.NORMAL;
            large = bUseLargeFont;
            _boundary = large ? Boundary.Large : Boundary.Small;
        }
        public void Refresh() {
            loopCount = r.Next();
            opacity = 0;
            State = STATE.FADING_IN;
            _solvedPosition.X = Position.X;
            _solvedPosition.Y = Position.Y;
            _solvedVelocityX = 5;
            _solvedVelocityY = 10 - r.Next(20);
        }
        #endregion

        #region Draw / Update
        public void Draw(CanvasAnimatedDrawEventArgs args) {
            Color drawColor = _color;
            Matrix3x2 push = args.DrawingSession.Transform;
            switch (State) {
                case STATE.GROWING:
                case STATE.SHRINKING:
                    drawColor = Colors.Yellow;
                    args.DrawingSession.Transform = Matrix3x2.CreateScale(scalingFactor, scalingFactor, new Vector2(Position.X + (float)TextLayout.LayoutBounds.Width / 2, Position.Y + (float)TextLayout.LayoutBounds.Height / 2));// * Matrix3x2.CreateRotation((float)(_rotation * Math.PI / 180), new Vector2(Position.X + (float)TextLayout.LayoutBounds.Width / 2, Position.Y + (float)TextLayout.LayoutBounds.Height / 2));
                    args.DrawingSession.DrawTextLayout(TextLayout, new Vector2(Position.X, Position.Y + _offsetY), drawColor);
                    args.DrawingSession.Transform = push;
                    break;
                case STATE.NORMAL:
                    args.DrawingSession.DrawTextLayout(TextLayout, new Vector2(Position.X, Position.Y + _offsetY), drawColor);
                    break;
                case STATE.SOLVE_EXIT_STAGE_LEFT:
                case STATE.SOLVE_EXIT_STAGE_RIGHT:
                    args.DrawingSession.DrawTextLayout(TextLayout, new Vector2(_solvedPosition.X, _solvedPosition.Y + _offsetY), drawColor);
                    break;
                case STATE.FADING_IN:
                    drawColor = Color.FromArgb(opacity, drawColor.R, drawColor.G, drawColor.B);
                    args.DrawingSession.DrawTextLayout(TextLayout, new Vector2(_solvedPosition.X, _solvedPosition.Y + _offsetY), drawColor);
                    break;
            }
        }
        public void Update(CanvasAnimatedUpdateEventArgs args) {
            _offsetY = (int)(7 * Math.Sin(++loopCount * 0.05));
            _rotation = (_rotation + 1) % 360;

            switch (State) {
                case STATE.GROWING:
                    scalingFactor += 0.1f;
                    if (scalingFactor >= 1.5f) { State = STATE.SHRINKING; }
                    break;
                case STATE.SHRINKING:
                    scalingFactor -= 0.1f;
                    if (scalingFactor <= 1.0f) { scalingFactor = 1.0f; State = STATE.NORMAL; }
                    break;
                case STATE.FADING_IN:
                    byte step = 2;
                    if (255 - opacity <= step) {
                        opacity = 255;
                        State = STATE.NORMAL;
                    }
                    else {
                        opacity += step;
                    }
                    break;
                case STATE.SOLVE_EXIT_STAGE_LEFT:
                    _solvedPosition.X -= _solvedVelocityX;
                    _solvedPosition.Y += _solvedVelocityY;
                    _solvedVelocityX += 2;
                    if (_solvedPosition.X < -Width) { State = STATE.DONE; }
                    break;
                case STATE.SOLVE_EXIT_STAGE_RIGHT:
                    _solvedPosition.X += _solvedVelocityX;
                    _solvedPosition.Y += _solvedVelocityY;
                    _solvedVelocityX += 2;
                    if (_solvedPosition.X > 1920) { State = STATE.DONE; }
                    break;
            }
        }
        #endregion

        public void Solve(PalindromePuzzle.SOLVE_FADEOUT_TYPE fadeoutType) {
            switch (fadeoutType) {
                case PalindromePuzzle.SOLVE_FADEOUT_TYPE.FLYOUT:
                    switch (r.Next(2)) {
                        case 0:
                            State = STATE.SOLVE_EXIT_STAGE_LEFT;
                            break;
                        case 1:
                            State = STATE.SOLVE_EXIT_STAGE_RIGHT;
                            break;
                    }
                    break;
            }
        }
    }
}