using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_DIarySave : Func_SaveSticker
{
    [SerializeField] private UI_PictureDiary uI_PictureDiary = null;
    [SerializeField] private bool canSave = false;
    // fix Func_SaveSticker location
    protected override void Start()
    {
        uI_PictureDiary = FindObjectOfType<UI_PictureDiary>();
        savePath = Application.persistentDataPath;
        //calculate all position
        saveImageRect = saveImage.GetComponent<RectTransform>();
        startXPos = saveImageRect.rect.position.x + 960 + 244;
        startYPos = saveImageRect.rect.position.y + 540;
        widthValue = (int)saveImageRect.rect.width;
        heightValue = (int)saveImageRect.rect.height;
    }

    public void Onclick_Capture()
    {

    }
    public void SetCansave(bool value) //set canSave State; 프로필 선택시 트루 , 프로필 선택창 닫기시 폴스 
    {
        if (value == false)
            canSave = true;
        else
            canSave = false;
    }
    public void OnClick_Save()
    {
        //base.OnClick_SaveImgae(StickerType.FreeSticker);
        if (canSave == true)
            base.SaveTexture(StickerType.Diary);
    }
    public void SaveTempCapture()
    {
        StartCoroutine(Co_ScreenShotFrame());
    }
    private IEnumerator Co_ScreenShotFrame()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(widthValue, heightValue, TextureFormat.RGB24, false);

        Rect rex = new Rect(startXPos, startYPos, widthValue, heightValue);

        tex.ReadPixels(rex, 0, 0);
        tex.Apply();

        //
        Debug.Log("BGC : " + backGroundColor);
        Texture2D newTex = new Texture2D(widthValue, heightValue);
        for (int x = 0; x < widthValue; x++)
        {
            for (int y = 0; y < heightValue; y++)
            {
                Color pixelColor = tex.GetPixel(x, y);
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
        uI_PictureDiary.OnClick_OpenProfileButton();
    }
}
