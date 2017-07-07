using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace win2d_speech_recognition {
    static class Puzzle {
        private static List<PalindromePuzzle> Puzzles = new List<PalindromePuzzle>();
        private static int nIndex = 0;

        public static PalindromePuzzle CurrentPuzzle { get { return Puzzles[nIndex]; } }
        public static void Initialize(CanvasDevice device) {
            Puzzles.Add(new PalindromePuzzle(device, "Sore was I ere I noticed Eros.", "noticed", "saw"));
            Puzzles.Add(new PalindromePuzzle(device, "A man, a plan, a passage, Panama", "passage", "canal"));
            Puzzles.Add(new PalindromePuzzle(device, "Never a foot too distant, even.", "distant", "far"));
        }

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            CurrentPuzzle.Draw(args);
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            if (CurrentPuzzle.Done) { NextPuzzle(); }
            else { CurrentPuzzle.Update(args); }
        }

        private static void NextPuzzle() {
            nIndex = (nIndex + 1) % Puzzles.Count;
            CurrentPuzzle.Refresh();
        }

        public static void SolveCurrentPuzzle() {
            CurrentPuzzle.Solve();

            // if previous whoosh is still playing, reset position to beginning
            if (Music.Whoosh.PlaybackSession.PlaybackState == MediaPlaybackState.Playing) {
                Music.Whoosh.PlaybackSession.Position = TimeSpan.Zero;
            }
            else {
                Music.Whoosh.Play();
            }
        }
    }
}
