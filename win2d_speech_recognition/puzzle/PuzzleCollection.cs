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
        private static int _winCount = 30;
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
            AllPuzzles.Add(new PalindromePuzzle(device, "Retaliation, my baby? Meg? Never!", "retaliation", "revenge"));
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
            AllPuzzles.Add(new PalindromePuzzle(device, "Some boys interpret nine memos.", "boys", "men"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Delia sailed as unhappy Elias ailed.", "unhappy", "sad"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Did Dean help Diana? Ed did.", "help", "aid"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Tuna pecan.", "pecan", "nut"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Roy Ames, I was a wise governor.", "governor", "mayor"));
            AllPuzzles.Add(new PalindromePuzzle(device, "No, it is closed on one position.", "closed", "open"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Enid and Edna eat.", "eat", "dine"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Senile cats.", "cats", "felines"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Paint, O coward!", "paint", "draw"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Rise to vote, madam.", "madam", "sir"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Now, sir, a war is lost!", "lost", "won"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Live on, Time; emit no malice.", "malice", "evil"));
            AllPuzzles.Add(new PalindromePuzzle(device, "May a moody baby doom a potato?", "potato", "yam"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Sh! Tom sees butterflies.", "butterflies", "moths"));
            AllPuzzles.Add(new PalindromePuzzle(device, "No misses ordered flowers, Simon.", "flowers", "roses"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Dodge me, Dave.", "dodge", "evade"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Four animals I slam in a net.", "four", "ten"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Go hang a salami, I'm a spaghetti hog.", "spaghetti", "lasagna"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Flee to me, remote orc.", "orc", "elf"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Nemo, we revere ladies.", "ladies", "women"));
            AllPuzzles.Add(new PalindromePuzzle(device, "A Subaru.", "subaru", "toyota"));
            AllPuzzles.Add(new PalindromePuzzle(device, "He lost a Toyota now, eh?", "lost", "won"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Dog, as a demon deified, lived as a god.", "demon", "devil"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Not so, Boise.", "boise", "boston"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Stand on a potato pan, Otis.", "stand", "sit"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Eva, can I stab bats in a tunnel?", "tunnel", "cave"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Mad? Am I, sir?", "sir", "madam"));
            AllPuzzles.Add(new PalindromePuzzle(device, "No, it always propagates if I set a gap or prevention.", "always", "never"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Was it a mouse I saw?", "mouse", "rat"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Lew, Otto has a warm towel.", "warm", "hot"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Must buy at tallest sum.", "buy", "sell"));
        }

        public static void NewGame() {
            _solveCount = 0;

            o1 = 0.0f;
            o2 = 0.0f;
            o3 = 0.0f;

            // debug - remove this
            _lastUsed.Clear();
            // debug - remove this

            CurrentPuzzles.Clear();

            for (int i = 0; i < _winCount; i++) {
                int index = Statics.r.Next(AllPuzzles.Count);
                while (_lastUsed.Contains(index)) { index = Statics.r.Next(AllPuzzles.Count); }

                if (_lastUsed.Count >= _winCount * 4) { _lastUsed.RemoveAt(0); }
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
