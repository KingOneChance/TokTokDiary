using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_DIarySave : Func_SaveSticker
{
    // fix Func_SaveSticker location
    protected override void Start()
    {
        savePath = Application.persistentDataPath;
        //calculate all position
        saveImageRect = saveImage.GetComponent<RectTransform>();
        startXPos = saveImageRect.rect.position.x + 960 + 244;
        startYPos = saveImageRect.rect.position.y + 540 + 2;
        widthValue = (int)saveImageRect.rect.width;
        heightValue = (int)saveImageRect.rect.height;
    }
    public void OnClick_Save()
    {
        base.OnClick_SaveImgae(StickerType.FreeSticker);
    }
}
