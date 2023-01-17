using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FreeDraw
{
    // Helper methods used to set drawing settings
    public class Func_DrawingSettings : MonoBehaviour
    {
        public static bool isCursorOverUI = false;
        public float Transparency = 1f;

        // Changing pen settings is easy as changing the static properties Drawable.Pen_Colour and Drawable.Pen_Width
        public void SetMarkerColour(Color new_color)
        {
        //    Func_Drawable.Pen_Colour = new_color;
        //    Func_Drawable.drawable.AAAAA = new_color;
        }
        // new_width is radius in pixels
        public void SetMarkerWidth(int new_width)
        {
        //    Func_Drawable.Pen_Width = new_width;
        }
        public void SetMarkerWidth(float new_width)
        {
            SetMarkerWidth((int)new_width);
        }

        public void SetTransparency(float amount)
        {
            Transparency = amount;
        //    Color c = Func_Drawable.Pen_Colour;
        //    c.a = amount;
        //    Func_Drawable.Pen_Colour = c;
        }


        // Call these these to change the pen settings
        public void SetMarkerRed()
        {
            Func_Draw.selectedColor(ColorType.Red);
        }
        public void SetMarkerOrange()
        {
            Func_Draw.selectedColor(ColorType.Red);
        }
        public void SetMarkerYellow()
        {
            Func_Draw.selectedColor(ColorType.Red);
        }
        public void SetMarkerGreen()
        {
            Func_Draw.selectedColor(ColorType.Green);
        }
        public void SetMarkerBlue()
        {
            Func_Draw.selectedColor(ColorType.Blue);
        }
        public void SetMarkerPupple()
        {
            Func_Draw.selectedColor(ColorType.Blue);
        }
        public void SetMarkerPink()
        {
            Func_Draw.selectedColor(ColorType.Red);
        }
        public void SetMarkerBrown()
        {
            Func_Draw.selectedColor(ColorType.Yellow);
        }
        public void SetMarkerBlack()
        {
            Func_Draw.selectedColor(ColorType.Black);
        }
        public void SetEraser()
        {
            SetMarkerColour(new Color32(0, 0, 0, 0));
        }

        public void PartialSetEraser()
        {
            SetMarkerColour(new Color32(255, 255, 255, 0));
        }
    }
}