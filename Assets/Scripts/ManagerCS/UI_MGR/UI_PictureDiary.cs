using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using static NativeCamera;
using System;

public class UI_PictureDiary : MonoBehaviour
{
    [SerializeField] string path = "";
    //그림판
    [Header("그림판")]
    [SerializeField] Image ui_DrawBackground = null;
    [SerializeField] Button[] ui_WeatherBtn = null;
   
    [Header("카메라")]
    //카메라 on - off, 불러오기
    [SerializeField] Button ui_CameraOnBtn = null;
    //[SerializeField] Button cameraOffBtn = null;
    [SerializeField] Button ui_LoadBtn = null;
    [Header("기타")]
    //홈버튼
    [SerializeField] Button ui_HomeBtn = null;
    [Header("스티커")]
    [SerializeField] RawImage[] ui_Stickers = null;
    [SerializeField] Button[] ui_StickersBtns = null;

    [Header("바늘")]
    [SerializeField] Button ui_NiddleBtn = null;
    [SerializeField] Texture2D ui_NiddleImage = null;

    [SerializeField] Texture2D ui_StickImage = null;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private bool isNiddleClicked = false;
    private bool isStickClicked = false;

    private Func_Camera func_Camera = null;

    [SerializeField] GameObject soupImage = null;

    private void Start()
    {
        func_Camera = FindObjectOfType<Func_Camera>();
        hotSpot.x = ui_NiddleImage.width / 2;
        hotSpot.y = ui_NiddleImage.height / 2;

        Manager_Main.Instance.UI_StickerRepository.OnClick_RepositoryOpen();

        //바늘 상태
        NiddleState = NiddleType.None;
        //비눗방울 스티커 상태
        SoupState = SoupBubbleType.Soap;

        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
    }
    public void OnClick_WaffleRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.freeList);
    }
    public void OnClick_AudioRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.signList);
    }
    public void OnClick_WeatherRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.bearList);
    }
    public void OnClick_DiaryRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.diaryList);
    }
    public void LoadLocalStickerInventory(List<string> anyList)
    {
        //initiate raw images's texture
        for (int i = 0; i < ui_Stickers.Length; i++)
        {
            ui_Stickers[i].texture = null;
        }
        //Fill in the raw image's texture
        for (int i = 0; i < anyList.Count; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(anyList[i]);

            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);

                ui_Stickers[i].texture = texture;
            }
        }  
    }

    public void OnClick_NativeCameraOnBtn()
    {
        TakePicture(CallBack);
    }

    private void CallBack(string path)
    {
        Byte[] bytes = File.ReadAllBytes(path);
        string fileName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
        NativeGallery.SaveImageToGallery(bytes, "DiaryPictureAlbum", fileName + ".jpg");
    }

    public void OnClick_NiddleBtn()
    {
        if (!isNiddleClicked)
        {
            Cursor.SetCursor(ui_NiddleImage, hotSpot, cursorMode);
            isNiddleClicked = true;
            NiddleState = NiddleType.Niddle;
        }
        else
        {
            Cursor.SetCursor(default, hotSpot, cursorMode);
            isNiddleClicked = false;
            NiddleState = NiddleType.None;
        }
    } 
    public void OnClick_BubbleStick()
    {
        if (!isStickClicked)
        {
            Cursor.SetCursor(ui_StickImage, hotSpot, cursorMode);
            isStickClicked = true;
            StickState = StickType.Stick;
        }
        else
        {
            Cursor.SetCursor(default, hotSpot, cursorMode);
            isStickClicked = false;
            StickState = StickType.None;
        }
    }


    private NiddleType NiddleState;
    public NiddleType NiddleStateInfo { get { return NiddleState; } }
    private SoupBubbleType SoupState;
    public SoupBubbleType SoupStateInfo { get { return SoupState; } }

    private StickType StickState;
    public StickType StickStateInfo { get { return StickState; } }
    public void OnClick_Pop()
    {
        if(NiddleState == NiddleType.Niddle && SoupState == SoupBubbleType.Soap)
        {
            Debug.Log("펑");
            soupImage.GetComponent<Func_StickerDrag>().enabled = false;
            SoupState = SoupBubbleType.Attached;
        }
        if (SoupState == SoupBubbleType.Attached && StickState == StickType.Stick)
        {
            soupImage.GetComponent<Func_StickerDrag>().enabled = true;
            SoupState = SoupBubbleType.Soap;
            Debug.Log("떼졌쥬?");
        }
    }

}
