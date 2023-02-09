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

    public List<string> bubbleStickerList = new List<string>();
    public List<string> recordingStickerList = new List<string>();
    public List<string> recordingSignList = new List<string>();
    public List<string> bubbleGunStickerList = new List<string>();
    public List<string> bubbleFreeStickerList = new List<string>();

    string path = "";

    private void Start()
    {
        path = Application.persistentDataPath;
    }
    public void OnClick_BubbleStickerRepository()
    {
        LoadLocalSticker(bubbleStickerList);
    }
    public void OnClick_RecordingStickerRepository()
    {
        LoadLocalSticker(recordingStickerList);
    }
    public void OnClick_RecordFileRepository()
    {
        LoadLocalSticker(recordingStickerList, recordingSignList);
    }
    public void OnClick_BubbleGunStickerRepository()
    {
        LoadLocalSticker(bubbleGunStickerList);
    }
    public void OnClick_BubbleFreeStickerRepository()
    {
        LoadLocalSticker(bubbleFreeStickerList);
    }

    public void OnClick_RepositoryOpen()
    {
        string[] allFiles = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
        if (allFiles.Length == bubbleStickerList.Count + recordingStickerList.Count + recordingSignList.Count + bubbleGunStickerList.Count + bubbleFreeStickerList.Count)
        {
            return;
        }
        //List Initiate for rearrange;
        bubbleStickerList.Clear();
        recordingStickerList.Clear();
        recordingSignList.Clear();
        bubbleGunStickerList.Clear();
        bubbleFreeStickerList.Clear();
        for (int i = 0; i < allFiles.Length; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(allFiles[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);


                if (allFiles[i].Contains("BubbleSticker"))
                {
                    bubbleStickerList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("RecordingSticker"))
                {
                    recordingStickerList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("RecordingSign"))
                {
                    recordingSignList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("BubbleGun"))
                {
                    bubbleGunStickerList.Add(allFiles[i]);
                }
                else if (allFiles[i].Contains("BubbleFree"))
                {
                    bubbleFreeStickerList.Add(allFiles[i]);
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
