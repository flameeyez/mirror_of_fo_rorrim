using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win2d_speech_recognition {
    static class CharacterDictionary {
        public static Dictionary<char, CanvasBitmap> Entry = new Dictionary<char, CanvasBitmap>();
        public async static Task Initialize(CanvasDevice device) {
            Entry.Add('A', await CanvasBitmap.LoadAsync(device, "letter_images/hp_upper_case_A.png"));
            Entry.Add('B', await CanvasBitmap.LoadAsync(device, "letter_images/hp_upper_case_B.png"));
            Entry.Add('C', await CanvasBitmap.LoadAsync(device, "letter_images/hp_upper_case_C.png"));
            Entry.Add('D', await CanvasBitmap.LoadAsync(device, "letter_images/hp_upper_case_D.png"));
            Entry.Add('E', await CanvasBitmap.LoadAsync(device, "letter_images/hp_upper_case_E.png"));
            Entry.Add('T', await CanvasBitmap.LoadAsync(device, "letter_images/hp_upper_case_T.png"));
        }
    }
}
