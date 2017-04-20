using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI;
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
        }

        AnimatedString A;

        string str = "nada";
        private async Task RecordSpeechFromMicrophoneAsync() {
            var speechRecognizer = new SpeechRecognizer();
            speechRecognizer.Timeouts.InitialSilenceTimeout = new TimeSpan(0, 1, 0);
                //UIOptions.IsReadBackEnabled = false;

            await speechRecognizer.CompileConstraintsAsync();
            SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();//RecognizeWithUIAsync();
            
            str = speechRecognitionResult.Text;
            //var messageDialog = new Windows.UI.Popups.MessageDialog(speechRecognitionResult.Text, "Text spoken");
            //await messageDialog.ShowAsync();
        }

        private void canvasMain_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args) {
            // args.DrawingSession.DrawText(str, new System.Numerics.Vector2(10, 10), Colors.White);

            if(A != null) {
                A.Draw(args);
            }
        }

        private void canvasMain_Update(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedUpdateEventArgs args) {
            if (A != null) {
                A.Update(args);
            }            
        }

        private async void canvasMain_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args) {
            await CharacterDictionary.Initialize(sender.Device);
            A = new AnimatedString(sender.Device, "ABCDET ABCDET ABCDET", new Vector2(10, 100));
        }

        private void canvasMain_PointerMoved(object sender, PointerRoutedEventArgs e) {

        }

        private void canvasMain_PointerPressed(object sender, PointerRoutedEventArgs e) {

        }

        private void canvasMain_PointerReleased(object sender, PointerRoutedEventArgs e) {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e) {
            // await RecordSpeechFromMicrophoneAsync();
            // int i = 0;
        }
    }
}
