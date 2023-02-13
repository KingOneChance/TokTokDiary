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
    [SerializeField] protected string saveFileName = "";
    [SerializeField] protected RawImage saveTemp = null;
    [SerializeField] protected Color backGroundColor;
    private string bubbleGunStikcerFolder = "BubbleGunSticker";
    private string bubbleStickerFolder = "BubbleSticker";
    private string audioStickerFolder = "RecordingSticker";
    private string recordingSignFolder = "RecordingSign";
    private string freeStickerFolder = "BubbleFreeSticker";
    private string diaryFolder = "Diary";
    protected string savePath = "";

    [Tooltip("startXPos and startYPos 's start postion from left of bottom")]
    [Header("Save position of screen")]
    [SerializeField] protected GameObject saveImage;
    [SerializeField] protected float startXPos;
    [SerializeField] protected float startYPos;
    [SerializeField] protected int widthValue;
    [SerializeField] protected int heightValue;

    protected RectTransform saveImageRect = null;
    protected virtual void Start()
    {
        savePath = Application.persistentDataPath;
        //calculate all position
        saveImageRect = saveImage.GetComponent<RectTransform>();
        startXPos = saveImage.gameObject.transform.position.x + saveImageRect.rect.position.x;
        startYPos = saveImage.gameObject.transform.position.y + saveImageRect.rect.position.y;
        widthValue = (int)saveImageRect.rect.width;
        heightValue = (int)saveImageRect.rect.height;
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
        //
        Debug.Log("BGC : " + backGroundColor);
        Texture2D newTex = new Texture2D(widthValue, heightValue);
        for (int x = 0; x < widthValue; x++)
        {
            for (int y = 0; y < heightValue; y++)
            {
                Color pixelColor = texture.GetPixel(x, y);
                if (pixelColor != backGroundColor)
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
        //
        saveTemp.texture = newTex;

      //  saveTemp.texture = texture;

        SaveTexture(stickerType);
    }
    protected virtual void SaveTexture(StickerType stickerType, string name = null)
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
                SaveTextureToPng(saveTemp.texture, savePath + $"/{bubbleGunStikcerFolder}/", saveFileName + "_" + nowNum);
                //SaveTextureToPng(saveImage.texture, "C:/Users/User/Desktop/Sticker/", saveFileName + "_" + nowNum);
                break;
            case StickerType.BubbleSticker:
                nowNum = Manager_Main.Instance.GetBubbleStickerNum(bubbleStickerFolder);
                Manager_Main.Instance.SetBubbleStickerNum();
                SaveTextureToPng(saveTemp.texture, savePath + $"/{bubbleStickerFolder}/", saveFileName + "_" + nowNum);
                break;
            case StickerType.AudioSticker:
                nowNum = Manager_Main.Instance.GetAudioStickerNum(audioStickerFolder);
                Manager_Main.Instance.SetAudioStickerNum();
                SaveTextureToPng(saveTemp.texture, savePath + $"/{audioStickerFolder}/", saveFileName + "_" + nowNum,1);
                break;
            case StickerType.SignSticker:
                nowNum = Manager_Main.Instance.GetSignStickerNum(recordingSignFolder);
                Manager_Main.Instance.SetSignStickerNum();
                SaveTextureToPng(saveTemp.texture, savePath + $"/{recordingSignFolder}/", saveFileName + "_" + nowNum,1);
                break;
            case StickerType.FreeSticker:
                nowNum = Manager_Main.Instance.GetFreeStickerNum(freeStickerFolder);
                Manager_Main.Instance.SetFreeStickerNum();
                SaveTextureToPng(saveTemp.texture, savePath + $"/{freeStickerFolder}/", saveFileName + "_" + nowNum);
                break;
            case StickerType.Diary:
                nowNum = Manager_Main.Instance.GetDiaryNum(diaryFolder);
                Manager_Main.Instance.SetDiaryNum();
                SaveTextureToPng(saveTemp.texture, savePath + $"/{diaryFolder}/", saveFileName + "_" + nowNum);
                break;
            default:
                Debug.Log("there is no sticker what you want");
                nowNum = 0;
                break;
        }
    }


    protected virtual void SaveTextureToPng(Texture texture, string directoryPath, string fileName, int test = 0)
    {
        if (true == string.IsNullOrEmpty(directoryPath)) return;
        if (false == Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        widthValue = widthValue / 7;
        heightValue = heightValue / 7;
        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture copiedRenderTexture = new RenderTexture(widthValue, heightValue, 0);

        Graphics.Blit(texture, copiedRenderTexture);
        RenderTexture.active = copiedRenderTexture;

        Texture2D texture2D = new Texture2D(widthValue, heightValue, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, widthValue, heightValue), 0, 0);
        texture2D.Apply();

        //
        if (test == 0)
        {
            Debug.Log("BGC 22: " + backGroundColor);
            Texture2D newTex = new Texture2D(widthValue, heightValue);
            for (int x = 0; x < widthValue; x++)
            {
                for (int y = 0; y < heightValue; y++)
                {
                    Color pixelColor = texture2D.GetPixel(x, y);
                    if (pixelColor != backGroundColor)
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
            saveTemp.texture = newTex;
            //

            RenderTexture.active = currentRenderTexture;

            // byte[] texturePNGBytes = texture2D.EncodeToPNG();
            byte[] texturePNGBytes = newTex.EncodeToPNG();
            string filePath = directoryPath + fileName + ".jpg";

            File.WriteAllBytes(filePath, texturePNGBytes);
        }
        else
        {
            RenderTexture.active = currentRenderTexture;

            byte[] texturePNGBytes = texture2D.EncodeToPNG();
            string filePath = directoryPath + fileName + ".jpg";

            File.WriteAllBytes(filePath, texturePNGBytes);
        }

    }
}
