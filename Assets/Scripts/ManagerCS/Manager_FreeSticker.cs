using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_FreeSticker : Func_SaveSticker
{
    [SerializeField] public RawImage rawImage; // raw 이미지를 참조할 RawImage 컴포넌트
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
    public void OnClick_ScaleControl(float scale) // 버튼 클릭 이벤트 핸들러
    {
        rawImage.transform.localScale = new Vector3(scale, scale, 1f); // raw 이미지의 스케일 값을 조절
    }
}


