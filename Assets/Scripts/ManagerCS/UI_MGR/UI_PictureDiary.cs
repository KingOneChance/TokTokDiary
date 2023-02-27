using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using static NativeCamera;

public class UI_PictureDiary : MonoBehaviour
{
    //�׸���
    [Header("�׸���")]
    [SerializeField] private Image ui_DrawBackground = null;
    [SerializeField] Button[] ui_WeatherBtn = null;

    [Header("ī�޶�")]
    //ī�޶� on - off, �ҷ�����
    [SerializeField] Button ui_CameraOnBtn = null;
    //[SerializeField] Button cameraOffBtn = null;
    [SerializeField] Button ui_LoadBtn = null;


    [Header("��ƼĿ")]
    [SerializeField] RawImage[] ui_Stickers = null;
    [SerializeField] Button[] ui_StickersBtns = null;

    [Header("�ٴ�")]
    [SerializeField] Button ui_NiddleBtn = null;
    [SerializeField] Texture2D ui_NiddleImage = null;

    [SerializeField] Texture2D ui_StickImage = null;

    [Header("������")]
    //������ ����
    [SerializeField] Image ui_ProfileBackGround = null;
    [SerializeField] RawImage ui_ProfileMain = null;
    [SerializeField] RawImage ui_ProfilePlus = null;
    [SerializeField] RawImage ui_ProfilePick = null;
    [SerializeField] RawImage ui_ProfileOverWrite = null;
    [SerializeField] RawImage ui_ProfileDelete = null;
    [SerializeField] RawImage ui_ProfilePickImagePlus = null;
    [SerializeField] RawImage ui_ProfilePickImageOverWrite = null;

    [SerializeField] RawImage[] basicProfileImges = null;

    //������ �߰�
    [SerializeField] RawImage plusProfileImage = null;
    [SerializeField] Sprite plusSprite = null;
    [SerializeField] TMP_InputField newNickName = null;
    [SerializeField] Sprite selectedSprite = null;
    [SerializeField] RawImage[] profileFrames = null;
    //������ ����
    [SerializeField] RawImage overWriteImage = null;
    [SerializeField] TMP_InputField overWriteNickName = null;
    [Header("��Ÿ")]
    //Ȩ��ư
    [SerializeField] Button ui_HomeBtn = null;
    [SerializeField] private RawImage loadImage;

    private TextMeshProUGUI before = null;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private bool isNiddleClicked = false;
    private bool isStickClicked = false;

    [SerializeField] GameObject soupImage = null;

    [SerializeField] private MouseType MouseState;
    public MouseType MouseStateInfo { get { return MouseState; } }

    Func_LoadProfilePicture func_LoadProfilePicture = null;

    [SerializeField] private TextMeshProUGUI dateText = null;

    private void Start()
    {
        dateText.text = DateTime.Now.ToString("yyyy" + " �� " + "MM" + " �� " + "dd" + "��");
        hotSpot.x = ui_NiddleImage.width / 2;
        hotSpot.y = ui_NiddleImage.height / 2;

       // Manager_Main.Instance.UI_StickerRepository.OnClick_RepositoryOpen();

        //�ٴ� ����
        MouseState = MouseType.None;

        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
        before = new TextMeshProUGUI();
    }

