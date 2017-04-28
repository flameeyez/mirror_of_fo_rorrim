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
        //enum MOVE_STATE {
        //    MOVING,
        //    IDLE
        //}
        //private MOVE_STATE _moveState = MOVE_STATE.MOVING;
        //private DateTime lastStart = DateTime.Now;
        //private DateTime lastStop = DateTime.Now;
        //private int lastMoveThreshold = 2000 + r.Next(2000);
        //private int lastStopThreshold = 2000 + r.Next(2000);
        // private int rotation = 0;
        //private int _velocityX = 1;
        //private int _velocityY = 1;

        private static CanvasTextFormat HarryP;
        static AnimatedCharacter() {
            HarryP = new CanvasTextFormat();
            HarryP.FontFamily = "Harry P";
            HarryP.FontSize = 120;
            HarryP.WordWrapping = CanvasWordWrapping.NoWrap;
        }

        private int _rotation;

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
            }
        }
        private Rect _boundary = new Rect(0, 0, 100, 150);
        private Color _color;
        private static Random r = new Random(DateTime.Now.Millisecond);

        public AnimatedCharacter(CanvasDevice device, char c, Color color) {
            Character = c;
            TextLayout = new CanvasTextLayout(device, c.ToString(), HarryP, 0, 0);
            _color = color;
        }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            //Matrix3x2 push = args.DrawingSession.Transform;
            //args.DrawingSession.Transform = Matrix3x2.CreateScale(-1, 1, new Vector2(Position.X + (float)TextLayout.LayoutBounds.Width / 2, Position.Y + (float)TextLayout.LayoutBounds.Height / 2)) * Matrix3x2.CreateRotation((float)(_rotation * Math.PI / 180), new Vector2(Position.X + (float)TextLayout.LayoutBounds.Width / 2, Position.Y + (float)TextLayout.LayoutBounds.Height / 2));
            args.DrawingSession.DrawTextLayout(TextLayout, new Vector2(Position.X, Position.Y + _offsetY), _color);
            //args.DrawingSession.Transform = push;
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            _offsetY = (int)(7 * Math.Sin(++loopCount * 0.05));
            //int y = (int)(_position.Y + 200 * Math.Sin(i * .20 + loopCount * 0.01));
            _rotation = (_rotation + 1) % 360;
        }
    }
}

//switch (_moveState) {
//    case MOVE_STATE.MOVING:
//        switch (r.Next(100)) {
//            case 0:
//                _velocityX += 1;
//                break;
//            case 1:
//                _velocityX -= 1;
//                break;
//            case 2:
//                _velocityY += 1;
//                break;
//            case 3:
//                _velocityY += -1;
//                break;
//            default:
//                //_velocityX = 0;
//                //_velocityY = 0;
//                break;
//        }

//        int threshold = 1;
//        _velocityX = Math.Min(threshold, Math.Max(-threshold, _velocityX));
//        _velocityY = Math.Min(threshold, Math.Max(-threshold, _velocityY));

//        double _offsetX = _offset.X + _velocityX;
//        if (_offsetX + TextLayout.LayoutBounds.Width > _boundary.Right) {
//            _offsetX = _boundary.Right - TextLayout.LayoutBounds.Width;
//            _velocityX = -_velocityX;

//            if (r.Next(10) == 0) {
//                _moveState = MOVE_STATE.IDLE;
//                lastStop = DateTime.Now;
//            }
//        }
//        else if (_offsetX < _boundary.Left) {
//            _offsetX = _boundary.Left;
//            _velocityX = -_velocityX;

//            if (r.Next(10) == 0) {
//                _moveState = MOVE_STATE.IDLE;
//                lastStop = DateTime.Now;
//            }
//        }

//        double _offsetY = _offset.Y + _velocityY;
//        if (_offsetY + TextLayout.LayoutBounds.Height > _boundary.Bottom) {
//            _offsetY = _boundary.Bottom - TextLayout.LayoutBounds.Height;
//            _velocityY = -_velocityY;

//            if (r.Next(10) == 0) {
//                _moveState = MOVE_STATE.IDLE;
//                lastStop = DateTime.Now;
//            }
//        }
//        else if (_offsetY < _boundary.Top) {
//            _offsetY = _boundary.Top;
//            _velocityY = -_velocityY;

//            if (r.Next(10) == 0) {
//                _moveState = MOVE_STATE.IDLE;
//                lastStop = DateTime.Now;
//            }
//        }

//        _offset = new Vector2((float)_offsetX, (float)_offsetY);

//        TimeSpan startDelta = DateTime.Now - lastStart;
//        if (startDelta.TotalMilliseconds > lastMoveThreshold && r.Next(100) == 0) {
//            _moveState = MOVE_STATE.IDLE;
//            lastStop = DateTime.Now;
//        }
//        break;
//    case MOVE_STATE.IDLE:
//        TimeSpan stopDelta = DateTime.Now - lastStop;
//        if (stopDelta.TotalMilliseconds > lastStopThreshold && r.Next(100) == 0) {
//            _moveState = MOVE_STATE.MOVING;
//            lastStart = DateTime.Now;
//        }
//        break;
//}