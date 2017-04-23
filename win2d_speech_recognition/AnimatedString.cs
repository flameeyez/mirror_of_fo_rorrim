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
        private static int _lineBuffer = 20;
        private List<AnimatedLine> Lines = new List<AnimatedLine>();

        private Vector2 _position;
        private int _width;
        private int _height;

        public AnimatedString(CanvasDevice device, string str) {
            string[] words = str.Split(" ".ToCharArray());

            AnimatedLine line = new AnimatedLine();
            for (int i = 0; i < words.Length; i++) {
                AnimatedWord currentWord = new AnimatedWord(device, words[i]);
                if (line.Width + currentWord.Width >= 1800) {
                    Lines.Add(line);
                    line = new AnimatedLine();
                }

                line.AddWord(currentWord);
            }

            if (line.Width > 0) { Lines.Add(line); }

            _width = Lines.Max(x => x.Width);
            _height = Lines.Sum(x => x.Height) + (Lines.Count - 1) * _lineBuffer;
            _position = new Vector2((1920 - _width) / 2, (1080 - _height) / 2);

            // determine each line's position based on line width
            // determine vertical position based on number of lines and line height
        }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            Vector2 position = _position;
            foreach (AnimatedLine line in Lines) {
                line.Draw(args, position);
                position.Y += line.Height + _lineBuffer;
            }
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            foreach (AnimatedLine line in Lines) {
                line.Update(args);
            }
        }
    }
}
