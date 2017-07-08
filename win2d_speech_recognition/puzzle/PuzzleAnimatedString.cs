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
    class PuzzleAnimatedString {
        #region Static
        public static Color DarkColor = Color.FromArgb(255, 150, 150, 150);
        public static Color LightColor = Color.FromArgb(255, 255, 255, 255);
        protected static int _lineBuffer = 20;
        #endregion

        #region Lines
        protected List<PuzzleAnimatedLine> Lines = new List<PuzzleAnimatedLine>();
        #endregion

        #region Constructor
        public PuzzleAnimatedString(CanvasDevice device, string[] str, bool bUseLargeFont = false) {
            foreach (string s in str) {
                string[] words = s.Split(" ".ToCharArray());
                PuzzleAnimatedLine line = new PuzzleAnimatedLine();

                for (int i = 0; i < words.Length; i++) {
                    Color color = i % 2 == 0 ? DarkColor : LightColor;
                    PuzzleAnimatedWord currentWord = new PuzzleAnimatedWord(device, words[i], color, bUseLargeFont);
                    line.AddWord(currentWord);
                }

                Lines.Add(line);
            }

            int totalHeight = Lines.Sum(x => x.Height) + (Lines.Count - 1) * _lineBuffer;
            int y = (1080 - totalHeight) / 2;

            // center each line
            foreach (PuzzleAnimatedLine l in Lines) {
                l.SetPosition(y: y);
                y += l.Height + _lineBuffer;
            }
        }

        public PuzzleAnimatedString(CanvasDevice device, string str, bool bUseLargeFont = false) {
            string[] words = str.Split(" ".ToCharArray());

            PuzzleAnimatedLine line = new PuzzleAnimatedLine();
            for (int i = 0; i < words.Length; i++) {
                Color color = i % 2 == 0 ? DarkColor : LightColor;
                PuzzleAnimatedWord currentWord = new PuzzleAnimatedWord(device, words[i], color, bUseLargeFont);
                if (line.Width + currentWord.Width >= 1800) {
                    Lines.Add(line);
                    line = new PuzzleAnimatedLine();
                }
                line.AddWord(currentWord);
            }

            if (line.Width > 0) { Lines.Add(line); }

            int totalHeight = Lines.Sum(x => x.Height) + (Lines.Count - 1) * _lineBuffer;
            int y = (1080 - totalHeight) / 2;

            // center each line
            foreach (PuzzleAnimatedLine l in Lines) {
                l.SetPosition(y: y);
                y += l.Height + _lineBuffer;
            }
        }
        #endregion

        #region Draw / Update
        public virtual void Draw(CanvasAnimatedDrawEventArgs args) {
            foreach (PuzzleAnimatedLine line in Lines) {
                line.Draw(args);
            }
        }
        public virtual void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach (PuzzleAnimatedLine line in Lines) {
                line.Update(args);
            }
        }
        #endregion

        #region State
        public void HighlightWord(string str) {
            foreach (PuzzleAnimatedLine line in Lines) {
                PuzzleAnimatedWord word = line.GetWord(str);
                if (word != null) {
                    word.Highlight();
                }
            }
        }
        public void Solve(PalindromePuzzle.SOLVE_FADEOUT_TYPE fadeoutType) {
            foreach (PuzzleAnimatedLine line in Lines) {
                line.Solve(fadeoutType);
            }
        }
        public void Refresh() {
            foreach (PuzzleAnimatedLine line in Lines) {
                line.Refresh();
            }
        }
        public bool Done {
            get {
                foreach (PuzzleAnimatedLine line in Lines) {
                    if (!line.Done) { return false; }
                }
                return true;
            }
        }
        public void FadeIn() {
            foreach (PuzzleAnimatedLine line in Lines) {
                line.FadeIn();
            }
        }
        #endregion
    }
}
