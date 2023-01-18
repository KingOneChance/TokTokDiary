using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UI_StickerRepository : MonoBehaviour
{
    [Header("===StickerButtons===")]
    [SerializeField] private Button[] ui_StickerBtns = null;
    [Header("===Stickers===")]
    [SerializeField] private RawImage[] ui_Stickers = null;
    [SerializeField] private RawImage[] ui_RecordSubStickers = null;

    public List<string> freeList = new List<string>();
    public List<string> signList = new List<string>();
    public List<string> bearList = new List<string>();
    public List<string> diaryList = new List<string>();


#if UNITY_EDITOR_WIN
    string path = "C:/Users/User/Desktop/Sticker/";
#else
        string path = Application.persistentDataPath + "/RecordSticker/";
#endif

    public void OnClick_WaffleRepository()
    {
        LoadLocalSticker(freeList);
    }
    public void OnClick_AudioRepository()
    {
        LoadLocalSticker(signList);
    }
    public void OnClick_WeatherRepository()
    {
        LoadLocalSticker(bearList);
    }
    public void OnClick_DiaryRepository()
    {
        LoadLocalSticker(diaryList);
    }
    public void OnClick_RecordStickerRepository()
    {
        LoadLocalSticker(bearList, signList);
    }

    public void OnClick_RepositoryOpen()
    {
        string[] allFiles = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
        if (allFiles.Length == freeList.Count + signList.Count + bearList.Count + diaryList.Count)
        {
            return;
        }
        //List Initiate for rearrange;
        freeList.Clear();
        signList.Clear();
        bearList.Clear();
        diaryList.Clear();
        for (int i = 0; i < allFiles.Length; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(allFiles[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);


                if (allFiles[i].Contains("Free"))
                {
                    freeList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("Sign"))
                {
                    signList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("Bear"))
                {
                    bearList.Add(allFiles[i]);
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
    /// <summary>
    /// This function is purpose to fill Repository 
    /// </summary>
    /// <param name="anyList">Insert anyList without signlist</param>
    /// <param name="anyList2">Insert signList</param>
    public void LoadLocalSticker(List<string> anyList, List<string> anyList2 = null)
    {
        //initiate raw images's texture
        for (int i = 0; i < ui_Stickers.Length; i++)
        {
            ui_Stickers[i].texture = null;
            ui_RecordSubStickers[i].gameObject.SetActive(false);
            if (anyList2 != null)
            {
                ui_RecordSubStickers[i].gameObject.SetActive(true);    
                ui_RecordSubStickers[i].texture = null;
            }
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
            if (anyList2 != null)
            {
                byte[] byteTexture2 = File.ReadAllBytes(anyList2[i]);
                if (byteTexture2.Length > 0)
                {
                    Texture2D texture2 = new Texture2D(0, 0);
                    texture2.LoadImage(byteTexture2);

                    ui_RecordSubStickers[i].texture = texture2;
                }
            }
        }
    }
}
