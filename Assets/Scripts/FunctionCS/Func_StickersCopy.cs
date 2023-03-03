using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FreeDraw;
using UnityEngine.UI;
using System.IO;
using System;

public class Func_StickersCopy : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("===copy object===")]
    [SerializeField] private GameObject mySticker = null;
    [SerializeField] private GameObject myParents = null;
    [SerializeField] private GameObject myGrandPa = null;
    [SerializeField] private GameObject myPos = null;
    [SerializeField] private Func_TodayFeelingImage drawObject = null;
    [Header("===Texture===")]
    [SerializeField] private RawImage mainSticker = null;
    [SerializeField] private RawImage signSticker = null;
    [SerializeField] private RawImage myRawimage = null;
    [Header("===Scripts===")]
    [SerializeField] private Func_DiaryInventory func_DiaryInventory = null;
    [SerializeField] private Func_DIarySave func_DiarySave = null;
    [SerializeField] private Func_HelperGuideDrag func_HelperGuideDrag;


    private RectTransform myRectTransform;
    private RectTransform copyRectTransform;
    private RectTransform startTransform;
    private Vector2 mousePos = Vector2.zero;
    [SerializeField] private Vector2 startPos = Vector2.zero;
    [SerializeField] private string stickerName = "";

    private void Awake()
    {
        func_DiaryInventory = FindObjectOfType<Func_DiaryInventory>();
        func_DiarySave = FindObjectOfType<Func_DIarySave>();
        drawObject = FindObjectOfType<Func_TodayFeelingImage>();
        myRectTransform = GetComponent<RectTransform>();
        func_DiaryInventory.del_SendName = StickerName;
        myRawimage = GetComponent<RawImage>();
    }
    private void StickerName()
    {
        if (myRawimage.texture != null)
        {
            stickerName = myRawimage.texture.name;
        }
    }

    public void OnClick_MakeClone()
    {
        startPos = myRectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //  if (func_HelperGuideDrag.canDrag == true) return;
        if (gameObject.tag == "CannotMake") return;
        drawObject.IsStickerMaking(true);
        gameObject.transform.SetParent(myGrandPa.transform);

    }
    public void OnDrag(PointerEventData eventData)
    {
        //  if (func_HelperGuideDrag.canDrag == true) return;
        if (gameObject.tag == "CannotMake") return;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myRectTransform.position = mousePos;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //  if (func_HelperGuideDrag.canDrag == true) return;
        if (gameObject.tag == "CannotMake") return;
        if (Input.mousePosition.x < 540)
        {
            gameObject.transform.SetParent(myParents.transform);
            myRectTransform.position = myPos.transform.position;
            return;
        }
        StickerName();
        GameObject newSticker = Instantiate(mySticker, myGrandPa.transform);
        copyRectTransform = newSticker.GetComponent<RectTransform>();
        copyRectTransform = myRectTransform;
        newSticker.AddComponent<Func_DragObject>();
        newSticker.AddComponent<Func_DetectOnSticker>();
        newSticker.transform.localScale = new Vector2(newSticker.transform.localScale.x * 2, newSticker.transform.localScale.y * 2);
        switch (func_DiaryInventory.GetNowType())
        {
            case StickerType.BubbleSticker:
                newSticker.tag = "BubbleSticker";
                func_DiarySave.SetUsedBubbleNum(int.Parse(gameObject.name));
                func_DiaryInventory.OnClick_BubbleStickerRepository();
                break;
            case StickerType.BubbleGunSticker:
                newSticker.tag = "GunSticker";
                func_DiarySave.SetUsedGunNum(int.Parse(gameObject.name));
                func_DiaryInventory.OnClick_BubbleGunStickerRepository();
                break;
            case StickerType.FreeSticker:
                newSticker.tag = "FreeSticker";
                newSticker.transform.localScale = new Vector2(newSticker.transform.localScale.x * 2, newSticker.transform.localScale.y * 2);
                func_DiarySave.SetUsedFreeNum(int.Parse(gameObject.name));
                func_DiaryInventory.OnClick_BubbleFreeStickerRepository();
                break;
            case StickerType.RecordSticker:
                newSticker.tag = "RecordSticker";
                string buffer = func_DiaryInventory.GetRecordName(int.Parse(gameObject.name));
                newSticker.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Manager_Main.Instance.GetAudio().PlayLocalSound(buffer, newSticker, false, false);
                    //newSticker . RecordToJson으로 위치와 레코드 파일 네임 보내주기
                });
                Manager_Main.Instance.func_DiaryToJson.AddRecordFileName(buffer);
                Manager_Main.Instance.func_DiaryToJson.AddRecordPos(newSticker);
                func_DiarySave.AddRecordList(buffer);
                func_DiarySave.SetUsedRecordNum(int.Parse(gameObject.name)); //이대로 자유스티커에 넣어줘야함 , name은 인덱스
                newSticker.transform.localScale = new Vector2(newSticker.transform.localScale.x * 2, newSticker.transform.localScale.y * 2.5f);
                func_DiaryInventory.OnClick_RecordFileRepository();
                break;
            default:
                break;
        }
        myRectTransform.position = myPos.transform.position;

        //nodedata plus
        SData_NodeData temp = new SData_NodeData();
        RectTransform tempRect = new RectTransform();
        tempRect = newSticker.GetComponent<RectTransform>();
        temp.position = tempRect.position;
        temp.rotation = tempRect.rotation.eulerAngles;
        temp.scale = tempRect.localScale;
        Manager_Main.Instance.manager_PictureDiary.AddDragInit(temp, newSticker);
        //
        gameObject.transform.SetParent(myParents.transform);
        drawObject.IsStickerMaking(false);

        Destroy(newSticker.GetComponent<Func_StickersCopy>());
    }
}
