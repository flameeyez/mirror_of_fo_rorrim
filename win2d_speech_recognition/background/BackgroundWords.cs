using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    static class BackgroundWords {
        private static List<FloatingAnimatedString> FloatyWords = new List<FloatingAnimatedString>();
        private static object FloatyWordsLock = new object();
        private static Queue<string> FloatingWordsQueue = new Queue<string>();
        private static CanvasDevice _device;

        public static void Initialize(CanvasDevice device) {
            _device = device;
        }

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            lock (FloatyWordsLock) {
                foreach (FloatingAnimatedString str in FloatyWords) {
                    str.Draw(args);
                }
            }
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            lock (FloatyWordsLock) {
                if (FloatingWordsQueue.Count > 0 && Statics.r.Next(50) == 0) {
                    FloatyWords.Add(new FloatingAnimatedString(_device, FloatingWordsQueue.Dequeue()));
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

        public static int Count {
            get {
                int nReturn;
                lock (FloatyWordsLock) { nReturn = FloatyWords.Count + FloatingWordsQueue.Count; }
                return nReturn;
            }
        }
    }
}
