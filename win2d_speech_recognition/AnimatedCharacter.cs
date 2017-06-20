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
    class AnimatedCharacter {
        public enum STATE {
            NORMAL,
            GROWING,
            SHRINKING,
            SOLVE_CONVERGE_TO_CENTER,
            SOLVE_FLOAT_UP,
            DONE
        }

        public enum SOLVED_HORIZONTAL_MOVEMENT {
            LEFT,
            RIGHT
        }

        public STATE State { get; set; }
        private SOLVED_HORIZONTAL_MOVEMENT HorizontalMovement { get; set; }
        public bool Done { get { return State == STATE.DONE; } }

        private static CanvasTextFormat HarryP;
        static AnimatedCharacter() {
            HarryP = new CanvasTextFormat();
            HarryP.FontFamily = "Harry P";
            HarryP.FontSize = 120;
            HarryP.WordWrapping = CanvasWordWrapping.NoWrap;
        }

        private int _rotation;
        private float scalingFactor = 1.0f;

        public int Width { get { return (int)_boundary.Width; } }
        public int Height { get { return (int)_boundary.Height; } }
        public char Character { get; set; }
        public CanvasTextLayout TextLayout { get; set; }
        private static Vector2 _shadowOffset = new Vector2(10, 10);
        private int loopCount = r.Next();
        private int _offsetY;
        private Vector2 _position;
        public Vector2 Position {
            get {
                return _position;
            }
            set {
                _position = value;
                _boundary = new Rect(_position.X, _position.Y, 100, 150);
                _solvedPosition.X = _position.X;
                _solvedPosition.Y = _position.Y;
            }
        }

        private Vector2 _solvedPosition;

        private Rect _boundary = new Rect(0, 0, 100, 150);
        private Color _color;
        private static Random r = new Random(DateTime.Now.Millisecond);

        public AnimatedCharacter(CanvasDevice device, char c, Color color) {
            Character = c;
            TextLayout = new CanvasTextLayout(device, c.ToString(), HarryP, 0, 0);
            _color = color;
            State = STATE.NORMAL;
        }

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
                    args.DrawingSession.Transform = Matrix3x2.CreateScale(scalingFactor, scalingFactor, new Vector2(Position.X + (float)TextLayout.LayoutBounds.Width / 2, Position.Y + (float)TextLayout.LayoutBounds.Height / 2));// * Matrix3x2.CreateRotation((float)(_rotation * Math.PI / 180), new Vector2(Position.X + (float)TextLayout.LayoutBounds.Width / 2, Position.Y + (float)TextLayout.LayoutBounds.Height / 2));
                    args.DrawingSession.DrawTextLayout(TextLayout, new Vector2(Position.X, Position.Y + _offsetY), drawColor);
                    args.DrawingSession.Transform = push;
                    break;
                case STATE.SOLVE_CONVERGE_TO_CENTER:
                case STATE.SOLVE_FLOAT_UP:
                    drawColor = Colors.Green;
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
                case STATE.SOLVE_CONVERGE_TO_CENTER:
                    if (_solvedPosition.X < 1920 / 2) {
                        _solvedPosition.X += 10;
                    }
                    else if (_solvedPosition.X > 1920 / 2) {
                        _solvedPosition.X -= 10;
                    }

                    if ((_solvedPosition.X > 1920 / 2 - (Width * 2))
                        && _solvedPosition.X < 1920 / 2 + (Width * 2)) {
                        State = STATE.SOLVE_FLOAT_UP;
                        switch (r.Next(2)) {
                            case 0:
                                HorizontalMovement = SOLVED_HORIZONTAL_MOVEMENT.LEFT;
                                break;
                            case 1:
                                HorizontalMovement = SOLVED_HORIZONTAL_MOVEMENT.RIGHT;
                                break;
                        }
                    }
                    break;
                case STATE.SOLVE_FLOAT_UP:
                    switch (HorizontalMovement) {
                        case SOLVED_HORIZONTAL_MOVEMENT.LEFT:
                            _solvedPosition.X -= 5 + r.Next(10);
                            if ((_solvedPosition.X < (1920 / 2) - 100) && r.Next(10) == 0) {
                                HorizontalMovement = SOLVED_HORIZONTAL_MOVEMENT.RIGHT;
                            }
                            break;
                        case SOLVED_HORIZONTAL_MOVEMENT.RIGHT:
                            _solvedPosition.X += 5 + r.Next(10);
                            if ((_solvedPosition.X > (1920 / 2) + 100) && r.Next(10) == 0) {
                                HorizontalMovement = SOLVED_HORIZONTAL_MOVEMENT.LEFT;
                            }
                            break;
                    }

                    _solvedPosition.Y -= 5;

                    if (_solvedPosition.Y < -Height) {
                        State = STATE.DONE;
                    }
                    break;
            }
        }

        public void Refresh() {
            loopCount = 0;
            State = STATE.NORMAL;
            _solvedPosition.X = Position.X;
            _solvedPosition.Y = Position.Y;
        }
    }
}