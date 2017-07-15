using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    static class BackgroundWords {
        private static List<BackgroundAnimatedString> FloatyWords = new List<BackgroundAnimatedString>();
        private static object FloatyWordsLock = new object();
        private static Queue<string> FloatingWordsQueue = new Queue<string>();
        private static CanvasDevice _device;
        private static bool _initialized = false;

        public static void Initialize(CanvasDevice device) {
            _device = device;
            _initialized = true;
        }

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            lock (FloatyWordsLock) {
                foreach (BackgroundAnimatedString str in FloatyWords) {
                    str.Draw(args);
                }
            }
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            if (!_initialized) { return; }

            lock (FloatyWordsLock) {
                if (FloatingWordsQueue.Count > 0 && Statics.r.Next(50) == 0) {
                    FloatyWords.Add(new BackgroundAnimatedString(_device, FloatingWordsQueue.Dequeue()));
                }

                for (int i = FloatyWords.Count - 1; i >= 0; i--) {
                    if (FloatyWords[i].IsOutOfBounds) {
                        FloatyWords.RemoveAt(i);
                    }
                    else {
                        FloatyWords[i].Update(args);
                    }
                }
            }
        }

        public static void Enqueue(string str) {
            lock (FloatyWordsLock) {
                // this bit adds the same string twice (normal + mirrored)
                //for(int i = 0; i < 10; i++) {
                //    FloatyWords.Add(new FloatingAnimatedString(canvasMain.Device, args.Result.Text, FloatingAnimatedString.DRAW_STYLE.NORMAL));
                //    FloatyWords.Add(new FloatingAnimatedString(canvasMain.Device, args.Result.Text, FloatingAnimatedString.DRAW_STYLE.MIRRORED));
                //}

                // this bit splits the string into words and enqueues; mirroring is assigned when object is created (on dequeue; drawStyle = DEALERS_CHOICE)
                string[] words = str.Split(" ".ToCharArray());
                foreach (string word in words) {
                    FloatingWordsQueue.Enqueue(word);
                }
            }
        }

        public static void Clear() {
            lock (FloatyWordsLock) {
                FloatyWords.Clear();
                FloatingWordsQueue.Clear();
            }
        }

        public static void EnqueueRandomWords(int count) {
            for (int i = 0; i < count; i++) {
                EnqueueRandomWord();
            }
        }

        private static string[] _randomWords = { "palindrome", "puzzle", "safari", "2017", "magic", "mystery", "illusion" };
        private static void EnqueueRandomWord() {
            lock (FloatyWordsLock) {
                FloatingWordsQueue.Enqueue(_randomWords.Random());
            }
        }

        public static void EnqueueWinningWords(int count) {
            for (int i = 0; i < count; i++) {
                EnqueueWinningWord();
            }
        }

        private static string[] _randomWinningWords = { "Winner!", "winner", "You did it!", "Sweet!", "Cool!", "Yeah!", "Woo!" };
        private static void EnqueueWinningWord() {
            lock (FloatyWordsLock) {
                FloatingWordsQueue.Enqueue(_randomWinningWords.Random());
            }
        }

        public static void EnqueueRandomMicTestWords(int count) {
            for (int i = 0; i < count; i++) {
                EnqueueRandomMicTestWord();
            }
        }

        private static string[] _randomMicTestWords = { "Test!", "should work", "totally not a joke screen", "microphone", "say something" };
        private static void EnqueueRandomMicTestWord() {
            lock (FloatyWordsLock) {
                FloatingWordsQueue.Enqueue(_randomMicTestWords.Random());
            }
        }

        public static void EnqueueRandomMicTestResponseWords(int count) {
            for (int i = 0; i < count; i++) {
                EnqueueRandomMicTestResponseWord();
            }
        }

        private static string[] _randomMicTestResponseWords = { "extremely accurate", "totally worked", "also not a joke screen" };
        private static void EnqueueRandomMicTestResponseWord() {
            lock (FloatyWordsLock) {
                FloatingWordsQueue.Enqueue(_randomMicTestResponseWords.Random());
            }
        }

        public static void EnqueueRandomLetsGoWords(int count) {
            for (int i = 0; i < count; i++) {
                EnqueueRandomLetsGoWord();
            }
        }

        private static string[] _randomLetsGoWords = { "let's go!", "ready!" };
        private static void EnqueueRandomLetsGoWord() {
            lock (FloatyWordsLock) {
                FloatingWordsQueue.Enqueue(_randomLetsGoWords.Random());
            }
        }

        public static int Count {
            get {
                int nReturn;
                lock (FloatyWordsLock) { nReturn = FloatyWords.Count + FloatingWordsQueue.Count; }
                return nReturn;
            }
        }
    }
}
