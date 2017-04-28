using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace win2d_speech_recognition {
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args) {
            switch (args.VirtualKey) {
                case Windows.System.VirtualKey.Space:
                    nIndex = (nIndex + 1) % Puzzles.Count;
                    break;
            }
        }

        private Random r = new Random(DateTime.Now.Millisecond);
        private List<FloatingAnimatedString> FloatyWords = new List<FloatingAnimatedString>();
        private object FloatyWordsLock = new object();
        private SpeechRecognizer speechRecognizer;
        private List<PalindromePuzzle> Puzzles = new List<PalindromePuzzle>();
        private int nIndex = 0;
        private Queue<string> FloatingWordsQueue = new Queue<string>();

        private void canvasMain_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args) {
            Stopwatch s = Stopwatch.StartNew();

            Puzzles[nIndex].Draw(args);

            lock (FloatyWordsLock) {
                foreach (FloatingAnimatedString str in FloatyWords) {
                    str.Draw(args);
                }
            }

            s.Stop();
            Debug.LastDrawMilliseconds = s.ElapsedMilliseconds;
            Debug.Draw(args);
        }

        private async void canvasMain_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args) {
            Stopwatch s = Stopwatch.StartNew();

            Puzzles[nIndex].Update(args);

            lock (FloatyWordsLock) {
                if (FloatingWordsQueue.Count > 0 && r.Next(50) == 0) {
                    FloatyWords.Add(new FloatingAnimatedString(canvasMain.Device, FloatingWordsQueue.Dequeue()));
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

            s.Stop();
            Debug.LastUpdateMilliseconds = s.ElapsedMilliseconds;
            Debug.Update(args);
        }

        private async void canvasMain_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args) {
            mediaSimple.MediaPlayer.RealTimePlayback = true;
            mediaSimple.MediaPlayer.IsLoopingEnabled = true;
            LoadPuzzles(sender.Device);

            // initialize speech recognition with default grammar
            speechRecognizer = new SpeechRecognizer();
            SpeechRecognitionCompilationResult result = await speechRecognizer.CompileConstraintsAsync();
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
            speechRecognizer.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
            speechRecognizer.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.MaxValue;
            await speechRecognizer.ContinuousRecognitionSession.StartAsync(SpeechContinuousRecognitionMode.Default);
        }

        private async void ContinuousRecognitionSession_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args) {
            Debug.AddTimedString("Speech recognition has ended.");
            Debug.AddTimedString("Status: " + args.Status.ToString());
            Debug.AddTimedString("Restarting recognition sequence.");
            await speechRecognizer.ContinuousRecognitionSession.StartAsync(SpeechContinuousRecognitionMode.Default);
        }

        private void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args) {
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
              args.Result.Confidence == SpeechRecognitionConfidence.High) {

                Debug.AddTimedString("Matched (" + args.Result.Confidence.ToString() + "): " + args.Result.Text);

                if (args.Result.Text == Puzzles[nIndex].Solution) {
                    // solved!
                    // cancel speech recognition
                    // set solution animation in motion
                    // let update handle state and restart speech recognition when ready
                    nIndex = (nIndex + 1) % Puzzles.Count;
                }
                else {
                    // build a floaty word
                    lock (FloatyWordsLock) {
                        // this bit adds the same string twice (normal + mirrored)
                        //for(int i = 0; i < 10; i++) {
                        //    FloatyWords.Add(new FloatingAnimatedString(canvasMain.Device, args.Result.Text, FloatingAnimatedString.DRAW_STYLE.NORMAL));
                        //    FloatyWords.Add(new FloatingAnimatedString(canvasMain.Device, args.Result.Text, FloatingAnimatedString.DRAW_STYLE.MIRRORED));
                        //}

                        // this bit splits the string into words and enqueues; mirroring is assigned when object is created (on dequeue; drawStyle = DEALERS_CHOICE)
                        string[] words = args.Result.Text.Split(" ".ToCharArray());
                        foreach (string word in words) {
                            FloatingWordsQueue.Enqueue(word);
                        }
                    }
                }
            }
        }

        private void LoadPuzzles(CanvasDevice device) {
            Puzzles.Add(new PalindromePuzzle(device, "Sore was I ere I saw Eros.", "saw"));
            Puzzles.Add(new PalindromePuzzle(device, "A man, a plan, a canal, Panama", "canal"));
            Puzzles.Add(new PalindromePuzzle(device, "Never a foot too far, even.", "foot"));
        }

        private void canvasMain_PointerMoved(object sender, PointerRoutedEventArgs e) {

        }

        private void canvasMain_PointerPressed(object sender, PointerRoutedEventArgs e) {

        }

        private void canvasMain_PointerReleased(object sender, PointerRoutedEventArgs e) {

        }
    }
}
