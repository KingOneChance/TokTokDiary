using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_BubbleGunSave : Func_SaveSticker
{
    [SerializeField] private ParticleSystem[] eff_GetBubbleSticker = null;
    Func_GunCollision func_GunCollision = null;

    private new void Start()
    {
        func_GunCollision= FindObjectOfType<Func_GunCollision>();
        savePath = Application.persistentDataPath;
        //calculate all position
        saveImageRect = saveImage.GetComponent<RectTransform>();
        startXPos = saveImage.gameObject.transform.position.x + saveImageRect.rect.position.x + 960;
        startYPos = saveImage.gameObject.transform.position.y + saveImageRect.rect.position.y + 540;
        widthValue = (int)saveImageRect.rect.width;
        heightValue = (int)saveImageRect.rect.height;
    }
    public void SaveBubbleGun(string fileName)
    {
        StartCoroutine(Save(fileName));
    }

    protected override void OnClick_SaveImgae(StickerType stickerType)
    {
        base.OnClick_SaveImgae(stickerType);
    }

    IEnumerator Save(string fileName)
    {
        isSaveDone = false;
        saveFileName = fileName;
        OnClick_SaveImgae(StickerType.BubbleGunSticker);
        yield return new WaitUntil(() => isSaveDone == true);
        func_GunCollision.offImage.gameObject.SetActive(false);
        StartCoroutine(CO_Bomb());
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
