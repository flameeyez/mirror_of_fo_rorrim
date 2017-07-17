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

namespace mirror_of_fo_rorrim {
    static class PuzzleCollection {
        private static List<PalindromePuzzle> AllPuzzles = new List<PalindromePuzzle>();
        public static int TotalPuzzleCount { get { return AllPuzzles.Count; } }

        private static List<int> _lastUsed = new List<int>();
        private static List<PalindromePuzzle> CurrentPuzzles = new List<PalindromePuzzle>();

        private static int _solveCount = 0;
        public static int SolveCount { get { return _solveCount; } }
        public static bool Done { get { return _solveCount == Statics.WinCount; } }
        private static bool _transitioning;

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
            AllPuzzles.Add(new PalindromePuzzle(device, "Noel notices Leon.", "notices", new string[] { "sees", "seas", "seize" }));
            AllPuzzles.Add(new PalindromePuzzle(device, "Anne, I vote more cars race Tuscany to Vienna.", "tuscany", new string[] { "rome", "roam" }));
            AllPuzzles.Add(new PalindromePuzzle(device, "Too cold to hoot.", "cold", "hot"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Nurse, save rare vases, walk!", "walk", "run"));
            AllPuzzles.Add(new PalindromePuzzle(device, "No mists or snow, Simon.", "snow", "frost"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Bottom step, Sara's pet spot.", "bottom", "top"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Evil is a moniker of a foeman, as I live.", "moniker", "name"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Some boys interpret nine memos.", "boys", "men"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Delia sailed as unhappy Elias ailed.", "unhappy", "sad"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Did Dean help Diana? Ed did.", "help", new string[] { "aid", "ade" }));
            AllPuzzles.Add(new PalindromePuzzle(device, "Tuna pecan.", "pecan", "nut"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Roy Ames, I was a wise governor.", "governor", "mayor"));
            AllPuzzles.Add(new PalindromePuzzle(device, "No, it is closed on one position.", "closed", "open"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Enid and Edna eat.", "eat", "dine"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Senile cats.", "cats", "felines"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Paint, O coward!", "paint", "draw"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Rise to vote, madam.", "madam", "sir"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Now, sir, a war is lost!", "lost", new string[] { "won", "one", "1" }));
            AllPuzzles.Add(new PalindromePuzzle(device, "Live on, Time; emit no malice.", "malice", "evil"));
            AllPuzzles.Add(new PalindromePuzzle(device, "May a moody baby doom a potato?", "potato", "yam"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Sh! Tom sees butterflies.", "butterflies", "moths"));
            AllPuzzles.Add(new PalindromePuzzle(device, "No misses ordered flowers, Simon.", "flowers", "roses"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Dodge me, Dave.", "dodge", "evade"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Four animals I slam in a net.", "four", new string[] { "ten", "10" }));
            AllPuzzles.Add(new PalindromePuzzle(device, "Go hang a salami, I'm a spaghetti hog.", "spaghetti", "lasagna"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Nemo, we revere ladies.", "ladies", "women"));
            AllPuzzles.Add(new PalindromePuzzle(device, "A Subaru.", "subaru", "toyota"));
            AllPuzzles.Add(new PalindromePuzzle(device, "He lost a Toyota now, eh?", "lost", new string[] { "won", "one", "1" }));
            AllPuzzles.Add(new PalindromePuzzle(device, "Dog, as a demon deified, lived as a god.", "demon", "devil"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Not so, Boise.", "boise", "boston"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Stand on a potato pan, Otis.", "stand", "sit"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Eva, can I stab bats in a tunnel?", "tunnel", "cave"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Mad? Am I, sir?", "sir", new string[] { "madam", "madame" }));
            AllPuzzles.Add(new PalindromePuzzle(device, "No, it always propagates if I set a gap or prevention.", "always", "never"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Was it a mouse I saw?", "mouse", "rat"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Lew, Otto has a warm towel.", "warm", "hot"));
            AllPuzzles.Add(new PalindromePuzzle(device, "Must buy at tallest sum.", "buy", new string[] { "sell", "sale", "cell" }));
        }

        public static void NewGame() {
            _transitioning = false;
            _solveCount = 0;
            SolveIcons.Initialize(Statics.WinCount);

            CurrentPuzzles.Clear();
            List<int> currentPuzzles = new List<int>();

            for (int i = 0; i < Statics.WinCount; i++) {
                int index = Statics.r.Next(AllPuzzles.Count);
                while (_lastUsed.Contains(index) || currentPuzzles.Contains(index)) { index = Statics.r.Next(AllPuzzles.Count); }

                if (_lastUsed.Count >= (AllPuzzles.Count / 2)) { _lastUsed.RemoveAt(0); }
                _lastUsed.Add(index);

                AllPuzzles[index].Refresh();
                CurrentPuzzles.Add(AllPuzzles[index]);
                currentPuzzles.Add(index);
            }
        }

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            if (CurrentPuzzle == null) { return; }
            CurrentPuzzle.Draw(args);
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            if (CurrentPuzzle != null) {
                if (CurrentPuzzle.Done) { NextPuzzle(); }
                else { CurrentPuzzle.Update(args); }
            }
        }

        private static void NextPuzzle() {
            _transitioning = false;
            if (CurrentPuzzles.Count == 0) { return; }
            CurrentPuzzles.RemoveAt(0);
        }

        public static void SolveCurrentPuzzle() {
            if (CurrentPuzzle == null) { return; }

            if (!_transitioning) {
                _transitioning = true;
                CurrentPuzzle.Solve();
                SolveIcons.FadeIn(_solveCount);
                _solveCount++;

                Music.Play(Music.Whoosh);
            }
        }
    }
}
