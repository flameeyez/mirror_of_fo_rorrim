using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace win2d_speech_recognition {
    class ScreenWinner : ScreenBase {
        private static PuzzleAnimatedString str;

        public ScreenWinner(CanvasDevice device) : base(device) {
            str = new PuzzleAnimatedString(_device, new string[] { "Winner!", "!renniW" }, true);
        }

        public override void Draw(CanvasAnimatedDrawEventArgs args) {
            BackgroundWords.Draw(args);
            if (str != null) { str.DrawMirrored(args); }
            SolveIcons.Draw(args);
        }

        public override void Update(CanvasAnimatedUpdateEventArgs args) {
            if (BackgroundWords.Count < 50) {
                BackgroundWords.EnqueueWinningWords(50);
            }

            BackgroundWords.Update(args);
            if (str != null) { str.Update(args); }
            PuzzleCollection.Update(args);
            SolveIcons.Update(args);
        }

        public override void Transition() {
            if (!_transitioning) {
                _transitioning = true;
                Music.Play(Music.Whoosh);
                str.Solve(PalindromePuzzle.SOLVE_FADEOUT_TYPE.FLYOUT);
            }
        }

        public override bool Done { get { return str != null && str.Done; } }
        public override void Reset() {
            str.Refresh();
            _transitioning = false;
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
