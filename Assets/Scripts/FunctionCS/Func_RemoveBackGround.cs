using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_RemoveBackGround : MonoBehaviour
{
    public Texture2D sourceTex;
    public Color backgroundColor;

    void Start()
    {
        Texture2D newTex = new Texture2D(sourceTex.width, sourceTex.height);
        for (int x = 0; x < sourceTex.width; x++)
        {
            for (int y = 0; y < sourceTex.height; y++)
            {
                Color pixelColor = sourceTex.GetPixel(x, y);
                if (pixelColor != backgroundColor)
                {
                    newTex.SetPixel(x, y, pixelColor);
                }
                else
                {
                    newTex.SetPixel(x, y, Color.clear);
                }
            }
        }
        newTex.Apply();
        GetComponent<Renderer>().material.mainTexture = newTex;
    }
}
