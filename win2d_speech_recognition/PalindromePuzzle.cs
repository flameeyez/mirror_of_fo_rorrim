using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    class PalindromePuzzle {
        private string _puzzle;
        public string Puzzle { get { return _puzzle; } }
        private string _solution;
        public string Solution { get { return _solution; } }

        private AnimatedString AnimatedString;

        public PalindromePuzzle(CanvasDevice device, string puzzle, string solution) {
            _puzzle = puzzle;
            _solution = solution;
            AnimatedString = new AnimatedString(device, puzzle, solution);
        }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            AnimatedString.Draw(args);
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            AnimatedString.Update(args);
        }
    }
}
