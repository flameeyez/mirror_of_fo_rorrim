using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Playback;
using Windows.Media.SpeechRecognition;
using Windows.UI.Core;
using Windows.System;

namespace win2d_speech_recognition {
    class ScreenGame : ScreenBase {
        public ScreenGame(CanvasDevice device) : base(device) { }

        public override bool Done { get { return PuzzleCollection.Done && SolveIcons.Done; } }

        public override void Draw(CanvasAnimatedDrawEventArgs args) {
            PuzzleCollection.Draw(args);
            BackgroundWords.Draw(args);
            SolveIcons.Draw(args);
        }

        public override void Update(CanvasAnimatedUpdateEventArgs args) {
            PuzzleCollection.Update(args);
            BackgroundWords.Update(args);
            SolveIcons.Update(args);
        }

        public override void Reset() {
            //throw new NotImplementedException();
        }

        public override void Transition() {
            PuzzleCollection.SolveCurrentPuzzle();
        }

        public override void KeyDown(VirtualKey vk) {
            switch (vk) {
                case VirtualKey.Space:
                    Transition();
                    break;
            }
        }
    }
}
