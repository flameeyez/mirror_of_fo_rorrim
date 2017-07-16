using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.System;

namespace win2d_speech_recognition {
    class ScreenIntro : ScreenBase {
        private static PuzzleAnimatedString str;

        public ScreenIntro(CanvasDevice device) : base(device) {
            str = new PuzzleAnimatedString(_device, new string[] { "Mirror of", "fo rorriM" }, true);
        }

        public override void Draw(CanvasAnimatedDrawEventArgs args) {
            BackgroundWords.Draw(args);
            if (str != null) { str.DrawMirrored(args); }
        }

        public override void Update(CanvasAnimatedUpdateEventArgs args) {
            if (BackgroundWords.Count < 40) {
                BackgroundWords.EnqueueRandomWords(40);
            }

            BackgroundWords.Update(args);
            if (str != null) { str.Update(args); }
        }

        public override void Transition() {
            if (!_transitioning) {
                _transitioning = true;
                PuzzleCollection.NewGame();
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
                case VirtualKey.Number1:
                    Statics.WinCount = 1;
                    break;
                case VirtualKey.Number2:
                    Statics.WinCount = 2;
                    break;
                case VirtualKey.Number3:
                    Statics.WinCount = 3;
                    break;
                case VirtualKey.Number4:
                    Statics.WinCount = 4;
                    break;
                case VirtualKey.Number5:
                    Statics.WinCount = 5;
                    break;
                case VirtualKey.Number6:
                    Statics.WinCount = 6;
                    break;
                case VirtualKey.Number7:
                    Statics.WinCount = 7;
                    break;
                case VirtualKey.Number8:
                    Statics.WinCount = 8;
                    break;
                case VirtualKey.Number9:
                    Statics.WinCount = 9;
                    break;
                case VirtualKey.Add:
                    Statics.WinCount++;
                    break;
                case VirtualKey.Subtract:
                    if (Statics.WinCount > 1) { Statics.WinCount--; }
                    break;
            }
        }
    }
}
