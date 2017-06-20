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
        private string _obscurer;
        public string Obscurer { get { return _obscurer; } }
        private string _solution;
        public string Solution { get { return _solution; } }

        public bool Solved { get; set; }
        public bool Done { get { return AnimatedString.Done; } }

        private AnimatedString AnimatedString;
        private TimeSpan _timeSinceLastHighlight;
        private static TimeSpan _highlightThreshold = new TimeSpan(0, 0, 10);

        public PalindromePuzzle(CanvasDevice device, string puzzle, string obscurer, string solution) {
            _puzzle = puzzle;
            _obscurer = obscurer;
            _solution = solution;
            AnimatedString = new AnimatedString(device, puzzle);
            Solved = false;
        }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            AnimatedString.Draw(args);
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            AnimatedString.Update(args);
            if (!Solved) {
                _timeSinceLastHighlight += args.Timing.ElapsedTime;
                if (_timeSinceLastHighlight > _highlightThreshold) {
                    HighlightObscurer();
                    _timeSinceLastHighlight = TimeSpan.Zero;
                }
            }
        }

        public void HighlightObscurer() {
            AnimatedString.HighlightWord(Obscurer);
        }

        public void Refresh() {
            _timeSinceLastHighlight = TimeSpan.Zero;
            AnimatedString.Refresh();
            Solved = false;
        }

        public void Solve() {
            Solved = true;
            AnimatedString.Solve();
        }
    }
}
