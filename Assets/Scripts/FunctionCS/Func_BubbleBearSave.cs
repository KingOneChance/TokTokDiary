using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FreeDraw;
using TMPro;
using System.IO;
using System;

public class Func_BubbleBearSave : MonoBehaviour
{
    [Header("===Panel===")]
    [SerializeField] private GameObject panel = null;
    [Header("===RawImageSpace===")]
    [SerializeField] private RawImage spaceBearImage = null;
    [SerializeField] private RawImage spaceSignImage = null;
    [Header("===Scripts===")]
    [SerializeField] private Manager_BubbleBear manager_BubbleBear = null;
    [SerializeField] private Func_BubbleBearSign func_BubbleBearSign = null;
    [Header("===Text===")]
    [SerializeField] private TextMeshProUGUI spriteText = null;
    [Header("===Path===")]
    [SerializeField] private string savePath = null;
    [Header("===FileName===")]
    [SerializeField] private string fileNameBear = null;
    [SerializeField] private string fileNameSign = null;

    private Sprite saveBearSprite = null;
    private Sprite saveSignSprite = null;


    private void Awake()
    {
        savePath = Application.persistentDataPath + "/RecordSticker/";
    }
    public void GetSaveSPrite(Sprite sprite)
    {
        if (sprite != null) spriteText.text = "is not null";
        else spriteText.text = "is null";

        saveBearSprite = sprite;
        spaceBearImage.texture = saveBearSprite.texture;
    }
    public void OnClick_PencileButton()
    {

    }
    public void OnClick_RealSave()
    {
        //test
        StartCoroutine(Co_ScreenShotFrame());
    }
    IEnumerator Co_ScreenShotFrame()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(500, 500, TextureFormat.RGB24, false);

        Rect rex = new Rect(1010, 240, 500, 500);

        tex.ReadPixels(rex, 0, 0);
        tex.Apply();

        spaceSignImage.texture = tex;


        SaveTexture();
    }
    private void SaveTexture()
    {
        int nowNum;
        nowNum = Manager_Main.Instance.GetRecordStickerNum();
        //Bearimage Save
        SaveTextureToPng(spaceBearImage.texture, savePath, fileNameBear + "_" + nowNum);
        //Signimage Save
        SaveTextureToPng(spaceSignImage.texture, savePath, fileNameSign + "_" + nowNum);

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
    }

    public void SignTextureMaker()
    {
        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();
        spaceSignImage.texture = (Texture)screenshot;
    }


    //I have to add Saving record file Function.
    //I have to add Saving sign file Function.
}
