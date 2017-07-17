using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace mirror_of_fo_rorrim {
    enum SOLVE_ICON_STATE {
        HIDDEN,
        FADING_IN,
        NORMAL
    }

    class SolveIcon {
        private SOLVE_ICON_STATE _state;
        public SOLVE_ICON_STATE State { get { return _state; } }

        private Vector2 _position;
        private float _opacity;
        private int _seed;

        public SolveIcon(Vector2 position) {
            _position = position;
            _opacity = 0.1f;
            _state = SOLVE_ICON_STATE.HIDDEN;
            _seed = Statics.r.Next(1000);
        }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            if(_opacity > 0.0f) {
                args.DrawingSession.DrawImage(Images.Wand, new Rect(_position.X, _position.Y + (int)(7 * Math.Sin(++_seed * 0.05)), Images.Wand.Bounds.Width, Images.Wand.Bounds.Height), Images.Wand.Bounds, _opacity);
            }            
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            switch (_state) {
                case SOLVE_ICON_STATE.FADING_IN:
                    _opacity += 0.02f;
                    if (_opacity >= 1.0f) {
                        _opacity = 1.0f;
                        _state = SOLVE_ICON_STATE.NORMAL;
                    }
                    break;
            }
        }

        public void FadeIn() {
            _state = SOLVE_ICON_STATE.FADING_IN;
        }
    }
}
