using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UI_StickerRepository : MonoBehaviour
{
    [SerializeField] Button[] ui_StickerBtns = null;
    [SerializeField] RawImage[] ui_Stickers = null;

    // Start is called before the first frame update
    void Start()
    {
        //Manager_Main.Instance.Func_LoadPngFile.FindAllPng();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick_WaffleRepository()
    {
        LoadLocalSticker("Waffle");
    }
    public void OnClick_AudioRepository()
    {
        LoadLocalSticker("Audio");
    }
    public void OnClick_WeatherRepository()
    {
        LoadLocalSticker("Weather");
    }
    public void OnClick_DiaryRepository()
    {
        LoadLocalSticker("Diary");
    }

    public void LoadLocalSticker(string stickerName)
    {
        string path = "C:/Users/User/Desktop/Sticker/";
        string[] allFiles = Directory.GetFiles(path, stickerName+"*.png", SearchOption.TopDirectoryOnly);

        for(int i =0; i < ui_Stickers.Length; i++)
        {
            ui_Stickers[i].texture = null;
        }

        for (int i = 0; i < allFiles.Length; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(allFiles[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);

                ui_Stickers[i].texture = texture;
            }
        }
    }
}
