﻿using Microsoft.Graphics.Canvas;
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
        MAIN
    }

    public sealed partial class MainPage : Page {
        private SCREEN CurrentScreen = SCREEN.INTRO;

        public MainPage() {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args) {
            switch (CurrentScreen) {
                case SCREEN.INTRO:
                    switch (args.VirtualKey) {
                        case Windows.System.VirtualKey.Space:
                            ScreenIntro.Transition();
                            break;
                    }
                    break;
                case SCREEN.MAIN:
                    ScreenGame.KeyDown(args);
                    break;
            }
        }

        private void canvasMain_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args) {
            Stopwatch s = Stopwatch.StartNew();

            switch (CurrentScreen) {
                case SCREEN.INTRO:
                    ScreenIntro.Draw(args);
                    break;
                case SCREEN.MAIN:
                    ScreenGame.Draw(args);
                    break;
            }            

            s.Stop();
            Debug.LastDrawMilliseconds = s.ElapsedMilliseconds;
            Debug.Draw(args);
        }

        private void canvasMain_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args) {
            Stopwatch s = Stopwatch.StartNew();

            switch (CurrentScreen) {
                case SCREEN.INTRO:
                    if (ScreenIntro.Done) {
                        CurrentScreen = SCREEN.MAIN;
                        BackgroundWords.Clear();
                    }
                    else {
                        ScreenIntro.Update(args);
                    }                    
                    break;
                case SCREEN.MAIN:
                    ScreenGame.Update(args);
                    break;
            }            
            
            s.Stop();
            Debug.LastUpdateMilliseconds = s.ElapsedMilliseconds;
            Debug.Update(args);
        }

        private async void canvasMain_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args) {
            mediaSimple.MediaPlayer.RealTimePlayback = true;
            mediaSimple.MediaPlayer.IsLoopingEnabled = true;

            Puzzle.Initialize(sender.Device);
            await Speech.Initialize();
            BackgroundWords.Initialize(sender.Device);
            Music.Initialize();
            ScreenIntro.Initialize(sender.Device);
        }

        private void canvasMain_PointerMoved(object sender, PointerRoutedEventArgs e) {

        }

        private void canvasMain_PointerPressed(object sender, PointerRoutedEventArgs e) {

        }

        private void canvasMain_PointerReleased(object sender, PointerRoutedEventArgs e) {

        }
    }
}
