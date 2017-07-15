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
            //Debug.AddTimedString("Speech recognition has ended.");
            //Debug.AddTimedString("Status: " + args.Status.ToString());
            //Debug.AddTimedString("Restarting recognition sequence.");
            await SpeechRecognizer.ContinuousRecognitionSession.StartAsync(SpeechContinuousRecognitionMode.Default);
        }

        private static void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args) {
            Debug.AddTimedString("Matched (" + args.Result.Confidence.ToString() + "): " + args.Result.Text);
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium || args.Result.Confidence == SpeechRecognitionConfidence.High) {
                switch (Statics.CurrentScreen) {
                    case SCREEN.MIC_TEST:
                        ScreenMicTest.Transition();
                        break;
                    case SCREEN.MIC_TEST_RESPONSE:
                        ScreenMicTestResponse.Transition();
                        break;
                    case SCREEN.LETS_GO:
                        ScreenLetsGo.Transition();
                        break;
                    default:
                        for(int i = 0; i < 5; i++) {
                            BackgroundWords.Enqueue(args.Result.Text);
                        }                        

                        if (PuzzleCollection.CurrentPuzzle != null) {
                            string[] words = args.Result.Text.Split(" ".ToCharArray());
                            foreach (string word in words) {
                                if (PuzzleCollection.CurrentPuzzle.IsSolution(word)) {
                                    PuzzleCollection.SolveCurrentPuzzle();
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
