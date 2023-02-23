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
    [SerializeField] private GameObject followObject = null;

    //������ ȹ�� ��ƼĿ
    [SerializeField] public Image sticker = null;

    //��ƼĿ�� �� 
    [SerializeField] public RawImage[] randImages=null;

    private int randStickerNum = 0;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    Func_GunCollision func_GunCollision = null;
    void Start()
    {
        followImage.transform.position = new Vector3(2000, 2000, 2000);
        followObject.transform.position = new Vector3(2000, 2000, 2000);
       func_GunCollision = FindObjectOfType<Func_GunCollision>();
        randStickerNum = Random.Range(0, randImages.Length);
        
        randImages[randStickerNum].gameObject.SetActive(true);
        sticker.sprite = stickers[randStickerNum];

        //Cursor.SetCursor(bubbleStickImage, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        followImage.transform.position = Input.mousePosition;
        followObject.transform.position = Input.mousePosition;
    }
    //restart��ư
    public void OnClick_Restart()
    {
        SceneManager.LoadScene("BubbleGun");
    }
    public void TobeSticker()
    {
        randImages[randStickerNum].gameObject.GetComponent<Button>().interactable = true;
       
        //func_GunCollision.OnClickSticker();
    }

}
