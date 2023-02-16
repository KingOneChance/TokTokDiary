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
        startXPos = 293f;
        //saveImageRect.rect.position.x + 960 + 73;
        startYPos = 226.5f;
        //saveImageRect.rect.position.y + 540 + 23;
        widthValue = 1290;
        //(int)saveImageRect.rect.width;
        heightValue = 740;
            //(int)saveImageRect.rect.height;
    }
    public void OnClick_Save()
    {
        base.OnClick_SaveImgae(StickerType.FreeSticker);
    }
}
