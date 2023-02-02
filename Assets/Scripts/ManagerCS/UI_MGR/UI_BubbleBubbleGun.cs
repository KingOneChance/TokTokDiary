using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_BubbleBubbleGun : MonoBehaviour
{
    //��ƼĿ�� �� ������
    [SerializeField] private Sprite[] stickers = null;
    //������ �񴰹��
    [SerializeField] private RawImage[] blacks = null;
    //���콺 Ŀ�� �̹���
    [SerializeField] private Texture2D bubbleStickImage = null;
    //�浹ó�����ִ� �̹���
    [SerializeField] private RawImage followImage = null;

    //������ ȹ�� ��ƼĿ
    [SerializeField] private Image sticker = null;
    //��������Ʈ ����
    [SerializeField] private Sprite[] randSticker = null;
    //��ƼĿ�� �� 
    [SerializeField] private RawImage[] randImages=null;


    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        int randStickerNum = Random.Range(0, randImages.Length);
        
        randImages[randStickerNum].gameObject.SetActive(true);
        sticker.sprite = stickers[randStickerNum];

        //Cursor.SetCursor(bubbleStickImage, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        followImage.transform.position = Input.mousePosition;
    }
    //restart��ư
    public void OnClick_Restart()
    {
        SceneManager.LoadScene("BubbleGun");
    }
}
