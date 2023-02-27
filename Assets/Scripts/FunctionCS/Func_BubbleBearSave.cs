using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Func_BubbleBearSave : Func_SaveSticker
{
    [SerializeField] private RawImage signImage = null;
    [SerializeField] private RawImage signTempImage = null;
    [SerializeField] private RawImage hogImage = null;
    [SerializeField] private RawImage hogTempImage = null;
    [SerializeField] private GameObject finishPlayButton = null;

    [SerializeField] private ParticleSystem[] eff_GetBubbleSticker = null;

    protected override void Start()
    {
        savePath = Application.persistentDataPath;
        saveFileName = DateTime.Now.ToString("yyyy_MM_dd");
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
        base.OnClick_SaveImgae(StickerType.RecordSticker);
    }
    public void TextureChange()
    {
        signImage.texture = signTempImage.texture;
        hogImage.texture = hogTempImage.texture;
    //    SaveAll();
        
        saveTemp = hogTempImage;
        widthValue = 660;
        heightValue = 1000;
        SaveTexture(StickerType.RecordSticker);

        saveTemp = signTempImage;
        widthValue = 525;
        heightValue = 525;
        SaveTexture(StickerType.SignSticker);

        StartCoroutine(CO_Bomb());
    }
    public void OnClick_TurnOff()
    {
        finishPlayButton.SetActive(true);
    }

    private IEnumerator CO_Bomb()
    {
        Manager_Main.Instance.GetAudio().PlaySound("Fanfare", SoundType.Common, gameObject, false, true);
        for (int i = 0; i < eff_GetBubbleSticker.Length; ++i)
        {
            eff_GetBubbleSticker[i].Play();
        }

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < eff_GetBubbleSticker.Length; ++i)
        {
            eff_GetBubbleSticker[i].Clear(true);
        }
    }
}
