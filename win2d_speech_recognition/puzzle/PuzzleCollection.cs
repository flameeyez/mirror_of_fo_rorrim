using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;

namespace win2d_speech_recognition {
    static class PuzzleCollection {
        private static List<PalindromePuzzle> AllPuzzles = new List<PalindromePuzzle>();
        // private static int nIndex = 0;
        private static List<int> _lastUsed = new List<int>();
        private static List<PalindromePuzzle> CurrentPuzzles = new List<PalindromePuzzle>();
        private static int _solveCount = 0;
        private static int _winCount = 3;
        public static bool Winner { get { return _solveCount == _winCount; } }

        public static PalindromePuzzle CurrentPuzzle {
            get {
                if (CurrentPuzzles.Count == 0) { return null; }
                return CurrentPuzzles[0];
            }
        }

        public static void Initialize(CanvasDevice device) {
            AllPuzzles.Add(new PalindromePuzzle(device, "Sore was I ere I noticed Eros.", "noticed", "saw"));
            AllPuzzles.Add(new PalindromePuzzle(device, "A man, a plan, a passage, Panama", "passage", "canal"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Never a foot too distant, even.", "distant", "far"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Red flowers run no risk, sir, on nurses order.", "flowers", "roses"));
            AllPuzzles.Add(new PalindromePuzzle(device, "I, man, am regal; a Frenchman am I.", "frenchman", "german"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Tracy, no worrying in a pony-cart.", "worrying", "panic"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Egad! Loretta has Adams as angry as a hatter. Old age!", "angry", "mad"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Resume so pacific a stance, muser.", "stance", "pose"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Marge let a moody baby doom a letter.", "letter", "telegram"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Noel notices Leon.", "notices", "sees"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Anne, I vote more cars race Tuscany to Vienna.", "tuscany", "rome"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Too cold to hoot.", "cold", "hot"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Nurse, save rare vases, walk!", "walk", "run"));
            AllPuzzles.Add(new PalindromePuzzle(device, "No mists or snow, Simon.", "snow", "frost"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Bottom step, Sara's pet spot.", "bottom", "top"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Evil is a moniker of a foeman, as I live.", "moniker", "name"));
        }

        public static void NewGame() {
            _solveCount = 0;
            CurrentPuzzles.Clear();

            for (int i = 0; i < 3; i++) {
                int index = Statics.r.Next(AllPuzzles.Count);
                while (_lastUsed.Contains(index)) { index = Statics.r.Next(AllPuzzles.Count); }

                if (_lastUsed.Count >= 10) { _lastUsed.RemoveAt(0); }
                _lastUsed.Add(index);

                CurrentPuzzles.Add(AllPuzzles[index]);
            }
        }

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            if (CurrentPuzzle == null) { return; }

            CurrentPuzzle.Draw(args);
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            if (CurrentPuzzle == null) { return; }

            if (CurrentPuzzle.Done) { NextPuzzle(); }
            else { CurrentPuzzle.Update(args); }
        }

        private static void NextPuzzle() {
            if (CurrentPuzzles.Count == 0) { return; }
            CurrentPuzzles.RemoveAt(0);

            if (CurrentPuzzle != null) {
                CurrentPuzzle.Refresh();
            }

            //nIndex = (nIndex + 1) % AllPuzzles.Count;
        }

        public static void SolveCurrentPuzzle() {
            if (CurrentPuzzle == null) { return; }

            CurrentPuzzle.Solve();
            _solveCount++;

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
