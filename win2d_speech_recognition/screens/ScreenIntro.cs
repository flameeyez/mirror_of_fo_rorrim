using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;

namespace win2d_speech_recognition {
    static class ScreenIntro {
        private static PuzzleAnimatedString str;

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            BackgroundWords.Draw(args);
            if (str != null) { str.Draw(args); }

            args.DrawingSession.DrawImage(bitmap: Images.Triwizard, destinationRectangle: new Rect(10, 10, Images.Triwizard.Bounds.Width / 6, Images.Triwizard.Bounds.Height / 6));
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            if (BackgroundWords.Count < 20) {
                BackgroundWords.EnqueueRandomWords(20);
            }

            BackgroundWords.Update(args);
            if (str != null) { str.Update(args); }
        }

        public static void Initialize(CanvasDevice device) {
            str = new PuzzleAnimatedString(device, new string[] { "Mirror of", "fo rorriM" }, true);
            PuzzleCollection.NewGame();
        }

        public static void Transition() {
            Music.Whoosh.Play();
            str.Solve(PalindromePuzzle.SOLVE_FADEOUT_TYPE.FLYOUT);
        }
        public static bool Done { get { return str != null && str.Done; } }
        public static void Reset() { str.Refresh(); }
    }
}
