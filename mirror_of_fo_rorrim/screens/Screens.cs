using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mirror_of_fo_rorrim {
    enum SCREEN_TYPE {
        INTRO,
        GAME,
        WINNER
    }

    static class Screens {
        private static SCREEN_TYPE _currentScreen;
        public static ScreenBase CurrentScreen {
            get {
                ScreenBase screen;
                ScreenDictionary.TryGetValue(_currentScreen, out screen);
                return screen;
            }
        }
        public static Dictionary<SCREEN_TYPE, ScreenBase> ScreenDictionary = new Dictionary<SCREEN_TYPE, ScreenBase>();

        public static void Initialize(CanvasDevice device) {
            ScreenDictionary.Add(SCREEN_TYPE.INTRO, new ScreenIntro(device));
            ScreenDictionary.Add(SCREEN_TYPE.GAME, new ScreenGame(device));
            ScreenDictionary.Add(SCREEN_TYPE.WINNER, new ScreenWinner(device));

            _currentScreen = SCREEN_TYPE.INTRO;
        }

        public static void Draw(CanvasAnimatedDrawEventArgs args) {
            if (CurrentScreen != null) { CurrentScreen.Draw(args); }
        }

        public static void Update(CanvasAnimatedUpdateEventArgs args) {
            if (CurrentScreen == null) { return; }

            if (CurrentScreen.Done) {
                NextScreen();
            }
            else {
                CurrentScreen.Update(args);
            }
        }

        private static void NextScreen() {
            switch (_currentScreen) {
                case SCREEN_TYPE.INTRO:
                    _currentScreen = SCREEN_TYPE.GAME;
                    break;
                case SCREEN_TYPE.GAME:
                    _currentScreen = SCREEN_TYPE.WINNER;
                    break;
                case SCREEN_TYPE.WINNER:
                    _currentScreen = SCREEN_TYPE.INTRO;
                    break;
            }

            BackgroundWords.Clear();
            if (CurrentScreen != null) { CurrentScreen.Reset(); }
        }
    }
}
