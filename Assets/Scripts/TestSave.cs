using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����ϱ� ���� ���� 
/// ��ư 1���� �� �ο� �̹��� ���� 1�� �ʿ�
/// �� �ο� �̹��� ���� 1���� �Ⱥ��̴� ������ ���ּ���
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
