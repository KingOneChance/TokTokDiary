using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static NativeCamera;

public class UI_PictureDiary : MonoBehaviour
{
    [SerializeField] string path = "";
    //�׸���
    [Header("�׸���")]
    [SerializeField] Image ui_DrawBackground = null;
    [SerializeField] Button[] ui_WeatherBtn = null;
   
    [Header("ī�޶�")]
    //ī�޶� on - off, �ҷ�����
    [SerializeField] Button ui_CameraOnBtn = null;
    //[SerializeField] Button cameraOffBtn = null;
    [SerializeField] Button ui_LoadBtn = null;
    [Header("��Ÿ")]
    //Ȩ��ư
    [SerializeField] Button ui_HomeBtn = null;
    [Header("��ƼĿ")]
    [SerializeField] RawImage[] ui_Stickers = null;
    [SerializeField] Button[] ui_StickersBtns = null;

    [Header("�ٴ�")]
    [SerializeField] Button ui_NiddleBtn = null;
    [SerializeField] Texture2D ui_NiddleImage = null;

    [SerializeField] Texture2D ui_StickImage = null;

    [SerializeField] private RawImage loadImage;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private bool isNiddleClicked = false;
    private bool isStickClicked = false;

    [SerializeField] GameObject soupImage = null;

    private void Start()
    {
        hotSpot.x = ui_NiddleImage.width / 2;
        hotSpot.y = ui_NiddleImage.height / 2;

        Manager_Main.Instance.UI_StickerRepository.OnClick_RepositoryOpen();

        //�ٴ� ����
        NiddleState = NiddleType.None;
        //�񴰹�� ��ƼĿ ����
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
        StartCoroutine(LoadImage(path));
    }

    public void Onclick_LoadImage()
    {
        Debug.Log("file Path : ");
        NativeGallery.GetImageFromGallery((file) =>
        {
            //�뷮����
            Debug.Log("file Path : 1��°��" + file);
            FileInfo selected = new FileInfo(file);
            //�뷮����
            Debug.Log("file Path : 2��°��" + file);
            if (selected.Length > 50000000)
            {
                Debug.Log("file Path ����");
                return;
            }
            if (!string.IsNullOrEmpty(file))
            {
                Debug.Log("file Path ����");

                //�ҷ��Ͷ�
                StartCoroutine(LoadImage(file));
            }
        });
        Debug.Log("file Path Ž���� ");
        if (loadImage.rectTransform.rotation != Quaternion.identity)
            loadImage.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
    }

    IEnumerator LoadImage(string path)
    {
        yield return null;
        byte[] fileData = File.ReadAllBytes(path);
        string fileName = Path.GetFileName(path).Split('.')[0];
        string savePath = Application.persistentDataPath + "/TestImage";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        File.WriteAllBytes(savePath + fileName + ".png", fileData);

        var temp = File.ReadAllBytes(savePath + fileName + ".png");

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp);
        loadImage.texture = tex;
        loadImage.rectTransform.rotation = Quaternion.identity;
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
            Debug.Log("��");
            soupImage.GetComponent<Func_StickerDrag>().enabled = false;
            SoupState = SoupBubbleType.Attached;
        }
        if (SoupState == SoupBubbleType.Attached && StickState == StickType.Stick)
        {
            soupImage.GetComponent<Func_StickerDrag>().enabled = true;
            SoupState = SoupBubbleType.Soap;
            Debug.Log("������?");
        }
    }
}
