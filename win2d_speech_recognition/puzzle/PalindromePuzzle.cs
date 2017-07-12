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
        public enum SOLVE_FADEOUT_TYPE {
            SPIRAL_UP,
            FLYOUT
        }

        private static Random r = new Random(DateTime.Now.Millisecond);

        private string _puzzle;
        public string Puzzle { get { return _puzzle; } }
        private string _obscurer;
        public string Obscurer { get { return _obscurer; } }
        private string _solution;
        public string Solution { get { return _solution; } }

        public bool Solved { get; set; }
        public bool Done { get { return AnimatedString.Done; } }

        private PuzzleAnimatedString AnimatedString;

        private bool _highlightAnswer = false;
        private TimeSpan _timeSinceLastHighlight;
        private static TimeSpan _highlightThreshold = new TimeSpan(0, 0, 1);//0);
        private static TimeSpan _initialHighlightThreshold = new TimeSpan(0, 0, 1);//30);

        public PalindromePuzzle(CanvasDevice device, string puzzle, string obscurer, string solution) {
            _puzzle = puzzle;
            _obscurer = obscurer;
            _solution = solution;
            AnimatedString = new PuzzleAnimatedString(device, puzzle);
            Refresh();
        }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            AnimatedString.Draw(args);
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            AnimatedString.Update(args);
            if (!Solved) {
                _timeSinceLastHighlight += args.Timing.ElapsedTime;
                if (!_highlightAnswer) {
                    if (_timeSinceLastHighlight > _initialHighlightThreshold) {
                        Debug.AddTimedString("Initial highlight.");
                        _highlightAnswer = true;
                        HighlightObscurer();
                        _timeSinceLastHighlight = TimeSpan.Zero;
                    }
                } 
                else if (_timeSinceLastHighlight > _highlightThreshold) {
                    Debug.AddTimedString("Subsequent highlight.");
                    HighlightObscurer();
                    _timeSinceLastHighlight = TimeSpan.Zero;
                }
            }
        }

        public void HighlightObscurer() {
            AnimatedString.HighlightWord(Obscurer);
        }

        public void Refresh() {
            Solved = false;
            _highlightAnswer = false;
            _timeSinceLastHighlight = TimeSpan.Zero;            
            AnimatedString.Refresh();
        }

        public void Solve() {
            Solved = true;

            // select random fadeout type
            Array values = Enum.GetValues(typeof(SOLVE_FADEOUT_TYPE));
            SOLVE_FADEOUT_TYPE fadeoutType = (SOLVE_FADEOUT_TYPE)values.GetValue(r.Next(values.Length));
            // AnimatedString.Solve(fadeoutType);
            AnimatedString.Solve(SOLVE_FADEOUT_TYPE.FLYOUT);
        }

        public void FadeIn() {
            AnimatedString.FadeIn();
        }
    }
}
