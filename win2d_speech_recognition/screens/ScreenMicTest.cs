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
    static class ScreenMicTest {
        private static CanvasDevice _device;
        private static PuzzleAnimatedString str;
        private static bool _transitioning;

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            BackgroundWords.Draw(args);
            if (str != null) { str.Draw(args); }
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            if (BackgroundWords.Count < 20) {
                BackgroundWords.EnqueueRandomMicTestWords(20);
            }

            BackgroundWords.Update(args);
            if (str != null) { str.Update(args); }
        }

        public static void Initialize(CanvasDevice device) {
            _device = device;
            str = new PuzzleAnimatedString(_device, new string[] { "Microphone test!", "Say something." }, false);
            _transitioning = false;
        }

        public static void Transition() {
            if (!_transitioning) {
                _transitioning = true;
                Music.Play(Music.Whoosh);
                str.Solve(PalindromePuzzle.SOLVE_FADEOUT_TYPE.FLYOUT);
            }
        }
        public static bool Done { get { return str != null && str.Done; } }
        public static void Reset() {
            str.Refresh();
            _transitioning = false;
        }
    }
}
