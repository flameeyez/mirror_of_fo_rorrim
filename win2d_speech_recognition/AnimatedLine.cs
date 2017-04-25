using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    class AnimatedLine {
        public static int SpaceBuffer = 100;
        private List<AnimatedWord> Words = new List<AnimatedWord>();
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

        public void AddWord(AnimatedWord word) {
            Words.Add(word);
        }

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

        internal void SetPosition(int y) {
            // determine x position based on width
            int x = (1920 - Width) / 2;
            // set the position of each word
            foreach (AnimatedWord word in Words) {
                word.SetPosition(new Vector2(x, y));
                x += word.Width + AnimatedLine.SpaceBuffer;
            }
        }
    }
}
