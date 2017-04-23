using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace win2d_speech_recognition {
    class AnimatedWord {
        private static Random r;
        private Color _color;

        static AnimatedWord() {
            r = new Random((int)DateTime.Now.Ticks);
        }

        private List<AnimatedCharacter> _characters = new List<AnimatedCharacter>();
        public int Width { get { return _characters.Sum(x => x.Width); } }
        public int Height {
            get {
                if (_characters.Count == 0) { return 0; }
                return _characters[0].Height;
            }
        }

        public AnimatedWord(CanvasDevice device, string word) {
            byte red = (byte)(100 + r.Next(155));
            byte green = (byte)(100 + r.Next(155));
            byte blue = (byte)(100 + r.Next(155));
            _color = Color.FromArgb(255, red, green, blue);

            foreach (char c in word) {
                _characters.Add(new AnimatedCharacter(device, c));
            }
        }

        public void Draw(CanvasAnimatedDrawEventArgs args, Vector2 position) {
            Vector2 charPosition = position;
            foreach (AnimatedCharacter c in _characters) {
                c.Draw(args, charPosition, _color);
                charPosition.X += c.Width;
            }
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach (AnimatedCharacter c in _characters) {
                c.Update(args);
            }
        }
    }
}
