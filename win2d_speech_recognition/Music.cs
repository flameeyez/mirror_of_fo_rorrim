using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace win2d_speech_recognition {
    static class Music {
        public static MediaPlayer BackgroundMusic;
        public static MediaPlayer Whoosh;

        public static void Initialize() {
            BackgroundMusic = new MediaPlayer() {
                Source = MediaSource.CreateFromUri(new Uri("ms-appx:///mp3/background.mp3")),
            };
            BackgroundMusic.MediaEnded += BackgroundMusic_MediaEnded;
            BackgroundMusic.Play();

            Whoosh = new MediaPlayer() {
                Source = MediaSource.CreateFromUri(new Uri("ms-appx:///mp3/whoosh_faster.mp3")),
            };
        }

        private static void BackgroundMusic_MediaEnded(MediaPlayer sender, object args) {
            BackgroundMusic.Play();
        }

        public static void Play(MediaPlayer audio) {
            // if still playing, reset position to beginning
            if (audio.PlaybackSession.PlaybackState == MediaPlaybackState.Playing) {
                audio.PlaybackSession.Position = TimeSpan.Zero;
            }
            else {
                audio.Play();
            }
        }
    }
}
