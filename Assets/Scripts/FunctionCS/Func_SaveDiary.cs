using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_SaveDiary : Func_SaveSticker
{
    [SerializeField] RectTransform diaryRect = null;


    protected override void Start()
    {
        base.Start();
    }
    public void OnClick_SaveDiary()
    {
        OnClick_SaveImgae(StickerType.Diary);
    }

    protected override void OnClick_SaveImgae(StickerType stickerType)
    {
        base.OnClick_SaveImgae(stickerType);
    }
}
