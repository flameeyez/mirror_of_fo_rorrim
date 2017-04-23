using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    class AnimatedLine {
        private static int _spaceBufferWidth = 100;
        private List<AnimatedWord> Words = new List<AnimatedWord>();
        public int Width {
            get {
                if (Words.Count == 0) { return 0; }
                return Words.Sum(x => x.Width) + (Words.Count - 1) * _spaceBufferWidth;
            }
        }

        public int Height {
            get {
                if(Words.Count == 0) { return 0;}
                return Words[0].Height;
            }
        }

        public void AddWord(AnimatedWord word) {
            Words.Add(word);
        }

        public void Draw(CanvasAnimatedDrawEventArgs args, Vector2 position) {
            Vector2 wordPosition = position;
            foreach(AnimatedWord word in Words) {
                word.Draw(args, wordPosition);
                wordPosition.X += word.Width + _spaceBufferWidth;
            }
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach(AnimatedWord word in Words) {
                word.Update(args);
            }
        }
    }
}
