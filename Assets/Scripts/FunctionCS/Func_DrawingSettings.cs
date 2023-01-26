using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FreeDraw
{
    // Helper methods used to set drawing settings
    public class Func_DrawingSettings : MonoBehaviour
    {
        [SerializeField] Func_Draw func_Draw = null;

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
         //   func_Draw.Pen_Width = new_width;
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
            func_Draw.selectedColor(ColorType.Red);
        }
        public void SetMarkerOrange()
        {
            func_Draw.selectedColor(ColorType.Orange);
        }
        public void SetMarkerYellow()
        {
            func_Draw.selectedColor(ColorType.Yellow);
        }
        public void SetMarkerGreen()
        {
            func_Draw.selectedColor(ColorType.Green);
        }
        public void SetMarkerBlue()
        {
            func_Draw.selectedColor(ColorType.Blue);
        }
        public void SetMarkerPupple()
        {
            func_Draw.selectedColor(ColorType.Purple);
        }
        public void SetMarkerPink()
        {
            func_Draw.selectedColor(ColorType.Pink);
        }
        public void SetMarkerBrown()
        {
            func_Draw.selectedColor(ColorType.Brown);
        }
        public void SetMarkerBlack()
        {
            func_Draw.selectedColor(ColorType.Black);
        }
        public void SetEraser()
        {
            func_Draw.selectedColor(ColorType.Eraser);
        }

        public void PartialSetEraser()
        {
            SetMarkerColour(new Color32(255, 255, 255, 255));
        }
    }
}