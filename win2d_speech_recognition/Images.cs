using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace win2d_speech_recognition {
    static class Images {
        public static CanvasBitmap Triwizard { get; set; }

        public static async Task Initialize(CanvasDevice device) {
            Triwizard = await CanvasBitmap.LoadAsync(device, "Assets/triwizard.png", 96, CanvasAlphaMode.Premultiplied);
        }
    }
}
