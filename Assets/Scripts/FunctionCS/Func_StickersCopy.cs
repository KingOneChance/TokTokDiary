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
    [Header("===Scripts===")]
    [SerializeField] private Func_DiaryInventory func_DiaryInventory = null;
    [SerializeField] private Func_DIarySave func_DIarySave = null;



    private RectTransform myRectTransform;
    private RectTransform copyRectTransform;
    private RectTransform startTransform;
    private Vector2 mousePos = Vector2.zero;
    private string stickerName = "";

    private void Start()
    {
        func_DIarySave = FindObjectOfType<Func_DIarySave>();
        func_DiaryInventory = FindObjectOfType<Func_DiaryInventory>();
        drawObject = FindObjectOfType<Func_TodayFeelingImage>();
        myRectTransform = GetComponent<RectTransform>();
        stickerName = GetComponent<RawImage>().texture != null ? GetComponent<RawImage>().texture.name : "";
    }
    public void OnClick_MakeClone()
    {


    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        drawObject.IsStickerMaking(true);
        gameObject.transform.SetParent(myGrandPa.transform);
    }
    public void OnDrag(PointerEventData eventData)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myRectTransform.position = mousePos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject newSticker = Instantiate(mySticker, myGrandPa.transform);
        copyRectTransform = newSticker.GetComponent<RectTransform>();
        copyRectTransform = myRectTransform;
        newSticker.AddComponent<Func_DragObject>();
        newSticker.AddComponent<Func_DetectOnSticker>();
        Destroy(newSticker.GetComponent<Func_StickersCopy>());
        newSticker.transform.localScale = new Vector2(newSticker.transform.localScale.x * 2, newSticker.transform.localScale.y * 2);
        switch(func_DiaryInventory.GetNowType())
        {
            case StickerType.BubbleSticker:
                break;
            case StickerType.BubbleGunSticker:
                break;
            case StickerType.FreeSticker:
                newSticker.transform.localScale = new Vector2(newSticker.transform.localScale.x * 2, newSticker.transform.localScale.y * 2);
                break;
            case StickerType.RecordSticker:
                newSticker.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (func_DiaryInventory.GetNowType() == StickerType.RecordSticker)
                    {
                        //
                        string buffer = func_DiaryInventory.GetRecordName(int.Parse(gameObject.name));
                        string signBuffer = func_DiaryInventory.GetRecordingSignList(int.Parse(gameObject.name));
                        string stickerBuffer = func_DiaryInventory.GetRecordingStickerList(int.Parse(gameObject.name));
                        Manager_Main.Instance.GetAudio().PlayLocalSound(buffer, newSticker, false, false);
                        //newSticker . RecordToJson으로 위치와 레코드 파일 네임 보내주기
                        Manager_Main.Instance.func_DiaryToJson.AddRecordFileName(buffer);
                        Manager_Main.Instance.func_DiaryToJson.AddRecordPos(newSticker);
                        func_DIarySave.AddRecordList(buffer);
                        //사용한 스티커 삭제하기
                        DeleteFile(signBuffer);
                        DeleteFile(stickerBuffer);
                    }
                });
                newSticker.transform.localScale = new Vector2(newSticker.transform.localScale.x * 2, newSticker.transform.localScale.y * 2.5f);
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

        //오디오 매니저 사용 방법
        //Manager_Main.Instance.GetAudio().PlaySound("클립이름", SoundType.Touch, gameObject,  false);
        //Manager_Main.Instance.GetAudio().PlayLocalSound("클립이름", newSticker, false);
    }
    private void DeleteFile (string path)
    {
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
