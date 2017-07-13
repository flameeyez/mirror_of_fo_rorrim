using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    static class ScreenWinner {
        private static CanvasDevice _device;
        private static PuzzleAnimatedString str;

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            BackgroundWords.Draw(args);
            if (str != null) { str.Draw(args); }
            SolveIcons.Draw(args);
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            if (BackgroundWords.Count < 50) {
                BackgroundWords.EnqueueWinningWords(50);
            }

            BackgroundWords.Update(args);
            if (str != null) { str.Update(args); }
            PuzzleCollection.Update(args);
            SolveIcons.Update(args);
        }

        public static void Initialize(CanvasDevice device) {
            _device = device;
            str = new PuzzleAnimatedString(_device, new string[] { "Winner!", "!renniW" }, true);
        }

        public static void Transition() {
            Music.Whoosh.Play();
            str.Solve(PalindromePuzzle.SOLVE_FADEOUT_TYPE.FLYOUT);
        }

        public static bool Done { get { return str != null && str.Done; } }
        public static void Reset() {
            str.Refresh();
        }
    }
}
