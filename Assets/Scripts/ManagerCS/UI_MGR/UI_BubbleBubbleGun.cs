using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_BubbleBubbleGun : MonoBehaviour
{
    //스티커가 될 사진들
    [SerializeField] private Sprite[] stickers = null;
    //더러운 비눗방울
    [SerializeField] private RawImage[] blacks = null;
    //마우스 커서 이미지
    [SerializeField] private Texture2D bubbleStickImage = null;

    //충돌처리해주는 이미지
    [SerializeField] private RawImage followImage = null;
    [SerializeField] private GameObject followObject = null;

    //마지막 획득 스티커
    [SerializeField] public Image sticker = null;

    //스티커가 될 
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
    //restart버튼
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
