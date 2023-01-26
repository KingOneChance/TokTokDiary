using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 저장하기 예시 파일 
/// 버튼 1개와 빈 로우 이미지 파일 1개 필요
/// 빈 로우 이미지 파일 1개는 안보이는 곳으로 놔주세용
/// </summary>
public class TestSave : Func_SaveSticker
{
    [SerializeField] private Button testBt;
    protected override void Start()
    {
        base.Start();
        testBt.onClick.AddListener(() => OnClick_SaveImgae(StickerType.BubbleGunSticker));
    }
}
