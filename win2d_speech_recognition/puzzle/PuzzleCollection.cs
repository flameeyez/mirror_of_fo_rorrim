using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Playback;

namespace win2d_speech_recognition {
    static class PuzzleCollection {
        private static List<PalindromePuzzle> AllPuzzles = new List<PalindromePuzzle>();
        // private static int nIndex = 0;
        private static List<int> _lastUsed = new List<int>();
        private static List<PalindromePuzzle> CurrentPuzzles = new List<PalindromePuzzle>();
        private static int _solveCount = 0;
        private static int _winCount = 3;
        public static bool Winner {
            get {
                // all puzzles solved
                // all wand images faded in completely
                if(_solveCount != _winCount) { return false; }
                if (o1 != 1.0f) { return false; }
                if (o2 != 1.0f) { return false; }
                if (o3 != 1.0f) { return false; }
                return true;
            }
        }

        // coordinates and offset values for wand images (puzzle solves)
        private static int x1 = (int)(Statics.CanvasWidth - Images.Wand.Bounds.Width) / 2 - 200;
        private static int x2 = (int)(Statics.CanvasWidth - Images.Wand.Bounds.Width) / 2;
        private static int x3 = (int)(Statics.CanvasWidth - Images.Wand.Bounds.Width) / 2 + 200;
        private static int y = (int)(Statics.CanvasHeight - 30 - Images.Wand.Bounds.Height);
        private static float o1 = 0;
        private static float o2 = 0;
        private static float o3 = 0;
        private static int seed1 = Statics.r.Next(1000);
        private static int seed2 = Statics.r.Next(1000);
        private static int seed3 = Statics.r.Next(1000);

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

            o1 = 0.0f;
            o2 = 0.0f;
            o3 = 0.0f;

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

        public static void DrawSolveCount(CanvasAnimatedDrawEventArgs args) {
            if (_solveCount > 0) {
                args.DrawingSession.DrawImage(Images.Wand, new Rect(x1, y + (int)(7 * Math.Sin(++seed1 * 0.05)), Images.Wand.Bounds.Width, Images.Wand.Bounds.Height), Images.Wand.Bounds, o1);

            }
            if (_solveCount > 1) {
                args.DrawingSession.DrawImage(Images.Wand, new Rect(x2, y + (int)(7 * Math.Sin(++seed2 * 0.05)), Images.Wand.Bounds.Width, Images.Wand.Bounds.Height), Images.Wand.Bounds, o2);
            }

            if (_solveCount > 2) {
                args.DrawingSession.DrawImage(Images.Wand, new Rect(x3, y + (int)(7 * Math.Sin(++seed3 * 0.05)), Images.Wand.Bounds.Width, Images.Wand.Bounds.Height), Images.Wand.Bounds, o3);
            }
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            if (CurrentPuzzle != null) {
                if (CurrentPuzzle.Done) { NextPuzzle(); }
                else { CurrentPuzzle.Update(args); }
            }

            if (_solveCount > 0) { o1 += 0.02f; }
            if (o1 >= 1.0f) { o1 = 1.0f; }

            if (_solveCount > 1) { o2 += 0.02f; }
            if (o2 >= 1.0f) { o2 = 1.0f; }

            if (_solveCount > 2) { o3 += 0.02f; }
            if (o3 >= 1.0f) { o3 = 1.0f; }
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
