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
    class PuzzleAnimatedWord {
        #region Position
        private int loopCount;
        internal void SetPosition(Vector2 position) {
            Vector2 currentPosition = position;
            foreach (PuzzleAnimatedCharacter c in _characters) {
                c.Position = currentPosition;
                currentPosition.X += c.Width;
            }
        }
        #endregion

        #region State
        public bool Done {
            get {
                foreach(PuzzleAnimatedCharacter c in _characters) {
                    if (!c.Done) { return false; }
                }
                return true;
            }
        }
        public void Highlight() {
            foreach (PuzzleAnimatedCharacter c in _characters) {
                if (char.IsLetter(c.Character)) {
                    c.State = PuzzleAnimatedCharacter.STATE.GROWING;
                }
            }
        }
        public void Solve(PalindromePuzzle.SOLVE_FADEOUT_TYPE fadeoutType) {
            foreach (PuzzleAnimatedCharacter c in _characters) {
                c.Solve(fadeoutType);
            }
        }
        public void FadeIn() {
            foreach(PuzzleAnimatedCharacter c in _characters) {
                c.State = PuzzleAnimatedCharacter.STATE.FADING_IN;
            }
        }
        #endregion

        #region Static
        private static Random r = new Random((int)DateTime.Now.Ticks);
        #endregion

        #region Characters
        private List<PuzzleAnimatedCharacter> _characters = new List<PuzzleAnimatedCharacter>();
        #endregion

        #region Bounds
        public int Width { get { return _characters.Sum(x => x.Width); } }
        public int Height {
            get {
                if (_characters.Count == 0) { return 0; }
                return _characters[0].Height;
            }
        }
        #endregion

        #region Constructor / Initialization
        public PuzzleAnimatedWord(CanvasDevice device, string word, Color color, bool bUseLargeFont = false) {
            foreach (char c in word) {
                _characters.Add(new PuzzleAnimatedCharacter(device, c, color, bUseLargeFont));
            }
        }
        public void Refresh() {
            foreach (PuzzleAnimatedCharacter c in _characters) {
                c.Refresh();
            }
        }
        #endregion

        #region Draw / Update
        public void Draw(CanvasAnimatedDrawEventArgs args) {
            for (int i = 0; i < _characters.Count; i++) {
                _characters[i].Draw(args);
            }
        }
        public void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach (PuzzleAnimatedCharacter c in _characters) {
                c.Update(args);
            }

            loopCount++;
        }
        #endregion

        #region ToString / Equality
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (PuzzleAnimatedCharacter c in _characters) {
                if (char.IsLetter(c.Character)) {
                    sb.Append(c.Character);
                }
            }
            return sb.ToString();
        }
        public bool Equals(string str) {
            return ToString().ToLower().Equals(str);
        }
        #endregion
    }
}
