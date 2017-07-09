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
        public static Random r = new Random(DateTime.Now.Millisecond);

        public static double CanvasWidth { get; set; }
        public static double CanvasHeight { get; set; }

        public static string Random(this string[] strArray) {
            return strArray[r.Next(strArray.Length)];
        }
    }
}
