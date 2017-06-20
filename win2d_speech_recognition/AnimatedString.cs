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
    class AnimatedString {
        public static Color DarkColor = Color.FromArgb(255, 150, 150, 150);
        public static Color LightColor = Color.FromArgb(255, 255, 255, 255);

        public bool Done {
            get {
                foreach(AnimatedLine line in Lines) {
                    if (!line.Done) { return false; }
                }
                return true;
            }
        }

        protected static int _lineBuffer = 20;
        protected List<AnimatedLine> Lines = new List<AnimatedLine>();

        public AnimatedString(CanvasDevice device, string str) {
            string[] words = str.Split(" ".ToCharArray());

            AnimatedLine line = new AnimatedLine();
            for (int i = 0; i < words.Length; i++) {
                Color color = i % 2 == 0 ? DarkColor : LightColor;
                AnimatedWord currentWord = new AnimatedWord(device, words[i], color);
                if (line.Width + currentWord.Width >= 1800) {
                    Lines.Add(line);
                    line = new AnimatedLine();
                }
                line.AddWord(currentWord);
            }

            if (line.Width > 0) { Lines.Add(line); }

            int totalHeight = Lines.Sum(x => x.Height) + (Lines.Count - 1) * _lineBuffer;
            int y = (1080 - totalHeight) / 2;

            // center each line
            foreach (AnimatedLine l in Lines) {
                l.SetPosition(y: y);
                y += l.Height + _lineBuffer;
            }
        }

        public virtual void Draw(CanvasAnimatedDrawEventArgs args) {
            foreach (AnimatedLine line in Lines) {
                line.Draw(args);
            }
        }

        public virtual void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach (AnimatedLine line in Lines) {
                line.Update(args);
            }
        }

        public void HighlightWord(string str) {
            foreach (AnimatedLine line in Lines) {
                AnimatedWord word = line.GetWord(str);
                if (word != null) {
                    word.Highlight();
                }
            }
        }

        public void Solve() {
            foreach(AnimatedLine line in Lines) {
                line.Solve();
            }
        }

        public void Refresh() {
            foreach(AnimatedLine line in Lines) {
                line.Refresh();
            }
        }
    }
}
