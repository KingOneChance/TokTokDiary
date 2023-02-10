using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Func_BubbleBearSave : Func_SaveSticker
{
    [SerializeField] private RawImage signImage = null;
    [SerializeField] private RawImage signTempImage = null;
    [SerializeField] private RawImage hogImage = null;
    [SerializeField] private RawImage hogTempImage = null;
    [SerializeField] private GameObject finishPlayButton = null;

    protected override void Start()
    {
        savePath = Application.persistentDataPath;
        //calculate all position
        saveImageRect = saveImage.GetComponent<RectTransform>();
        //   startXPos = saveImage.gameObject.transform.position.x + saveImageRect.rect.position.x + 960;
        //   startYPos = saveImage.gameObject.transform.position.y + saveImageRect.rect.position.y + 540;
        startXPos = saveImageRect.rect.position.x + 960;
        startYPos = saveImageRect.rect.position.y + 540;
        widthValue = (int)saveImageRect.rect.width;
        heightValue = (int)saveImageRect.rect.height;
    }
    //It will be called when the sign image drop on the hedgehog image
    public void SaveAll()
    {
        base.OnClick_SaveImgae(StickerType.AudioSticker);
    }
    public void TextureChange()
    {
        signImage.texture = signTempImage.texture;
        hogImage.texture = hogTempImage.texture;
    //    SaveAll();

        saveTemp = hogTempImage;
        saveFileName = "RecordingSticker";
        widthValue = 660;
        heightValue = 1000;
        SaveTexture(StickerType.AudioSticker);

        saveTemp = signTempImage;
        saveFileName = "RecordingSign";
        widthValue = 525;
        heightValue = 525;
        SaveTexture(StickerType.SignSticker);
    }
    public void OnClick_TurnOff()
    {
        finishPlayButton.SetActive(true);
    }
}
