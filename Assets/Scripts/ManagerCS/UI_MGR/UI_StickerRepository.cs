using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UI_StickerRepository : MonoBehaviour
{
    [SerializeField] Button[] ui_StickerBtns = null;
    [SerializeField] RawImage[] ui_Stickers = null;

    public List<string> waffleList = new List<string>();
    public List<string> audioList = new List<string>();
    public List<string> weatherList = new List<string>();
    public List<string> diaryList = new List<string>();


#if UNITY_EDITOR_WIN
    string path = "C:/Users/User/Desktop/Sticker/";
#else
        string path = Application.persistentDataPath + "/RecordSticker/";
#endif

    public void OnClick_WaffleRepository()
    {
        LoadLocalSticker(waffleList);
    }
    public void OnClick_AudioRepository()
    {
        LoadLocalSticker(audioList);
    }
    public void OnClick_WeatherRepository()
    {
        LoadLocalSticker(weatherList);
    }
    public void OnClick_DiaryRepository()
    {
        LoadLocalSticker(diaryList);
    }

    public void OnClick_RepositoryOpen()
    {
        string[] allFiles = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
        if(allFiles.Length == waffleList.Count + audioList.Count+ weatherList.Count+ diaryList.Count)
        {
            return;
        }
        //List Initiate for rearrange;
        waffleList.Clear();
        audioList.Clear();
        weatherList.Clear();
        diaryList.Clear();
        for (int i = 0; i < allFiles.Length; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(allFiles[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);


                if (allFiles[i].Contains("Waffle"))
                {
                    waffleList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("Audio"))
                {
                    audioList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("Weather"))
                {
                    weatherList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("Diary"))
                {
                    diaryList.Add(allFiles[i]);
                }
                else
                {
                    Debug.Log("It is not my Sticker");
                }
            }
        }
    }
    public void LoadLocalSticker(List<string> anyList )
    {
        //initiate raw images's texture
        for (int i = 0; i < ui_Stickers.Length; i++)
        {
            ui_Stickers[i].texture = null;
        }
        //Fill in the raw image's texture
        for (int i = 0; i < anyList.Count; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(anyList[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);

                ui_Stickers[i].texture = texture;
            }
        }
    }
}
