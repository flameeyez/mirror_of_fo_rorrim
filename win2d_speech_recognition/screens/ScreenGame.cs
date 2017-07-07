using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Media.SpeechRecognition;
using Windows.UI.Core;

namespace win2d_speech_recognition {
    static class ScreenGame {
        private static CanvasDevice _device;

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            Puzzle.Draw(args);
            BackgroundWords.Draw(args);
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            Puzzle.Update(args);
            BackgroundWords.Update(args);
        }

        public static void Initialize(CanvasDevice device) {
            _device = device;
        }

        public static void KeyDown(KeyEventArgs args) {
            switch (args.VirtualKey) {
                case Windows.System.VirtualKey.Space:
                    Puzzle.SolveCurrentPuzzle();
                    break;
                case Windows.System.VirtualKey.G:
                    Puzzle.CurrentPuzzle.HighlightObscurer();
                    break;
            }
        }
    }
}
