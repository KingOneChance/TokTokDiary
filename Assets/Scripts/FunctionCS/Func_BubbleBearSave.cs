using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FreeDraw;
using TMPro;
using System.IO;
using System;

public class Func_BubbleBearSave : Func_SaveSticker
{
    [Header("===SignButton===")]
    [SerializeField] private Button signSaveButton = null;

    [Header("===SignImage===")]
    [SerializeField] private RawImage signImage = null;
    [SerializeField] private RawImage tempSignImage = null;

    [Header("===BubbleImage===")]
    [SerializeField] private RawImage bubbleImage = null;

    [Header("===HedgehogImage===")]
    [SerializeField] private RawImage tempHedgeHogImage = null;

    protected override void Start()
    {
        base.Start();
        signSaveButton.onClick.AddListener(() => OnClick_SaveImgae(StickerType.AudioSticker));
    }

    //It will be called when the sign image drop on the hedgehog image
    public void SaveAll()
    {
        OnClick_SaveImgae(StickerType.AudioSticker);
    }
}
