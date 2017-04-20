using Microsoft.Graphics.Canvas;
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

        public char Character { get; set; }
        public CanvasBitmap Bitmap { get; set; }

        private int _velocityX = 1;
        private int _velocityY = 1;
        private Vector2 _position;
        private Rect _boundary;
        private static Random r = new Random(DateTime.Now.Millisecond);

        public AnimatedCharacter(char c, Vector2 position) {
            Character = c;
            Bitmap = CharacterDictionary.Entry[c];
            _position = position;

            _boundary = new Rect(position.X, position.Y, 100, 150);
        }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            args.DrawingSession.DrawRectangle(_boundary, Colors.White);
            args.DrawingSession.DrawImage(Bitmap, _position);
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

                    double _positionX = _position.X + _velocityX;
                    if (_positionX + Bitmap.Bounds.Width > _boundary.Right) {
                        _positionX = _boundary.Right - Bitmap.Bounds.Width;
                        _velocityX = -_velocityX;

                        if(r.Next(10) == 0) {
                            _moveState = MOVE_STATE.IDLE;
                            lastStop = DateTime.Now;
                        }
                    }
                    else if (_positionX < _boundary.Left) {
                        _positionX = _boundary.Left;
                        _velocityX = -_velocityX;

                        if (r.Next(10) == 0) {
                            _moveState = MOVE_STATE.IDLE;
                            lastStop = DateTime.Now;
                        }
                    }

                    double _positionY = _position.Y + _velocityY;
                    if (_positionY + Bitmap.Bounds.Height > _boundary.Bottom) {
                        _positionY = _boundary.Bottom - Bitmap.Bounds.Height;
                        _velocityY = -_velocityY;

                        if (r.Next(10) == 0) {
                            _moveState = MOVE_STATE.IDLE;
                            lastStop = DateTime.Now;
                        }
                    }
                    else if (_positionY < _boundary.Top) {
                        _positionY = _boundary.Top;
                        _velocityY = -_velocityY;

                        if (r.Next(10) == 0) {
                            _moveState = MOVE_STATE.IDLE;
                            lastStop = DateTime.Now;
                        }
                    }

                    _position = new Vector2((float)_positionX, (float)_positionY);

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
