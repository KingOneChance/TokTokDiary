using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_FreeSticker : Func_SaveSticker
{
    // save point override;
    protected override void Start()
    {
        savePath = Application.persistentDataPath;
        //calculate all position
        saveImageRect = saveImage.GetComponent<RectTransform>();
        startXPos = saveImageRect.rect.position.x + 960 + 73;
        startYPos = saveImageRect.rect.position.y + 540 + 23;
        widthValue = (int)saveImageRect.rect.width;
        heightValue = (int)saveImageRect.rect.height;
    }
    public void OnClick_Save()
    {
        OnClick_SaveImgae(StickerType.FreeSticker);
    }
}
