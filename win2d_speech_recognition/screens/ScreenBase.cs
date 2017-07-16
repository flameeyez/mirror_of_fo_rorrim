using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace win2d_speech_recognition {
    abstract class ScreenBase {
        protected CanvasDevice _device;
        protected bool _transitioning;
        public abstract bool Done { get; }
        public abstract void Draw(CanvasAnimatedDrawEventArgs args);
        public abstract void Update(CanvasAnimatedUpdateEventArgs args);
        public abstract void Reset();
        public abstract void Transition();
        public abstract void KeyDown(VirtualKey vk);
        public ScreenBase(CanvasDevice device) {
            _transitioning = false;
            _device = device;
        }
    }
}
