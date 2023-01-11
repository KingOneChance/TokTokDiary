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
            Func_Drawable.Pen_Colour = new_color;
            Func_Drawable.drawable.AAAAA = new_color;
        }
        // new_width is radius in pixels
        public void SetMarkerWidth(int new_width)
        {
            Func_Drawable.Pen_Width = new_width;
        }
        public void SetMarkerWidth(float new_width)
        {
            SetMarkerWidth((int)new_width);
        }

        public void SetTransparency(float amount)
        {
            Transparency = amount;
            Color c = Func_Drawable.Pen_Colour;
            c.a = amount;
            Func_Drawable.Pen_Colour = c;
        }


        // Call these these to change the pen settings
        public void SetMarkerRed()
        {
            SetMarkerColour(new Color32(247, 67, 67, 255));
            Func_Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerOrange()
        {
            SetMarkerColour(new Color32(248, 151, 49, 255));
            Func_Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerYellow()
        {
            SetMarkerColour(new Color32(253, 241, 100, 255));
            Func_Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerGreen()
        {
            SetMarkerColour(new Color32(147, 202, 91, 255));
            Func_Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerBlue()
        {
            SetMarkerColour(new Color32(98, 150, 207, 255));
            Func_Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerPupple()
        {
            SetMarkerColour(new Color32(158, 127, 186, 255));
            Func_Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerPink()
        {
            SetMarkerColour(new Color32(233, 160, 198, 255));
            Func_Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerBrown()
        {
            SetMarkerColour(new Color32(141, 107, 75, 255));
            Func_Drawable.drawable.SetPenBrush();
        }
        public void SetMarkerBlack()
        {
            SetMarkerColour(new Color32(30, 30, 30, 255));
            Func_Drawable.drawable.SetPenBrush();
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