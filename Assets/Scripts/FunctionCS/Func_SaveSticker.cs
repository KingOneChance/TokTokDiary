using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// you need to prepare 2 things,
/// First thing is empty raw image. 
/// it is inserted on "saveImage"
/// Second thing is button.
/// look at "TestSave" Script.
/// </summary>
public class Func_SaveSticker : MonoBehaviour
{
    [Tooltip("save path : Application.persistentDataPath.saveFolder.saveFileName_(ascending order number)")]
    [Header("Save path")]
    [SerializeField] private string saveFileName = "";
    [SerializeField] private RawImage saveImage = null;
    private string bubbleGunStikcerFolder = "BubbleGun";
    private string bubbleStickerFolder = "Bubble";
    private string audioStickerFolder = "Audio";
    private string freeStickerFolder = "Free";
    private string diaryFolder = "Diary";
    private string savePath ="";

    [Tooltip("startXPos and startYPos 's start postion from left of bottom")]
    [Header("Save position of screen")]
    [SerializeField] private float startXPos;
    [SerializeField] private float startYPos;
    [SerializeField] private int widthValue;
    [SerializeField] private int heightValue;

    protected virtual void Start()
    {
        savePath = Application.persistentDataPath;
    }
    protected virtual void OnClick_SaveImgae(StickerType stickerType)
    {
        //test
        StartCoroutine(Co_ScreenShotFrame(stickerType));
    }
    protected virtual IEnumerator Co_ScreenShotFrame(StickerType stickerType)
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = new Texture2D(widthValue, heightValue, TextureFormat.RGB24, false);

        Rect rect = new Rect(startXPos, startYPos, widthValue, heightValue);

        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        saveImage.texture = texture;

        SaveTexture(stickerType);
    }
    protected  virtual void SaveTexture(StickerType stickerType)
    {
        int nowNum;
        switch (stickerType)
        {
            //First, Get number of sticker what you want
            //Secont, Set number of sticker what you want plus 1
            case StickerType.BubbleGunSticker:
                nowNum = Manager_Main.Instance.GetBubbleGunStickerNum(bubbleGunStikcerFolder);
                Manager_Main.Instance.SetBubbleGunStickerNum();
                //Saveimage save
                SaveTextureToPng(saveImage.texture, savePath + $"/{bubbleGunStikcerFolder}/", saveFileName + "_" + nowNum);
                //SaveTextureToPng(saveImage.texture, "C:/Users/User/Desktop/Sticker/", saveFileName + "_" + nowNum);

                break;
            case StickerType.BubbleSticker:
                nowNum = Manager_Main.Instance.GetBubbleStickerNum(bubbleStickerFolder);
                Manager_Main.Instance.SetBubbleStickerNum();
                SaveTextureToPng(saveImage.texture, savePath + $"/{bubbleStickerFolder}/", saveFileName + "_" + nowNum);
                break;
            case StickerType.AudioSticker:
                nowNum = Manager_Main.Instance.GetAudioStickerNum(audioStickerFolder);
                Manager_Main.Instance.SetAudioStickerNum();
                SaveTextureToPng(saveImage.texture, savePath + $"/{audioStickerFolder}/", saveFileName + "_" + nowNum);
                break;
            case StickerType.FreeSticker:
                nowNum = Manager_Main.Instance.GetFreeStickerNum(freeStickerFolder);
                Manager_Main.Instance.SetFreeStickerNum();
                SaveTextureToPng(saveImage.texture, savePath + $"/{freeStickerFolder}/", saveFileName + "_" + nowNum);
                break;
            case StickerType.Diary:
                nowNum = Manager_Main.Instance.GetDiaryNum(diaryFolder);
                Manager_Main.Instance.SetDiaryNum();
                SaveTextureToPng(saveImage.texture, savePath + $"/{diaryFolder}/", saveFileName + "_" + nowNum);
                break;
            default:
                Debug.Log("there is no sticker what you want");
                nowNum = 0;
                break;
        }
    }


    protected virtual void SaveTextureToPng(Texture texture, string directoryPath, string fileName)
    {
        if (true == string.IsNullOrEmpty(directoryPath)) return;
        if (false == Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture copiedRenderTexture = new RenderTexture(texture.width, texture.height, 0);

        Graphics.Blit(texture, copiedRenderTexture);
        RenderTexture.active = copiedRenderTexture;

        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRenderTexture;

        byte[] texturePNGBytes = texture2D.EncodeToPNG();

        string filePath = directoryPath + fileName + ".png";

        File.WriteAllBytes(filePath, texturePNGBytes);
    }
}
