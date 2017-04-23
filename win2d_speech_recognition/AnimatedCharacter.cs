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
        enum MOVE_STATE {
            MOVING,
            IDLE
        }
        private MOVE_STATE _moveState = MOVE_STATE.MOVING;
        private DateTime lastStart = DateTime.Now;
        private DateTime lastStop = DateTime.Now;
        private int lastMoveThreshold = 2000 + r.Next(2000);
        private int lastStopThreshold = 2000 + r.Next(2000);

        private static CanvasTextFormat HarryP;
        static AnimatedCharacter() {
            HarryP = new CanvasTextFormat();
            HarryP.FontFamily = "Harry P";
            HarryP.FontSize = 80;
            HarryP.WordWrapping = CanvasWordWrapping.NoWrap;
        }

        public int Width { get { return (int)_boundary.Width; } }
        public int Height { get { return (int)_boundary.Height; } }
        public char Character { get; set; }
        public CanvasTextLayout TextLayout { get; set; }

        private int _velocityX = 1;
        private int _velocityY = 1;
        private Vector2 _offset;
        private Rect _boundary;
        private static Random r = new Random(DateTime.Now.Millisecond);

        public AnimatedCharacter(CanvasDevice device, char c) {
            Character = c;
            TextLayout = new CanvasTextLayout(device, c.ToString(), HarryP, 0, 0);
            _offset = Vector2.Zero;
            _boundary = new Rect(0, 0, 100, 150);
        }

        public void Draw(CanvasAnimatedDrawEventArgs args, Vector2 position, Color color) {
            //args.DrawingSession.DrawRectangle(new Rect(position.X, position.Y, Width, Height), Colors.White);
            args.DrawingSession.DrawTextLayout(TextLayout, position + _offset, color);
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            switch (_moveState) {
                case MOVE_STATE.MOVING:
                    switch (r.Next(100)) {
                        case 0:
                            _velocityX += 1;
                            break;
                        case 1:
                            _velocityX -= 1;
                            break;
                        case 2:
                            _velocityY += 1;
                            break;
                        case 3:
                            _velocityY += -1;
                            break;
                        default:
                            //_velocityX = 0;
                            //_velocityY = 0;
                            break;
                    }

                    int threshold = 1;
                    _velocityX = Math.Min(threshold, Math.Max(-threshold, _velocityX));
                    _velocityY = Math.Min(threshold, Math.Max(-threshold, _velocityY));

                    double _offsetX = _offset.X + _velocityX;
                    if (_offsetX + TextLayout.LayoutBounds.Width > _boundary.Right) {
                        _offsetX = _boundary.Right - TextLayout.LayoutBounds.Width;
                        _velocityX = -_velocityX;

                        if (r.Next(10) == 0) {
                            _moveState = MOVE_STATE.IDLE;
                            lastStop = DateTime.Now;
                        }
                    }
                    else if (_offsetX < _boundary.Left) {
                        _offsetX = _boundary.Left;
                        _velocityX = -_velocityX;

                        if (r.Next(10) == 0) {
                            _moveState = MOVE_STATE.IDLE;
                            lastStop = DateTime.Now;
                        }
                    }

                    double _offsetY = _offset.Y + _velocityY;
                    if (_offsetY + TextLayout.LayoutBounds.Height > _boundary.Bottom) {
                        _offsetY = _boundary.Bottom - TextLayout.LayoutBounds.Height;
                        _velocityY = -_velocityY;

                        if (r.Next(10) == 0) {
                            _moveState = MOVE_STATE.IDLE;
                            lastStop = DateTime.Now;
                        }
                    }
                    else if (_offsetY < _boundary.Top) {
                        _offsetY = _boundary.Top;
                        _velocityY = -_velocityY;

                        if (r.Next(10) == 0) {
                            _moveState = MOVE_STATE.IDLE;
                            lastStop = DateTime.Now;
                        }
                    }

                    _offset = new Vector2((float)_offsetX, (float)_offsetY);

                    TimeSpan startDelta = DateTime.Now - lastStart;
                    if (startDelta.TotalMilliseconds > lastMoveThreshold && r.Next(100) == 0) {
                        _moveState = MOVE_STATE.IDLE;
                        lastStop = DateTime.Now;
                    }
                    break;
                case MOVE_STATE.IDLE:
                    TimeSpan stopDelta = DateTime.Now - lastStop;
                    if (stopDelta.TotalMilliseconds > lastStopThreshold && r.Next(100) == 0) {
                        _moveState = MOVE_STATE.MOVING;
                        lastStart = DateTime.Now;
                    }
                    break;
            }
        }
    }
}
