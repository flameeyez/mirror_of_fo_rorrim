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

namespace mirror_of_fo_rorrim {
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
            if (Screens.CurrentScreen != null) {
                Screens.CurrentScreen.KeyDown(args.VirtualKey);
            }
        }

        private void CanvasMain_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args) {
            Stopwatch s = Stopwatch.StartNew();
            Screens.Draw(args);
            s.Stop();

            Debug.LastDrawMilliseconds = s.ElapsedMilliseconds;
            Debug.Draw(args);
        }

        private void CanvasMain_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args) {
            Stopwatch s = Stopwatch.StartNew();
            Screens.Update(args);
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
            Screens.Initialize(sender.Device);
        }

        private void CanvasMain_PointerMoved(object sender, PointerRoutedEventArgs e) {

        }

        private void CanvasMain_PointerPressed(object sender, PointerRoutedEventArgs e) {

        }

        private void CanvasMain_PointerReleased(object sender, PointerRoutedEventArgs e) {
            if (Screens.CurrentScreen != null) {
                Screens.CurrentScreen.Transition();
            }
        }
    }
}
