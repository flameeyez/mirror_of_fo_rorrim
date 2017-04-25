using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace win2d_speech_recognition {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
        private List<string> SpeechResults = new List<string>();
        private object SpeechResultsLock = new object();
        private List<PalindromePuzzle> Puzzles = new List<PalindromePuzzle>();
        private int nIndex = 0;

        private void canvasMain_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args) {
            Puzzles[nIndex].Draw(args);

            int x = 1500;
            int y = 100;
            lock (SpeechResultsLock) {
                foreach (string str in SpeechResults) {
                    args.DrawingSession.DrawText(str, new Vector2(x, y), Colors.White);
                    y += 20;
                }
            }

            lock (FloatyWordsLock) {
                foreach (FloatingAnimatedString str in FloatyWords) {
                    str.Draw(args);
                }
            }
        }

        private async void canvasMain_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args) {
            Puzzles[nIndex].Update(args);

            for (int i = FloatyWords.Count - 1; i >= 0; i--) {
                if (FloatyWords[i].IsOffScreen) {
                    FloatyWords.RemoveAt(i);
                }
                else {
                    FloatyWords[i].Update(args);
                }
            }
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
            lock (SpeechResultsLock) {
                SpeechResults.Add("Speech recognition has ended.");
                SpeechResults.Add("Status: " + args.Status.ToString());
                SpeechResults.Add("Restarting recognition sequence.");
            }
            await speechRecognizer.ContinuousRecognitionSession.StartAsync(SpeechContinuousRecognitionMode.Default);
        }

        private void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args) {
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
              args.Result.Confidence == SpeechRecognitionConfidence.High) {
                lock (SpeechResultsLock) {
                    SpeechResults.Add("Matched: " + args.Result.Text);
                }

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
                        FloatyWords.Add(new FloatingAnimatedString(canvasMain.Device, args.Result.Text));
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
