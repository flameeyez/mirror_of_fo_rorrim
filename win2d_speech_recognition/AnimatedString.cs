using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    class AnimatedString {
        private List<AnimatedCharacter> _characters = new List<AnimatedCharacter>();
        public AnimatedString(CanvasDevice device, string str, Vector2 position) {
            float x = position.X;
            foreach (char c in str) {
                if (c != ' ') {
                    _characters.Add(new AnimatedCharacter(c, new Vector2(x, position.Y)));
                }
                x += 90;
            }
        }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            foreach (AnimatedCharacter c in _characters) {
                c.Draw(args);
            }
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach (AnimatedCharacter c in _characters) {
                c.Update(args);
            }
        }
    }
}
