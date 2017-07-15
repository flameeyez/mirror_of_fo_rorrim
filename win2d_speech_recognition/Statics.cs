using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.SpeechRecognition;

namespace win2d_speech_recognition {
    static class Statics {
        public static SCREEN CurrentScreen = SCREEN.INTRO;
        public static Random r = new Random(DateTime.Now.Millisecond);

        public static double CanvasWidth { get; set; }
        public static double CanvasHeight { get; set; }

        public static string Random(this string[] strArray) {
            return strArray[r.Next(strArray.Length)];
        }

        private static int _winCount;
        public static int WinCount {
            get { return _winCount; }
            set {
                _winCount = Math.Min(value, PuzzleCollection.TotalPuzzleCount);
                Debug.AddTimedString("Win count: " + _winCount.ToString());
            }
        }
        static Statics() {
            _winCount = 5;
        }

        private static string[] _words = { "porkchop", "abfdslkje", "sandwich", "wizard", "magic", "gumdrops", "banana", "radiator" };
        public static string RandomMicTestWord() {
            return _words.Random();
        }

        private static string[] _randomYesWords = { "yes", "definitely", "totally", "absolutely" };
        public static string RandomYesWord() {
            return _randomYesWords.Random();
        }
    }
}
