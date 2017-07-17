using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace mirror_of_fo_rorrim {
    static class Debugging {
        public static long LastDrawMilliseconds { get; set; }
        public static long LastUpdateMilliseconds { get; set; }
        public static List<string> Strings = new List<string>();
        private static List<TimedString> TimedStrings = new List<TimedString>();
        private static object TimedStringsLock = new object();

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            Strings.Clear();
            Strings.Add("Draw: " + LastDrawMilliseconds.ToString() + "ms");
            Strings.Add("Update: " + LastUpdateMilliseconds.ToString() + "ms");


            int padding = 5;
            int width = 700;
            int height = (Strings.Count + 1) * 20;
            int x = 1920 - width - padding;
            int y = padding;
            //args.DrawingSession.FillRectangle(new Windows.Foundation.Rect(x - padding, y - padding, width, height), Colors.Gray);
            //args.DrawingSession.DrawRoundedRectangle(new Windows.Foundation.Rect(x - padding, y - padding, width, height), 3, 3, Colors.White);

            //foreach (string str in Strings) {
            //    args.DrawingSession.DrawText(str, new Vector2(x, y), Colors.White);
            //    y += 20;
            //}

            if (TimedStrings.Count > 0) {
                // y += 50;
                height = (TimedStrings.Count + 1) * 20;
                args.DrawingSession.FillRectangle(new Windows.Foundation.Rect(x, y, width, height), Colors.Gray);
                args.DrawingSession.DrawRoundedRectangle(new Windows.Foundation.Rect(x, y, width, height), 3, 3, Colors.White);
                lock (TimedStringsLock) {
                    foreach (TimedString str in TimedStrings) {
                        str.Draw(args, new Vector2(x + padding, y + padding));
                        y += 20;
                    }
                }
            }
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            lock (TimedStringsLock) {
                for (int i = TimedStrings.Count - 1; i >= 0; i--) {
                    if (TimedStrings[i].Dead) { TimedStrings.RemoveAt(i); }
                    else { TimedStrings[i].Update(args); }
                }
            }
        }

        public static void AddTimedString(string str) {
            lock (TimedStringsLock) {
                TimedStrings.Add(new TimedString(str, Colors.White));
            }
        }
    }
}