    #region Load Sticker Button
    public void OnClick_BSRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.bubbleStickerList);
    }
    public void OnClick_RecordingRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.recordingStickerList);
    }
    public void OnClick_BGRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.bubbleGunStickerList);
    }
    public void OnClick_BFRepository()
    {
        LoadLocalStickerInventory(Manager_Main.Instance.UI_StickerRepository.bubbleFreeStickerList);
    }
    #endregion
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
        StartCoroutine(LoadImage(path, loadImage));
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
                StartCoroutine(LoadImage(file, loadImage));
            }
        });
        Debug.Log("file Path Ž���� ");
        if (loadImage.rectTransform.rotation != Quaternion.identity)
            loadImage.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
    }
    public void Onclick_LoadImage(RawImage raw)
    {
        //Debug.Log("file Path : ");
        NativeGallery.GetImageFromGallery((file) =>
        {
            //�뷮����
            //Debug.Log("file Path : 1��°��" + file);
            FileInfo selected = new FileInfo(file);
            //�뷮����
            //Debug.Log("file Path : 2��°��" + file);
            if (selected.Length > 50000000)
            {
                //Debug.Log("file Path ����");
                return;
            }
            if (!string.IsNullOrEmpty(file))
            {
                //Debug.Log("file Path ����");

                //�ҷ��Ͷ�
                StartCoroutine(LoadImage(file, raw));
            }
        });
        //Debug.Log("file Path Ž���� ");
        if (loadImage.rectTransform.rotation != Quaternion.identity)
            loadImage.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
    }
    IEnumerator LoadImage(string path, RawImage loadImage)
    {
        yield return null;
        byte[] fileData = File.ReadAllBytes(path);
        string fileName = Path.GetFileName(path).Split('.')[0];
        // string savePath = Application.persistentDataPath + "/TestImage/";

        //if (!Directory.Exists(savePath))
        //{
        //    Directory.CreateDirectory(savePath);
        //}

        File.WriteAllBytes(path + fileName + ".png", fileData);

        var temp = File.ReadAllBytes(path + fileName + ".png");

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp);
        loadImage.texture = tex;
        loadImage.rectTransform.rotation = Quaternion.identity;
    }

    #region NiddleBtn , StickBtn
    public void OnClick_NiddleBtn()
    {
        if (MouseState!=MouseType.Niddle)
        {
            Cursor.SetCursor(ui_NiddleImage, hotSpot, cursorMode);
            isNiddleClicked = true;
            MouseState = MouseType.Niddle;
        }
        else
        {
            Cursor.SetCursor(default, hotSpot, cursorMode);
            isNiddleClicked = false;
            MouseState = MouseType.None;
        }
    }
    public void OnClick_BubbleStick()
    {
        if (MouseState != MouseType.BubbleStick)
        {
            Cursor.SetCursor(ui_StickImage, hotSpot, cursorMode);
            isStickClicked = true;
            MouseState = MouseType.BubbleStick;
        }
        else
        {
            Cursor.SetCursor(default, hotSpot, cursorMode);
            isStickClicked = false;
            MouseState = MouseType.None;
        }
    }

    #endregion

    #region Profile
    // ����ȭ�� x ��ư, ����ȭ�� ��ҹ�ư 

    public void OnClick_ExitProfile()
    {
        ui_ProfileBackGround.gameObject.SetActive(false);
    }

    //������ ����
    public void OnClick_OpenProfileButton()
    {
       new WaitForEndOfFrame();
        ui_ProfileBackGround.gameObject.SetActive(true);
        func_LoadProfilePicture = FindObjectOfType<Func_LoadProfilePicture>();
        ui_ProfileMain.gameObject.SetActive(true);
        ui_ProfilePlus.gameObject.SetActive(false);
        ui_ProfilePick.gameObject.SetActive(false);
        ui_ProfileOverWrite.gameObject.SetActive(false);

    }
    public void SelectedProfile(RawImage profileImg)
    {
        for(int i=0; i < profileFrames.Length; i++)
        {
            profileFrames[i].gameObject.SetActive(false);
        }
        if(profileImg.transform.GetChild(0).gameObject.activeSelf == false)
        {
            profileImg.transform.GetChild(0).gameObject.SetActive(true);
            
        }
    }
    public void OnClick_OpenPlusProfile()
    {
        if (GetDirecotoryCount(Application.persistentDataPath + "/" + "Profile") >= 6)
        {
            return;
        }
        ui_ProfilePlus.gameObject.SetActive(true);
        ui_ProfileMain.gameObject.SetActive(false);
        ui_ProfilePick.gameObject.SetActive(false);
        ui_ProfileOverWrite.gameObject.SetActive(false);
    }

    public void OnClick_OpenOverWriteProfile()
    {
        if (GetDirecotoryCount(Application.persistentDataPath + "/" + "Profile") == 0)
        {
            return;
        }
        InitProfile();
        ui_ProfilePick.gameObject.SetActive(true);
       
        ui_ProfilePlus.gameObject.SetActive(false);
        ui_ProfileMain.gameObject.SetActive(false);
        ui_ProfileOverWrite.gameObject.SetActive(false);
    }

    //������ �߰��κ�
    public void OnClick_ExitPlusProfile()
    {
        plusProfileImage.texture = plusSprite.texture;
        newNickName.text = default;
        InitProfile();
        ui_ProfilePlus.gameObject.SetActive(false);
        ui_ProfileMain.gameObject.SetActive(true);
    }
    public void OnClick_LoadProfilePicture()
    {
        ui_ProfilePickImagePlus.gameObject.SetActive(true);
    }
    
    public void PickProfileImage(RawImage picked)
    {
        plusProfileImage.texture = picked.texture;
        ui_ProfilePickImagePlus.gameObject.SetActive(false);
    }
    public void OnClick_SaveNewProfile()
    {
        if(plusProfileImage.texture == plusSprite.texture || newNickName.text =="")
        {
            return;
        }
        string savePath = Application.persistentDataPath + "/Profile/" + newNickName.text + "/";
        SaveTextureToPng(plusProfileImage.texture, savePath, newNickName.text);
        plusProfileImage.texture = plusSprite.texture;
        newNickName.text = default;
        InitProfile();
        ui_ProfileMain.gameObject.SetActive(true);
        ui_ProfilePlus.gameObject.SetActive(false);
    }
    public int GetDirecotoryCount(string filePath)
    {
        int count = 0;
        try
        {
            if ( Directory.Exists(filePath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                foreach(var item in directoryInfo.GetDirectories())
                {
                    count++;
                 
                }
            }
        }
        catch
        {; }
        return count;
    }
    private void SaveTextureToPng(Texture texture, string directoryPath, string fileName)
    {
        if(GetDirecotoryCount(Application.persistentDataPath + "/" + "Profile") > 6)
        {
            return;
        }
        if (true == string.IsNullOrEmpty(directoryPath)) return;
        if (false == Directory.Exists(Application.persistentDataPath + "/" + "Profile")) Directory.CreateDirectory(Application.persistentDataPath + "/Profile");
        if (false == Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        if (false == Directory.Exists(directoryPath + "/Diary/"))
        {
            Directory.CreateDirectory(directoryPath + "/Diary/");
            Directory.CreateDirectory(directoryPath + "/Jsons/");
            Directory.CreateDirectory(directoryPath + "/Records/");

        }

        int widthValue = texture.width;
        int heightValue = texture.height;

        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture copiedRenderTexture = new RenderTexture(texture.width, texture.height, 0);

        Graphics.Blit(texture, copiedRenderTexture);
        RenderTexture.active = copiedRenderTexture;

        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
        texture2D.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRenderTexture;

        byte[] texturePNGBytes = texture2D.EncodeToPNG();
        string filePath = directoryPath + fileName + ".png";

        File.WriteAllBytes(filePath, texturePNGBytes);
    }

    //������ ������ ����
    public void OnClick_ExitPickProfile()
    {
        InitProfile();
        ui_ProfilePick.gameObject.SetActive(false);
        ui_ProfileMain.gameObject.SetActive(true);

    }

    public void OnClick_PickToOverWriteImage(RawImage picked)
    {
        InitProfile();
        ui_ProfileOverWrite.gameObject.SetActive(true);
        ui_ProfilePick.gameObject.SetActive(false);
        overWriteImage.texture = picked.texture;

    }
    public void OnClick_PickToOverWriteText(TextMeshProUGUI beforeNickName)
    {
        overWriteNickName.text = beforeNickName.text;

        before.text = beforeNickName.text;
    }

    //������ �����κ�
    public void OnClick_ExitOverWriteProfile()
    {
        overWriteImage.texture = null;
        overWriteNickName.text = null;
        ui_ProfileOverWrite.gameObject.SetActive(false);
        ui_ProfilePick.gameObject.SetActive(false);
        ui_ProfileMain.gameObject.SetActive(true);

    } // x ��ư , ��ҹ�ư
    public void OnClick_ChangeProfilePicture()//rawImage ���������°�
    {
        ui_ProfilePickImageOverWrite.gameObject.SetActive(true);

    }
    public void PickProfileOverWrite(RawImage picked)
    {
        overWriteImage.texture = picked.texture;
        ui_ProfilePickImageOverWrite.gameObject.SetActive(false);
    }
    public void OnClick_OverWriteDirectory()
    {
        string path = Application.persistentDataPath + "/Profile/";
        
        if (false == Directory.Exists(path)) Directory.CreateDirectory(Application.persistentDataPath + overWriteNickName.text);

        Rename(path);
        TobeEmpty(path);
        string savePath = Application.persistentDataPath + "/Profile/" + overWriteNickName.text + "/";
        SaveTextureToPng(overWriteImage.texture, savePath, overWriteNickName.text);
        InitProfile();
    }
    private void TobeEmpty(string path)
    {
        string fileName = path + overWriteNickName.text;

        if (File.Exists(fileName + "/" + before.text + ".png"))
        {
            try
            {
                Debug.Log(fileName + "/" + before.text + ".png");
                File.Delete(fileName + "/" + before.text + ".png");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }

    private void Rename(string filepath)
    {
        string oldFile = filepath + "/" + before.text;
        string newFile = filepath + "/" + overWriteNickName.text;
        if(oldFile != newFile)
        Directory.Move(oldFile, newFile);
    }

    public void OnClick_DeleteProfile()
    {
        ui_ProfileDelete.gameObject.SetActive(true);
    }
    private void DeleteDirectory()
    {
        string path = Application.persistentDataPath + "/Profile/" + overWriteNickName.text;
        Directory.Delete(path, true);
    }
    public void OnClick_RealDelete()
    {
        DeleteDirectory();
        InitProfile();
        ui_ProfileDelete.gameObject.SetActive(false);
        ui_ProfilePick.gameObject.SetActive(false);
        ui_ProfilePlus.gameObject.SetActive(false);
        ui_ProfileMain.gameObject.SetActive(true);
        ui_ProfileOverWrite.gameObject.SetActive(false);
    }
    #endregion

    private void InitProfile()
    {
        func_LoadProfilePicture.FillProfileImg();
             func_LoadProfilePicture.FillOverWrite();
    }
}
