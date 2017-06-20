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
        private int loopCount;

        public bool Done {
            get {
                foreach(AnimatedCharacter c in _characters) {
                    if (!c.Done) { return false; }
                }
                return true;
            }
        }

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

        public AnimatedWord(CanvasDevice device, string word, Color color) {
            foreach (char c in word) {
                _characters.Add(new AnimatedCharacter(device, c, color));
            }
        }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            for (int i = 0; i < _characters.Count; i++) {
                _characters[i].Draw(args);
            }
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach (AnimatedCharacter c in _characters) {
                c.Update(args);
            }

            loopCount++;
        }

        internal void SetPosition(Vector2 position) {
            Vector2 currentPosition = position;
            foreach (AnimatedCharacter c in _characters) {
                c.Position = currentPosition;
                currentPosition.X += c.Width;
            }
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (AnimatedCharacter c in _characters) {
                if (char.IsLetter(c.Character)) {
                    sb.Append(c.Character);
                }
            }
            return sb.ToString();
        }

        public bool Equals(string str) {
            return ToString().Equals(str);
        }

        public void Highlight() {
            foreach (AnimatedCharacter c in _characters) {
                if (char.IsLetter(c.Character)) {
                    c.State = AnimatedCharacter.STATE.GROWING;
                }
            }
        }

        public void Solve() {
            foreach(AnimatedCharacter c in _characters) {
                c.State = AnimatedCharacter.STATE.SOLVE_CONVERGE_TO_CENTER;
            }
        }

        public void Refresh() {
            foreach(AnimatedCharacter c in _characters) {
                c.Refresh();
            }
        }
    }
}
