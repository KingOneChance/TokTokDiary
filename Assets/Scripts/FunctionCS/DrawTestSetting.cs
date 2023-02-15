using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FreeDraw
{
    // Helper methods used to set drawing settings
    public class DrawTestSetting : MonoBehaviour
    {
        public static bool isCursorOverUI = false;
        public float Transparency = 1f;

        // Changing pen settings is easy as changing the static properties Drawable.Pen_Colour and Drawable.Pen_Width
        public void SetMarkerColour(Color new_color)
        {
            DrawTest.Pen_Colour = new_color;
        }
        // new_width is radius in pixels
        public void SetMarkerWidth(int new_width)
        {
            DrawTest.Pen_Width = new_width;
        }
        public void SetMarkerWidth(float new_width)
        {
            SetMarkerWidth((int)new_width);
        }

        public void SetTransparency(float amount)
        {
            Transparency = amount;
            Color c = DrawTest.Pen_Colour;
            c.a = amount;
            DrawTest.Pen_Colour = c;
        }
        public void SetRed()
        {
            SetMarkerColour(new Color(122f, 122f, 122f, 250f));
            Debug.Log("뻘건거?");
        }

        // Call these these to change the pen settings
        public void SetEraser()
        {
            SetMarkerColour(new Color(122f, 122f, 122f, 0f));
            Debug.Log("지워지니?");
        }

        public void PartialSetEraser()
        {
            SetMarkerColour(new Color(255f, 255f, 255f, 0.5f));
        }
    }
}