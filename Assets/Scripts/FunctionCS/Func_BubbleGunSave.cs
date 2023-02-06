using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_BubbleGunSave : Func_SaveSticker
{
    public void SaveBubbleGun()
    {
        OnClick_SaveImgae(StickerType.BubbleGunSticker);
    }

    protected override void OnClick_SaveImgae(StickerType stickerType)
    {
        base.OnClick_SaveImgae(stickerType);
    }
}
