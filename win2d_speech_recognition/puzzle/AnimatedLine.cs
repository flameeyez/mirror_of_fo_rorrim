using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    class AnimatedLine {
        #region Static
        public static int SpaceBuffer = 100;
        #endregion

        #region Words
        private List<AnimatedWord> Words = new List<AnimatedWord>();
        public void AddWord(AnimatedWord word) {
            Words.Add(word);
        }
        public AnimatedWord GetWord(string strWord) {
            foreach (AnimatedWord word in Words) {
                if (word.Equals(strWord)) { return word; }
            }
            return null;
        }
        #endregion

        #region Position
        internal void SetPosition(int y) {
            // determine x position based on width
            int x = (1920 - Width) / 2;
            // set the position of each word
            foreach (AnimatedWord word in Words) {
                word.SetPosition(new Vector2(x, y));
                x += word.Width + AnimatedLine.SpaceBuffer;
            }
        }
        #endregion

        #region State
        public bool Done {
            get {
                foreach(AnimatedWord word in Words) {
                    if (!word.Done) { return false; }
                }
                return true;
            }
        }
        public void Solve(PalindromePuzzle.SOLVE_FADEOUT_TYPE fadeoutType) {
            foreach (AnimatedWord word in Words) {
                word.Solve(fadeoutType);
            }
        }
        public void Refresh() {
            foreach (AnimatedWord word in Words) {
                word.Refresh();
            }
        }
        public void FadeIn() {
            foreach(AnimatedWord word in Words) {
                word.FadeIn();
            }
        }
        #endregion

        #region Bounds
        public int Width {
            get {
                if (Words.Count == 0) { return 0; }
                return Words.Sum(x => x.Width) + (Words.Count - 1) * SpaceBuffer;
            }
        }
        public int Height {
            get {
                if (Words.Count == 0) { return 0; }
                return Words[0].Height;
            }
        }
        #endregion

        #region Draw / Update
        public void Draw(CanvasAnimatedDrawEventArgs args) {
            foreach (AnimatedWord word in Words) {
                word.Draw(args);
            }
        }
        public void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach (AnimatedWord word in Words) {
                word.Update(args);
            }
        }
        #endregion
    }
}
