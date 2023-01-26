using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_SaveSticker : MonoBehaviour
{
    [SerializeField] private RawImage saveImage = null;
    [SerializeField] private string saveFileName = "";
    [SerializeField] private string savePath = "";

    [Tooltip("startXPos and startYPos 's start postion from left of bottom")]
    [Header("Save position of screen")]
    [SerializeField] private float startXPos;
    [SerializeField] private float startYPos;
    [SerializeField] private int widthValue;
    [SerializeField] private int heightValue;

    protected void OnClick_RealSave()
    {
        //test
        StartCoroutine(Co_ScreenShotFrame());
    }
    protected IEnumerator Co_ScreenShotFrame()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = new Texture2D(widthValue, heightValue, TextureFormat.RGB24, false);

        Rect rect = new Rect(startXPos, startYPos, widthValue, heightValue);

        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        saveImage.texture = texture;

        //SaveTexture();
    }
   /* protected void SaveTexture()
    {
        int nowNum;
        nowNum = Manager_Main.Instance.GetRecordStickerNum();
        //Bearimage Save
        SaveTextureToPng(saveImage.texture, savePath, saveFileName + "_" + nowNum);

        func_BubbleBearSign.GetBearSPrite(spaceBearImage.texture);
        func_BubbleBearSign.GetSignSPrite(spaceSignImage.texture);

        //recordNum increase
        Manager_Main.Instance.SetRecordStickerNum();
        manager_BubbleBear.OnClick_ButtonSign();
    }


    public void SaveTextureToPng(Texture texture, string directoryPath, string fileName)
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
    }*/
}
