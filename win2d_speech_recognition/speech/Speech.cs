using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;

namespace win2d_speech_recognition {
    static class Speech {
        public static SpeechRecognizer SpeechRecognizer;

        public async static Task Initialize() {
            // initialize speech recognition with default grammar
            SpeechRecognizer = new SpeechRecognizer();
            SpeechRecognitionCompilationResult result = await SpeechRecognizer.CompileConstraintsAsync();
            SpeechRecognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
            SpeechRecognizer.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
            SpeechRecognizer.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.MaxValue;
            await SpeechRecognizer.ContinuousRecognitionSession.StartAsync(SpeechContinuousRecognitionMode.Default);
        }

        private async static void ContinuousRecognitionSession_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args) {
            Debug.AddTimedString("Speech recognition has ended.");
            Debug.AddTimedString("Status: " + args.Status.ToString());
            Debug.AddTimedString("Restarting recognition sequence.");
            await SpeechRecognizer.ContinuousRecognitionSession.StartAsync(SpeechContinuousRecognitionMode.Default);
        }

        private static void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args) {
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium || args.Result.Confidence == SpeechRecognitionConfidence.High) {

                Debug.AddTimedString("Matched (" + args.Result.Confidence.ToString() + "): " + args.Result.Text);
                if (args.Result.Text.Split(" ".ToCharArray()).Contains(PuzzleCollection.CurrentPuzzle.Solution)) {
                    PuzzleCollection.SolveCurrentPuzzle();
                }
                else {
                    BackgroundWords.Enqueue(args.Result.Text);
                }
            }
        }
    }
}
