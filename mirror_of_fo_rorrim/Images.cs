using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace mirror_of_fo_rorrim {
    static class Images {
        public static CanvasBitmap Wand { get; set; }
        public static CanvasImageBrush WandBrush { get; set; }

        public static async Task Initialize(CanvasDevice device) {
            Wand = await CanvasBitmap.LoadAsync(device, "Assets/wand.png", 96, CanvasAlphaMode.Premultiplied);
            WandBrush = new CanvasImageBrush(device, Wand);
            WandBrush.Opacity = 0.1f;
        }
    }
}
