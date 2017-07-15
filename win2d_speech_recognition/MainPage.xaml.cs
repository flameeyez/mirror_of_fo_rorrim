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
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.SpeechRecognition;
using Windows.Storage;
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
    enum SCREEN {
        INTRO,
        MAIN,
        WINNER
    }

    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args) {
            switch (Statics.CurrentScreen) {
                case SCREEN.INTRO:
                    switch (args.VirtualKey) {
                        case Windows.System.VirtualKey.Space:
                            ScreenIntro.Transition();
                            break;
                        case Windows.System.VirtualKey.Number1:
                            Statics.WinCount = 1;
                            break;
                        case Windows.System.VirtualKey.Number2:
                            Statics.WinCount = 2;
                            break;
                        case Windows.System.VirtualKey.Number3:
                            Statics.WinCount = 3;
                            break;
                        case Windows.System.VirtualKey.Number4:
                            Statics.WinCount = 4;
                            break;
                        case Windows.System.VirtualKey.Number5:
                            Statics.WinCount = 5;
                            break;
                        case Windows.System.VirtualKey.Number6:
                            Statics.WinCount = 6;
                            break;
                        case Windows.System.VirtualKey.Number7:
                            Statics.WinCount = 7;
                            break;
                        case Windows.System.VirtualKey.Number8:
                            Statics.WinCount = 8;
                            break;
                        case Windows.System.VirtualKey.Number9:
                            Statics.WinCount = 9;
                            break;
                        case Windows.System.VirtualKey.Add:
                            Statics.WinCount++;
                            break;
                        case Windows.System.VirtualKey.Subtract:
                            if (Statics.WinCount > 1) { Statics.WinCount--; }
                            break;
                    }
                    break;
                case SCREEN.MAIN:
                    ScreenGame.KeyDown(args);
                    break;
                case SCREEN.WINNER:
                    switch (args.VirtualKey) {
                        case Windows.System.VirtualKey.Space:
                            ScreenWinner.Transition();
                            break;
                    }
                    break;
            }
        }

        private void CanvasMain_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args) {
            Stopwatch s = Stopwatch.StartNew();

            switch (Statics.CurrentScreen) {
                case SCREEN.INTRO:
                    ScreenIntro.Draw(args);
                    break;
                case SCREEN.MAIN:
                    ScreenGame.Draw(args);
                    break;
                case SCREEN.WINNER:
                    ScreenWinner.Draw(args);
                    break;
            }

            s.Stop();
            Debug.LastDrawMilliseconds = s.ElapsedMilliseconds;
            Debug.Draw(args);
        }

        private void CanvasMain_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args) {
            Stopwatch s = Stopwatch.StartNew();

            switch (Statics.CurrentScreen) {
                case SCREEN.INTRO:
                    if (ScreenIntro.Done) {
                        BackgroundWords.Clear();
                        Statics.CurrentScreen = SCREEN.MAIN;
                    }
                    else {
                        ScreenIntro.Update(args);
                    }
                    break;
                case SCREEN.MAIN:
                    if (ScreenGame.Done) {
                        ScreenWinner.Reset();
                        BackgroundWords.Clear();
                        Statics.CurrentScreen = SCREEN.WINNER;                        
                    }
                    else {
                        ScreenGame.Update(args);
                    }
                    break;
                case SCREEN.WINNER:
                    if (ScreenWinner.Done) {
                        ScreenIntro.Reset();
                        BackgroundWords.Clear();
                        Statics.CurrentScreen = SCREEN.INTRO;                        
                    }
                    else {
                        ScreenWinner.Update(args);
                    }
                    break;
            }

            s.Stop();
            Debug.LastUpdateMilliseconds = s.ElapsedMilliseconds;
            Debug.Update(args);
        }

        private async void CanvasMain_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args) {
            Statics.CanvasWidth = canvasMain.ActualWidth;
            Statics.CanvasHeight = canvasMain.ActualHeight;

            mediaSimple.MediaPlayer.RealTimePlayback = true;
            mediaSimple.MediaPlayer.IsLoopingEnabled = true;

            await Images.Initialize(sender.Device);
            PuzzleCollection.Initialize(sender.Device);
            await Speech.Initialize();
            BackgroundWords.Initialize(sender.Device);
            Music.Initialize();
            ScreenMicTest.Initialize(sender.Device);
            ScreenMicTestResponse.Initialize(sender.Device);
            ScreenLetsGo.Initialize(sender.Device);
            ScreenIntro.Initialize(sender.Device);
            ScreenWinner.Initialize(sender.Device);
        }

        private void CanvasMain_PointerMoved(object sender, PointerRoutedEventArgs e) {

        }

        private void CanvasMain_PointerPressed(object sender, PointerRoutedEventArgs e) {

        }

        private void CanvasMain_PointerReleased(object sender, PointerRoutedEventArgs e) {
            switch (Statics.CurrentScreen) {
                case SCREEN.INTRO:
                    ScreenIntro.Transition();
                    break;
                case SCREEN.MAIN:
                    PuzzleCollection.SolveCurrentPuzzle();
                    break;
                case SCREEN.WINNER:
                    ScreenWinner.Transition();
                    break;
            }
        }
    }
}
